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
        protected static void LoadCollection(Collection collection, ApplicationDbContext _db)
        {
            if (collection == null)
                return;
            _db.CollectionFields.Where(cf => cf.CollectionId == collection.Id).Load();
            _db.Users.Where(user => user.Id == collection.UserId).Load();
            _db.Items.Where(item => item.CollectionId == collection.Id).Load();
            _db.IntegerItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).Load();
            _db.StringItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).Load();
            _db.TextItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).Load();
            _db.DatetimeItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).Load();
            _db.BoolItemFields.Where(field => field.CollectionField.CollectionId == collection.Id).Load();
            _db.Tags.Load();
        }
        public IActionResult Index(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");
            LoadCollection(collection, this._db);
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
                    collectionModel.Description ?? "",
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
        public async Task<IActionResult> Item(int id)
        {
            if (!_db.Items.Where(item => item.Id == id).Any())
                return View("NotFound");
            ItemViewModel itemModel = await ItemViewModel.CreateFromItemId(id, _db);
            return View(itemModel);
        }
        [Authorize]
        [HttpGet]
        public IActionResult Edit(int id)
        {
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View("NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");
            LoadCollection(collection, _db);
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
        [Authorize]
        [HttpGet]
        public IActionResult NewItem(int id)
        {
            _db.CollectionFields.Where(cf => cf.CollectionId == id).Load();
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == id);
            if (collection == null)
                return View(viewName: "NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");

            ItemViewModel itemViewModel = new ItemViewModel();
            itemViewModel.CollectionName = collection.Name;
            itemViewModel.CollectionTheme = collection.Theme;
            itemViewModel.ItemFieldNames = collection.CollectionFields.Select(cf => cf.Name).ToArray();
            return View(itemViewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewItem(ItemViewModel model)
        {
            _db.CollectionFields.Where(cf => cf.CollectionId == model.CollectionId).Load();
            Collection? collection = _db.Collections.FirstOrDefault(collection => collection.Id == model.CollectionId);
            if (collection == null)
                return View(viewName: "NotFound");
            if (!(collection.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");

            Item item = new Item(model.Name, _userManager.GetUserId(User), collection.Id);
            _db.Items.Add(item);

            if (model.Tags is not null)
            {
                foreach (string newTagName in model.Tags.Except(_db.Tags.Select(tag => tag.Name)))
                {
                    _db.Tags.Add(new Tag(newTagName));
                }
                item.Tags.AddRange(await _db.Tags.Where(tag => model.Tags.Contains(tag.Name)).ToListAsync());
            }
            List<CollectionField> collectionFields = collection.CollectionFields.ToList();
            if (collectionFields.Count > 0)
            {
                for (int i = 0; i < model.ItemFieldValues.Count(); i++)
                {
                    await item.AddField(model.ItemFieldValues[i],
                        collectionFields[i], _db);
                }
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Item", new { id = item.Id });
        }
    }
}
