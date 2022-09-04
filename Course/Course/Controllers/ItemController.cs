using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;

namespace Course.Controllers
{
    public class ItemController : Controller
    {
        private readonly ApplicationDbContext _db;
        private readonly UserManager<User> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IStringLocalizer<SharedResource> _sharedLocalizer;
        public ItemController(ApplicationDbContext db,
            UserManager<User> userManager,
            RoleManager<IdentityRole> roleManager,
            IStringLocalizer<SharedResource> sharedLocalizer)
        {
            _db = db;
            _userManager = userManager;
            _roleManager = roleManager;
            _sharedLocalizer = sharedLocalizer;
        }
        public async Task<IActionResult> Index(int id)
        {
            if (!_db.Items.Where(item => item.Id == id).Any())
                return View("NotFound");

            ItemViewModel itemModel = await ItemViewModel.CreateFromItemId(id, _db);
            if (User.Identity != null && User.Identity.IsAuthenticated)
                itemModel.IsLiked = (await _db.Users.Where(user => user.UserName == User.Identity.Name)
                    .Include(user => user.LikedItems).FirstAsync())
                    .LikedItems.Any(item => item.Id == id);
            return View(itemModel);
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
            itemViewModel.ItemFields = new ItemViewModel.ItemField[collection.CollectionFields.Count()];
            for (int i = 0; i < itemViewModel.ItemFields.Count(); i++)
                itemViewModel.ItemFields[i] = new ItemViewModel.ItemField
                {
                    Name = collection.CollectionFields[i].Name,
                    Type = collection.CollectionFields[i].Type
                };
            itemViewModel.AllTags = _db.Tags.Select(tag => tag.Name).ToList();
            return View(itemViewModel);
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> NewItem(ItemViewModel model)
        {
            if (!ModelState.IsValid)
            {
                if (model.CollectionId != 0)
                    return RedirectToAction("NewItem", model.CollectionId);
                else
                    return View("NotFound");
            }
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
                    if (!string.IsNullOrWhiteSpace(newTagName))
                    {
                        Tag tempTag = new Tag(newTagName);
                        _db.Tags.Add(tempTag);
                        item.Tags.Add(tempTag);
                    }
                }
                item.Tags.AddRange(await _db.Tags.Where(tag => model.Tags.Contains(tag.Name)).ToListAsync());
            }

            List<CollectionField> collectionFields = collection.CollectionFields.ToList();
            if (collectionFields.Count > 0)
            {
                for (int i = 0; i < model.ItemFields.Count(); i++)
                {
                    await item.AddField(model.ItemFields[i].Value, collectionFields[i], _db);
                }
            }
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = item.Id });
        }
        [Authorize]
        public async Task<IActionResult> DeleteItem(int id, string returnUrl)
        {
            Item? item = _db.Items.FirstOrDefault(item => item.Id == id);
            if (item is null)
                return View(viewName: "NotFound");
            if (!(item.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");
            int collectionId = item.CollectionId;

            _db.Items.Remove(item);
            await _db.SaveChangesAsync();

            return LocalRedirect(returnUrl ??= $"~/Collection/Index/{collectionId}");
        }
        [Authorize]
        [HttpGet]
        public async Task<IActionResult> EditItem(int id)
        {
            Item? item = _db.Items.FirstOrDefault(item => item.Id == id);
            if (item is null)
                return View("NotFound");
            if (!(item.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");

            return View(await ItemEditViewModel.CreateFromItemId(id, _db));
        }
        [Authorize]
        [HttpPost]
        public async Task<IActionResult> EditItem(ItemEditViewModel model)
        {
            Item? item = await _db.Items.Where(item => item.Id == model.Id).Include(item => item.Tags).FirstOrDefaultAsync();
            if (item is null)
                return View("NotFound");
            if (!(item.UserId == _userManager.GetUserId(User) || User.IsInRole("Admin")))
                return Redirect("/Identity/Account/AccessDenied");
            if (!ModelState.IsValid)
                return View(await ItemEditViewModel.CreateFromItemId(model.Id, _db));

            await item.UpdateFromEditModel(model, _db);
            await _db.SaveChangesAsync();
            return RedirectToAction("Index", new { id = item.Id });
        }
        [Authorize]
        public async Task<IActionResult> CommentItem(CommentItemViewModel model)
        {
            Item? item = _db.Items.FirstOrDefault(item => item.Id == model.ItemId);
            if (item is null)
                return View("NotFound");
            if (!string.IsNullOrWhiteSpace(model.Text))
            {
                Comment comment = new Comment(model.Text, _userManager.GetUserId(User), model.ItemId);
                _db.Comments.Add(comment);
                await _db.SaveChangesAsync();
            }
            return RedirectToAction("Index", new { id = model.ItemId });
        }
    }
}
