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
        public async Task UpdateFromEditModel(ItemEditViewModel model, Data.ApplicationDbContext db)
        {
            await Item.LoadFieldsAsync(model.Id, db);
            if (model.Name is not null) Name = model.Name;

            Tags = new List<Tag>();
            if (model.Tags is not null)
            {
                foreach (string newTagName in model.Tags.Except(db.Tags.Select(tag => tag.Name)))
                {
                    if (!string.IsNullOrWhiteSpace(newTagName))
                    {
                        Tag tempTag = new Tag(newTagName);
                        db.Tags.Add(tempTag);
                        Tags.Add(tempTag);
                    }
                }
                Tags.AddRange(await db.Tags.Where(tag => model.Tags.Contains(tag.Name)).ToListAsync());
                await db.SaveChangesAsync();
            }

            var collectionFields = db.CollectionFields.Where(cf => cf.CollectionId == CollectionId);
            foreach(ItemViewModel.ItemField modelItemField in model.ItemFields)
            {
                switch(modelItemField.Type)
                {
                    case "Integer":
                        IntegerFields
                            .First(field => field.CollectionFieldId == collectionFields.First(cf => cf.Name == modelItemField.Name).Id)
                            .Value = int.Parse(modelItemField.Value);
                        break;
                    case "Text":
                        TextFields
                            .First(field => field.CollectionFieldId == collectionFields.First(cf => cf.Name == modelItemField.Name).Id)
                            .Value = modelItemField.Value;
                        break;
                    case "String":
                        StringFields
                            .First(field => field.CollectionFieldId == collectionFields.First(cf => cf.Name == modelItemField.Name).Id)
                            .Value = modelItemField.Value;
                        break;
                    case "Datetime":
                        DatetimeFields
                            .First(field => field.CollectionFieldId == collectionFields.First(cf => cf.Name == modelItemField.Name).Id)
                            .Value = DateTime.Parse(modelItemField.Value);
                        break;
                    case "Bool":
                        BoolFields
                            .First(field => field.CollectionFieldId == collectionFields.First(cf => cf.Name == modelItemField.Name).Id)
                            .Value = bool.Parse(modelItemField.Value);
                        break;
                }
            }
        }
    }
}
