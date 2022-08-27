using Course.Data;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

namespace Course.Models
{
    public class ItemViewModel
    {
        public class ItemField
        {
            public string? Name { get; set; }

            [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
            public string Value { get; set; }

            public string? Type { get; set; }
            public string GetInputType()
            {
                return Type switch
                {
                    "Integer" => "number",
                    "Text" => "text",
                    "String" => "text",
                    "Datetime" => "datetime-local",
                    "Bool" => "checkbox",
                    _ => ""
                };
            }
        }
        public int Id { get; set; }
        [Required(ErrorMessageResourceName = "RequiredError", ErrorMessageResourceType = typeof(Resources.ErrorMessageResource))]
        [Display(Name = "Name", ResourceType = typeof(Resources.DisplayNameResource))]
        public string Name { get; set; }
        public string? UserId { get; set; }
        public string? UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        [Required]
        public int CollectionId { get; set; }
        public string? CollectionName { get; set; }
        public string? CollectionTheme { get; set; }

        public ItemField[] ItemFields { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public string[]? Tags { get; set; }
        public static async Task<ItemViewModel> CreateFromItemId(int itemId, ApplicationDbContext _db)
        {
            
            List<Comment> itemComments = await _db.Comments.Where(comment => comment.ItemId == itemId).ToListAsync();
            await Item.LoadFieldsAsync(itemId, _db);
            Item item = _db.Items.Where(item => item.Id == itemId).Include(item => item.Tags).First();
            await _db.Collections.Where(collection => collection.Id == item.CollectionId).LoadAsync();
            await _db.Users.Where(user => user.Id == item.UserId).LoadAsync();
            await _db.Users.Where(user => itemComments.Select(comment => comment.UserId).Contains(user.Id)).LoadAsync();

            ItemViewModel itemModel = new ItemViewModel();
            itemModel.Id = item.Id;
            itemModel.Name = item.Name;
            itemModel.UserId = item.UserId;
            itemModel.CreatedDate = item.CreatedDate;
            itemModel.CollectionId = item.CollectionId;
            itemModel.UserName = item.User.UserName;
            itemModel.CollectionName = item.Collection.Name;
            itemModel.CollectionTheme = item.Collection.Theme;

            List<CollectionField> collectionFields = _db.CollectionFields
                .Where(cf => cf.CollectionId == item.CollectionId).ToList();
            itemModel.ItemFields = new ItemField[collectionFields.Count()];
            for (int i = 0; i < collectionFields.Count(); i++)
            {
                itemModel.ItemFields[i] = new ItemField { Name = collectionFields[i].Name, Type = collectionFields[i].Type };
                switch (collectionFields[i].Type)
                {
                    case "Integer":
                        itemModel.ItemFields[i].Value = collectionFields[i].IntegerItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                    case "String":
                        itemModel.ItemFields[i].Value = collectionFields[i].StringItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value;
                        break;
                    case "Text":
                        itemModel.ItemFields[i].Value = collectionFields[i].TextItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value;
                        break;
                    case "Datetime":
                        itemModel.ItemFields[i].Value = collectionFields[i].DatetimeItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                    case "Bool":
                        itemModel.ItemFields[i].Value = collectionFields[i].BoolItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                }
            }
            
                itemModel.Comments = itemComments;
            
            itemModel.Tags = new string[item.Tags.Count];
            for (int i = 0; i < item.Tags.Count; i++)
                itemModel.Tags[i] = item.Tags[i].Name;
            return itemModel;
        }
    }
}
