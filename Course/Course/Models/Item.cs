using Microsoft.EntityFrameworkCore;

namespace Course.Models
{
    public class Item
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public DateTime CreatedDate { get; set; }
        public int CollectionId { get; set; }

        public User User { get; set; }
        public Collection Collection { get; set; }
        public List<Comment> Comments { get; set; } = new();
        public List<Tag> Tags { get; set; } = new();
        public List<IntegerItemField> IntegerFields { get; set; } = new();
        public List<StringItemField> StringFields { get; set; } = new();
        public List<TextItemField> TextFields { get; set; } = new();
        public List<BoolItemField> BoolFields { get; set; } = new();
        public List<DatetimeItemField> DatetimeFields { get; set; } = new();
        private Item()
        {
            CreatedDate = DateTime.Now.ToUniversalTime();
        }
        public Item(string name, string userId, int collectionId) : this()
        {
            Name = name;
            UserId = userId;
            CollectionId = collectionId;
        }
        public async Task AddField(string value, CollectionField collectionField, Data.ApplicationDbContext db)
        {

            switch (collectionField.Type)
            {
                case "Integer":
                    if (int.TryParse(value, out var intResult))
                    {
                        IntegerItemField integerField = new IntegerItemField(intResult, Id, collectionField.Id);
                        db.IntegerItemFields.Add(integerField);
                        IntegerFields.Add(integerField);
                    }
                    break;
                case "Text":
                    TextItemField textField = new TextItemField(value, Id, collectionField.Id);
                    db.TextItemFields.Add(textField);
                    TextFields.Add(textField);
                    break;
                case "String":
                    StringItemField stringField = new StringItemField(value, Id, collectionField.Id);
                    db.StringItemFields.Add(stringField);
                    StringFields.Add(stringField);
                    break;
                case "Datetime":
                    if (DateTime.TryParse(value, out var datetimeResult))
                    {
                        DatetimeItemField datetimeField = new DatetimeItemField(datetimeResult, Id, collectionField.Id);
                        db.DatetimeItemFields.Add(datetimeField);
                        DatetimeFields.Add(datetimeField);
                    }
                    break;
                case "Bool":
                    if(bool.TryParse(value, out var boolResult))
                    {
                        BoolItemField boolField = new BoolItemField(boolResult, Id, collectionField.Id);
                        db.BoolItemFields.Add(boolField);
                        BoolFields.Add(boolField);
                    }
                    break;
                default:
                    break;
            }
            await db.SaveChangesAsync();
        }
        public async static Task LoadFieldsAsync(int itemId, Data.ApplicationDbContext db)
        {
            await db.IntegerItemFields.Where(field => field.ItemId == itemId).LoadAsync();
            await db.StringItemFields.Where(field => field.ItemId == itemId).LoadAsync();
            await db.TextItemFields.Where(field => field.ItemId == itemId).LoadAsync();
            await db.DatetimeItemFields.Where(field => field.ItemId == itemId).LoadAsync();
            await db.BoolItemFields.Where(field => field.ItemId == itemId).LoadAsync();
        }
    }
}
