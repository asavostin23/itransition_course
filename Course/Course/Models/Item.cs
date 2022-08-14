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
    }
}
