using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Localization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace Course.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        private readonly ApplicationDbContext _db;

        public HomeController(ILogger<HomeController> logger,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            ApplicationDbContext db)
        {
            _logger = logger;
            _userManager = userManager;
            _roleManager = roleManager;
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            User currentUser = await _userManager.GetUserAsync(HttpContext.User);
            if (currentUser != null && await _userManager.IsInRoleAsync(currentUser, "Admin"))
                ViewBag.IsAdmin = true;
            else
                ViewBag.IsAdmin = false;

            HomeIndexViewModel model = new();
            model.TopCollections = await _db.Collections.OrderByDescending(collection => collection.Items.Count).Take(5)
                .Include(collection => collection.Items)
                .Include(collection => collection.CollectionFields)
                .ToListAsync();
            await _db.Users.Where(user => model.TopCollections.Select(collection => collection.UserId).Contains(user.Id)).LoadAsync();
            foreach(int itemId in model.TopCollections.SelectMany(collection => collection.Items).Select(item => item.Id))
            {
                await Item.LoadFieldsAsync(itemId, _db);
            }
            model.LastAddedItems = await _db.Items.OrderByDescending(item => item.CreatedDate).Take(10).Include(item => item.User).Include(item => item.Collection).ToListAsync();
            model.Tags = await _db.Tags.Include(tag => tag.Items).ToListAsync();

            return View(model);
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
        public IActionResult SetLanguage(string culture, string returnUrl)
        {
            Response.Cookies.Append(
                CookieRequestCultureProvider.DefaultCookieName,
                CookieRequestCultureProvider.MakeCookieValue(new RequestCulture(culture)),
                new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl ??= "Index");
        }
        public IActionResult SetTheme(string theme, string returnUrl)
        {
            Response.Cookies.Append("Theme", theme, new CookieOptions { Expires = DateTimeOffset.UtcNow.AddYears(1) });
            return LocalRedirect(returnUrl ??= "Index");
        }
    }
}