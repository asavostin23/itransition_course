using Course.Data;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;
using System.Security.AccessControl;
using System.Xml.Linq;
using static Course.Models.ItemViewModel;

namespace Course.Models
{
    public class ItemEditViewModel
    {
        public int Id { get; set; }

        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Name", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Name { get; set; }
        public ItemViewModel.ItemField[]? ItemFields { get; set; }
        public string[]? Tags { get; set; }
        public string? CollectionName { get; set; }
        public string? CollectionTheme { get; set; }
        public List<string>? AllTags { get; set; }
        public static async Task<ItemEditViewModel> CreateFromItemId(int itemId, ApplicationDbContext db)
        {
            Item item = await db.Items.Where(item => item.Id == itemId).Include(item => item.Tags).FirstOrDefaultAsync();
            ItemEditViewModel result = new ItemEditViewModel();
            result.Id = item.Id;
            result.Name = item.Name;
            result.Tags = item.Tags.Select(tag => tag.Name).ToArray();
            Collection collection = await db.Collections.FirstOrDefaultAsync(collection => collection.Id == item.CollectionId);
            result.CollectionName = collection.Name;
            result.CollectionTheme = collection.Theme;
            List<CollectionField> collectionFields = db.CollectionFields.Where(cf => cf.CollectionId == item.CollectionId).ToList();
            int i = 0;
            result.ItemFields = new ItemViewModel.ItemField[collectionFields.Count];
            foreach(CollectionField collectionField in collectionFields)
            {
                ItemViewModel.ItemField itemField = new();
                itemField.Name = collectionField.Name;
                itemField.Type = collectionField.Type;
                itemField.Value = collectionField.Type switch
                {
                    "Integer" => (await db.IntegerItemFields.FirstOrDefaultAsync(field => field.CollectionFieldId == collectionField.Id && field.ItemId == item.Id)).Value.ToString(),
                    "String" => (await db.StringItemFields.FirstOrDefaultAsync(field => field.CollectionFieldId == collectionField.Id && field.ItemId == item.Id)).Value.ToString(),
                    "Text" => (await db.TextItemFields.FirstOrDefaultAsync(field => field.CollectionFieldId == collectionField.Id && field.ItemId == item.Id)).Value.ToString(),
                    "Datetime" => (await db.DatetimeItemFields.FirstOrDefaultAsync(field => field.CollectionFieldId == collectionField.Id && field.ItemId == item.Id)).Value.ToString("s"),
                    "Bool" => (await db.BoolItemFields.FirstOrDefaultAsync(field => field.CollectionFieldId == collectionField.Id && field.ItemId == item.Id)).Value.ToString(),
                    _ => "Error"
                };
                result.ItemFields[i++] = itemField;
            }
            result.AllTags = db.Tags.Select(tag => tag.Name).ToList();
            return result;
        }
    }
}
