namespace Course.Models
{
    public class IntegerItemField : ItemField<int>
    {
        public IntegerItemField(int value, int itemId, int collectionFieldId) : base(value, itemId, collectionFieldId)
        {
        }

        public override string Type => "Integer";
    }
}
