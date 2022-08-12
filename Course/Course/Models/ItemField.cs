namespace Course.Models
{
    public abstract class ItemField<T>
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public T Value { get; set; }
        public int CollectionFieldId { get; set; }

        public abstract string Type { get; }

        public Item Item { get; set; }
        public CollectionField CollectionField { get; set; }

        public ItemField(T value, int itemId, int collectionFieldId)
        {
            Value = value;
            ItemId = itemId;
            CollectionFieldId = collectionFieldId;
        }
    }
}
