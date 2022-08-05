namespace Course.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string Description { get; set; }
        public string Theme { get; set; }
        public byte[]? ImageData { get; set; }

        public User User { get; set; }
        public ICollection<Item> Items { get; set; }
        public ICollection<CollectionField> CollectionFields { get; set; }

        public Collection(string name, string description, string theme, string userId, byte[]? imageData = null)
        {
            Name = name;
            UserId = userId;
            Description = description;
            Theme = theme;
            ImageData = imageData;
        }
    }
}
