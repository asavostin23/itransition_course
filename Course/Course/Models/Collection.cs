namespace Course.Models
{
    public class Collection
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string UserId { get; set; }
        public string? Description { get; set; }
        public string Theme { get; set; }
        public byte[]? ImageData { get; set; }

        public User User { get; set; }
        public List<Item> Items { get; set; } = new();
        public List<CollectionField> CollectionFields { get; set; } = new();

        public Collection(string name, string description, string theme, string userId, byte[]? imageData = null)
        {
            Name = name;
            UserId = userId;
            Description = description;
            Theme = theme;
            ImageData = imageData;
        }
        public void UpdateFromEditModel(CollectionEditViewModel model)
        {
            if (model.Name is not null) Name = model.Name;
            if (model.Description is not null) Description = model.Description;
            if (model.Theme is not null) Theme = model.Theme;
            if (model.ImageData is not null)
            {
                byte[] imageData = null;
                using (BinaryReader binaryReader = new BinaryReader(model.ImageData.OpenReadStream()))
                {
                    imageData = binaryReader.ReadBytes((int)model.ImageData.Length);
                }
                ImageData = imageData;
            }
        }
        public bool RemoveItemById(int id)
        {
            return Items.Remove(Items.FirstOrDefault(item => item.Id == id));
        }
        public void AddItem(Item item)
        {
            Items.Add(item);
        }
    }
}
