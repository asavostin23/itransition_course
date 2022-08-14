namespace Course.Models
{
    public class CollectionField
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Type { get; set; }
        public int CollectionId { get; set; }

        public Collection Collection { get; set; }

        public List<BoolItemField> BoolItemFields { get; set; } = new();
        public List<IntegerItemField> IntegerItemFields { get; set; } = new();
        public List<DatetimeItemField> DatetimeItemFields { get; set; } = new();
        public List<StringItemField> StringItemFields { get; set; } = new();
        public List<TextItemField> TextItemFields { get; set; } = new();

        public CollectionField(string name, string type, int collectionId)
        {
            Name = name;
            Type = type;
            CollectionId = collectionId;
        }
        public CollectionField(string name, string type)
        {
            Name = name;
            Type = type;
        }
    }
}
