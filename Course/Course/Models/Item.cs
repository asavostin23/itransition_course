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
        public ICollection<Comment> Comments { get; set; }
        public ICollection<Tag> Tags { get; set; }
        public ICollection<IntegerItemField> IntegerFields { get; set; }
        public ICollection<StringItemField> StringFields { get; set; }
        public ICollection<TextItemField> TextFields { get; set; }
        public ICollection<BoolItemField> BoolFields { get; set; }
        public ICollection<DatetimeItemField> DatetimeFields { get; set; }
        private Item()
        {
            CreatedDate = DateTime.Now;
        }
        public Item(string name, string userId, int collectionId) : this()
        {
            Name = name;
            UserId = userId;
            CollectionId = collectionId;
        }
    }
}
