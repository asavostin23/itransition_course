namespace Course.Models
{
    public class TextItemField : ItemField<string>
    {
        public TextItemField(string value, int itemId, int collectionFieldId) : base(value, itemId, collectionFieldId)
        {
        }

        public override string Type => "Text";
    }
}
