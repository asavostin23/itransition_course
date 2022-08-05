namespace Course.Models
{
    public class CollectionField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        public CollectionField(string name, string type, int collectionId)
        {
            Name = name;
            Type = type;
            CollectionId = collectionId;
        }
    }
}
