using Course.Data;
using Microsoft.EntityFrameworkCore;

namespace Course.Models
{
    public class ItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string UserName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CollectionId { get; set; }
        public string CollectionName { get; set; }
        public string CollectionTheme { get; set; }

        public string[] ItemFieldValues { get; set; }
        public string[] ItemFieldNames { get; set; }
        public List<string> Comments { get; set; } = new();
        public string[] Tags { get; set; }
        public static async Task<ItemViewModel> CreateFromItemId(int itemId, ApplicationDbContext _db)
        {
            await _db.Collections.Where(collection => collection.Items.Where(item => item.Id == itemId).Any()).LoadAsync();
            await _db.Users.Where(u => u.Items.Where(item => item.Id == itemId).Any()).LoadAsync();
            await _db.Comments.Where(comment => comment.ItemId == itemId).LoadAsync();
            await Item.LoadFieldsAsync(itemId, _db);
            Item item = _db.Items.Where(item => item.Id == itemId).Include(item => item.Tags).FirstOrDefault();

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
            itemModel.ItemFieldNames = collectionFields.Select(cf => cf.Name).ToArray();
            itemModel.ItemFieldValues = new string[itemModel.ItemFieldNames.Length];

            for (int i = 0; i < collectionFields.Count; i++)
            {
                switch (collectionFields[i].Type)
                {
                    case "Integer":
                        itemModel.ItemFieldValues[i] = collectionFields[i].IntegerItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                    case "String":
                        itemModel.ItemFieldValues[i] = collectionFields[i].StringItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value;
                        break;
                    case "Text":
                        itemModel.ItemFieldValues[i] = collectionFields[i].TextItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value;
                        break;
                    case "Datetime":
                        itemModel.ItemFieldValues[i] = collectionFields[i].DatetimeItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                    case "Bool":
                        itemModel.ItemFieldValues[i] = collectionFields[i].BoolItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString();
                        break;
                }
            }
            foreach (Comment comment in item.Comments)
            {
                itemModel.Comments.Add($"{comment.User.UserName} - {comment.CreatedDate} - {comment.Text}");
            }
            itemModel.Tags = new string[item.Tags.Count];
            for (int i = 0; i < item.Tags.Count; i++)
                itemModel.Tags[i] = item.Tags[i].Name;
            return itemModel;
        }
    }
}
