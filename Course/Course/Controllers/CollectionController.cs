using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Course.Controllers
{
    public class CollectionController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public CollectionController(ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _sharedLocalizer = sharedLocalizer;
        }
        protected async static Task LoadCollectionAsync(Collection collection, ApplicationDbContext _db)
        {
            if (collection == null)
                return;
            await _db.CollectionFields.Where(cf => cf.CollectionId == collection.Id).LoadAsync();
            await _db.Users.Where(user => user.Id == collection.UserId).LoadAsync();
            await _db.Items.Where(item => item.CollectionId == collection.Id).LoadAsync();
            await _db.IntegerItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).LoadAsync();
            await _db.StringItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).LoadAsync();
            await _db.TextItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).LoadAsync();
            await _db.DatetimeItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).LoadAsync();
            await _db.BoolItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).LoadAsync();
            await _db.Tags.LoadAsync();
        }
        public async Task<IActionResult> Index(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");
            await LoadCollectionAsync(collection, this._db);
            return View(collection);
        }
        [Authorize]
        [HttpGet]
        public IActionResult New()
        {
            ViewBag.PossibleCollectionTypes =
                new List<SelectListItem> {
                    new SelectListItem { Text = _sharedLocalizer["Integer"], Value = "Integer" },
                    new SelectListItem { Text = _sharedLocalizer["Text"], Value = "Text" },
                    new SelectListItem { Text = _sharedLocalizer["String"], Value = "String" },
                    new SelectListItem { Text = _sharedLocalizer["Datetime"], Value = "Datetime" },
                    new SelectListItem { Text = _sharedLocalizer["Bool"], Value = "Bool" }
                };
            return View();
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> New(CollectionNewViewModel collectionModel)
        {
            if (ModelState.IsValid)
            {
                Collection collection = new Collection
                    (collectionModel.Name,
                    collectionModel.Description,
                    collectionModel.Theme,
                    (await _userManager.GetUserAsync(User)).Id);

                for (int i = 0; i < collectionModel.FieldTypes.Count(); i++)
                {
                    if (collectionModel.FieldTypes[i] != "Nothing")
                    {
                        collection.CollectionFields
                            .Add(new CollectionField(collectionModel.FieldNames[i], collectionModel.FieldTypes[i]));
                    }
                }
                if (collectionModel.ImageData != null)
                {
                    byte[] imageData;
                    using (BinaryReader binaryReader = new BinaryReader(collectionModel.ImageData.OpenReadStream()))
                    {
                        imageData = binaryReader.ReadBytes((int)collectionModel.ImageData.Length);
                    }
                    collection.ImageData = imageData;
                }
                _db.Collections.Add(collection);
                await _db.SaveChangesAsync();
                return RedirectToAction("Index", new { id = collection.Id.ToString() });
            }
            return View();
        }
        
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");

            await LoadCollectionAsync(collection, _db);
            return View(collection);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> Edit(CollectionEditViewModel updatedCollection)
        {
            Collection? collection = _db.Collections.FirstOrDefault(col => col.Id == updatedCollection.Id);
            if (collection == null)
                return RedirectToAction("NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");
            if (!ModelState.IsValid)
                return View(collection);

            collection.UpdateFromEditModel(updatedCollection);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = collection.Id.ToString() });
        }
        [Authorize]
        public async Task<IActionResult> EditDeleteImage(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");

            collection.ImageData = null;
            await _db.SaveChangesAsync();
            return RedirectToAction("Edit", new { id = collection.Id.ToString() });
        }
        
        
        [Route("{controller}/{action}/{username?}")]
        public async Task<IActionResult> UserCollections(string username)
        {
            if(username is null)
            {
                if (User.Identity != null && User.Identity.IsAuthenticated)
                    username = User.Identity.Name;
                else
                    return View(viewName: "NotFound");
            }
            User? user = await _userManager.FindByNameAsync(username);
            if (user is null)
                return View(viewName: "NotFound");
            ViewBag.UserName = user.UserName;

            await _db.Entry(user).Collection(user => user.Collections).LoadAsync();
            List<Collection> userCollections = user.Collections.ToList();
            foreach (Collection collection in userCollections)
                await LoadCollectionAsync(collection, _db);
            return View(userCollections);
        }
        
    }
}
