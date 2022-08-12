using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Course.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private RoleManager<IdentityRole> _roleManager;
        public CollectionController(ApplicationDbContext db, UserManager<User> userManager, RoleManager<IdentityRole> roleManager)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
        }
        public IActionResult Index(int id)
        {
            
            return View("NotFound");
        }
        [Authorize]
        [HttpGet]
        public IActionResult New()
        {
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> New(CollectionViewModel collectionModel)
        {
            if (ModelState.IsValid)
            {
            Collection collection = new Collection(
                collectionModel.Name,
                collectionModel.Description,
                collectionModel.Theme,
                (await _userManager.GetUserAsync(User)).Id);
            }
            return View();
        }
        public IActionResult Item(int id)
        {
            Item? item = _db.Items.FirstOrDefault(item => item.Id == id);
            if (item != null)
            {
                ItemViewModel itemModel = ItemViewModel.CreateFromItem(item, _db);
                return View(itemModel);
            }
            return View("NotFound");
        }
    }
}
