namespace Course.Models
{
    public class StringItemField : ItemField<string>
    {
        public StringItemField(string value, int itemId, int collectionFieldId) : base(value, itemId, collectionFieldId)
        {
        }
        public override string Type => "String";
    }
}
