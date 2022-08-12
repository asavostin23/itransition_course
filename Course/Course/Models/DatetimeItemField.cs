namespace Course.Models
{
    public class DatetimeItemField : ItemField<DateTime>
    {
        public DatetimeItemField(DateTime value, int itemId, int collectionFieldId) : base(value, itemId, collectionFieldId)
        {
        }

        public override string Type => "Datetime";
    }
}
