namespace Course.Models
{
    public class BoolItemField : ItemField<bool>
    {
        public BoolItemField(bool value, int itemId, int collectionFieldId) : base(value, itemId, collectionFieldId)
        {
        }

        public override string Type => "Bool";
    }
}
