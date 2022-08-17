select 
IntegerItemFields.Id,
IntegerItemFields.ItemId,
IntegerItemFields.CollectionFieldId,
IntegerItemFields.Value as "IntVal",
TextItemFields.Value as "IntVal",
StringItemFields.Value as "StrVal",
DatetimeItemFields.Value as "DateVal",
BoolItemFields.Value as "BoolVal"
from
IntegerItemFields
join TextItemFields on TextItemFields.ItemId = IntegerItemFields.ItemId
join StringItemFields on StringItemFields.ItemId = IntegerItemFields.ItemId
join DatetimeItemFields on DatetimeItemFields.ItemId = IntegerItemFields.ItemId
join BoolItemFields on BoolItemFields.ItemId = IntegerItemFields.ItemId;