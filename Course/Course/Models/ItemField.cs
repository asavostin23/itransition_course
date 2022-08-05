namespace Course.Models
{
    public class ItemField<T>
    {
        public int Id { get; set; }
        public int ItemId { get; set; }
        public T Value { get; set; }

        public Item Item { get; set; }

        public ItemField(T value, int itemId)
        {
            Value = value;
            ItemId = itemId;
        }
    }
}
