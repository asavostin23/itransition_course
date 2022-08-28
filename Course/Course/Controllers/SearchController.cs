using Course.Data;
using Course.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGeneration.EntityFrameworkCore;
using NuGet.Packaging;

namespace Course.Controllers
{
    public class SearchController : Controller
    {
        private readonly ApplicationDbContext _db;
        public SearchController(ApplicationDbContext db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(string searchText)
        {
            if (string.IsNullOrEmpty(searchText))
                return View();
            List<User> userSearch = await _db.Users.Where(user => EF.Functions.FreeText(user.UserName, searchText)).ToListAsync();
            List<Tag> tagSearch = await _db.Tags.Where(tag => EF.Functions.FreeText(tag.Name, searchText)).Include(tag => tag.Items).ToListAsync();
            
            List<Collection> collectionSearch =  await _db.Collections
                .Where(collection => EF.Functions.FreeText(collection.Name, searchText)
                || EF.Functions.FreeText(collection.Theme, searchText)
                || EF.Functions.FreeText(collection.Description, searchText)).ToListAsync();
            collectionSearch.AddRange(await _db.CollectionFields.Where(cf => EF.Functions.FreeText(cf.Name, searchText)).Join(_db.Collections, cf => cf.CollectionId, col => col.Id, (cf, col) => col).ToListAsync());
            await _db.Users.Where(user => collectionSearch.Select(collection => collection.UserId).Contains(user.Id)).LoadAsync();

            List<Item> itemSearch = await _db.Items.Where(item => EF.Functions.FreeText(item.Name, searchText)).Include(item => item.Tags).ToListAsync();
            itemSearch.AddRange(await _db.Comments.Where(comment => EF.Functions.FreeText(comment.Text, searchText)).Join(_db.Items, comment=> comment.ItemId, item => item.Id, (comment, item) => item).ToListAsync());
            itemSearch.AddRange(await _db.StringItemFields.Where(field => EF.Functions.FreeText(field.Value, searchText)).Join(_db.Items, field => field.ItemId, item => item.Id, (field, item) => item).ToListAsync());
            itemSearch.AddRange(await _db.TextItemFields.Where(field => EF.Functions.FreeText(field.Value, searchText)).Join(_db.Items, field => field.ItemId, item => item.Id, (field, item) => item).ToListAsync());
            itemSearch.AddRange(tagSearch.SelectMany(tag => tag.Items));
            itemSearch = new List<Item>(itemSearch.Distinct());
            await _db.Collections.Where(collection => itemSearch.Select(item => item.CollectionId).Contains(collection.Id)).LoadAsync();

            return View(new SearchViewModel(itemSearch, collectionSearch, userSearch, tagSearch));
        }
    }
}
