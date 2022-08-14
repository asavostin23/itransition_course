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
        public CollectionController(
            ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _sharedLocalizer = sharedLocalizer;
        }
        public IActionResult Index(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");

            _db.CollectionFields.Where(cf => cf.CollectionId == id).Load();
            _db.Items.Where(item => item.CollectionId == id).Load();
            _db.IntegerItemFields.Where(field => field.CollectionField.CollectionId == id).Load();
            _db.StringItemFields.Where(field => field.CollectionField.CollectionId == id).Load();
            _db.TextItemFields.Where(field => field.CollectionField.CollectionId == id).Load();
            _db.DatetimeItemFields.Where(field => field.CollectionField.CollectionId == id).Load();
            _db.BoolItemFields.Where(field => field.CollectionField.CollectionId == id).Load();
            _db.Tags.Load();
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
        public async Task<IActionResult> New(CollectionViewModel collectionModel)
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
                    byte[] imageData = null;
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
