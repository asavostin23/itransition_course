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
        public List<string> ItemFieldValues { get; set; }
        public List<string> ItemFieldNames { get; set; }
        public List<string> Comments { get; set; }
        public List<string> Tags { get; set; }
        public static ItemViewModel CreateFromItem(Item item, ApplicationDbContext _db)
        {
            ItemViewModel itemModel = new ItemViewModel();
            itemModel.Id = item.Id;
            itemModel.Name = item.Name;
            itemModel.UserId = item.UserId;
            itemModel.CreatedDate = item.CreatedDate;
            _db.Collections.Where(collection => collection.Id == item.CollectionId).Load();
            itemModel.CollectionId = item.CollectionId;
            _db.Users.Where(u => u.Id == item.UserId).Load();
            itemModel.UserName = item.User.UserName;
            itemModel.CollectionName = item.Collection.Name;
            itemModel.CollectionTheme = item.Collection.Theme;

            IQueryable<CollectionField> collectionFields = _db.CollectionFields
                .Where(cf => cf.CollectionId == item.CollectionId);

            itemModel.ItemFieldNames.AddRange(
                collectionFields
                .Select(cf => cf.Name));
            foreach (CollectionField collectionField in collectionFields)
            {
                switch (collectionField.Type)
                {
                    case "Integer":
                        itemModel.ItemFieldValues
                            .Add(collectionField.IntegerItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString());
                        break;
                    case "String":
                        itemModel.ItemFieldValues
                            .Add(collectionField.StringItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value);
                        break;
                    case "Text":
                        itemModel.ItemFieldValues
                            .Add(collectionField.TextItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value);
                        break;
                    case "Datetime":
                        itemModel.ItemFieldValues
                            .Add(collectionField.DatetimeItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString());
                        break;
                    case "Bool":
                        itemModel.ItemFieldValues
                            .Add(collectionField.BoolItemFields.FirstOrDefault(itemField => itemField.ItemId == item.Id).Value.ToString());
                        break;
                }
            }
            _db.Comments.Where(comment => comment.ItemId == item.Id).Load();
            foreach(Comment comment in item.Comments)
            {
                itemModel.Comments.Add($"{comment.User.UserName} - {comment.CreatedDate} - {comment.Text}");
            }
            _db.Tags.Load();
            foreach (Tag tag in item.Tags)
                itemModel.Tags.Add(tag.Name);
            return itemModel;
        }
    }
}
