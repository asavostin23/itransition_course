using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Data.Migrations
{
    public partial class dbSetsAdded : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolItemField_Item_ItemId",
                table: "BoolItemField");

            migrationBuilder.DropForeignKey(
                name: "FK_Collection_AspNetUsers_UserId",
                table: "Collection");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionField_Collection_CollectionId",
                table: "CollectionField");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_Comment_Item_ItemId",
                table: "Comment");

            migrationBuilder.DropForeignKey(
                name: "FK_DatetimeItemField_Item_ItemId",
                table: "DatetimeItemField");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegerItemField_Item_ItemId",
                table: "IntegerItemField");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_AspNetUsers_UserId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTag_Item_ItemsId",
                table: "ItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTag_Tag_TagsId",
                table: "ItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_StringItemField_Item_ItemId",
                table: "StringItemField");

            migrationBuilder.DropForeignKey(
                name: "FK_TextItemField_Item_ItemId",
                table: "TextItemField");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tag_Name",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tag",
                table: "Tag");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StringItemField",
                table: "StringItemField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Item",
                table: "Item");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegerItemField",
                table: "IntegerItemField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatetimeItemField",
                table: "DatetimeItemField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comment",
                table: "Comment");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionField",
                table: "CollectionField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collection",
                table: "Collection");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoolItemField",
                table: "BoolItemField");

            migrationBuilder.RenameTable(
                name: "Tag",
                newName: "Tags");

            migrationBuilder.RenameTable(
                name: "StringItemField",
                newName: "StringItemFields");

            migrationBuilder.RenameTable(
                name: "Item",
                newName: "Items");

            migrationBuilder.RenameTable(
                name: "IntegerItemField",
                newName: "IntegerItemFields");

            migrationBuilder.RenameTable(
                name: "DatetimeItemField",
                newName: "DatetimeItemFields");

            migrationBuilder.RenameTable(
                name: "Comment",
                newName: "Comments");

            migrationBuilder.RenameTable(
                name: "CollectionField",
                newName: "CollectionFields");

            migrationBuilder.RenameTable(
                name: "Collection",
                newName: "Collections");

            migrationBuilder.RenameTable(
                name: "BoolItemField",
                newName: "BoolItemFields");

            migrationBuilder.RenameIndex(
                name: "IX_StringItemField_ItemId",
                table: "StringItemFields",
                newName: "IX_StringItemFields_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_UserId",
                table: "Items",
                newName: "IX_Items_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Item_CollectionId",
                table: "Items",
                newName: "IX_Items_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_IntegerItemField_ItemId",
                table: "IntegerItemFields",
                newName: "IX_IntegerItemFields_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DatetimeItemField_ItemId",
                table: "DatetimeItemFields",
                newName: "IX_DatetimeItemFields_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_UserId",
                table: "Comments",
                newName: "IX_Comments_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comment_ItemId",
                table: "Comments",
                newName: "IX_Comments_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionField_CollectionId",
                table: "CollectionFields",
                newName: "IX_CollectionFields_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_Collection_UserId",
                table: "Collections",
                newName: "IX_Collections_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_BoolItemField_ItemId",
                table: "BoolItemFields",
                newName: "IX_BoolItemFields_ItemId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tags_Name",
                table: "Tags",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tags",
                table: "Tags",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StringItemFields",
                table: "StringItemFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Items",
                table: "Items",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegerItemFields",
                table: "IntegerItemFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatetimeItemFields",
                table: "DatetimeItemFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comments",
                table: "Comments",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionFields",
                table: "CollectionFields",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collections",
                table: "Collections",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoolItemFields",
                table: "BoolItemFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolItemFields_Items_ItemId",
                table: "BoolItemFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionFields_Collections_CollectionId",
                table: "CollectionFields",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comments_Items_ItemId",
                table: "Comments",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DatetimeItemFields_Items_ItemId",
                table: "DatetimeItemFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegerItemFields_Items_ItemId",
                table: "IntegerItemFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Items_AspNetUsers_UserId",
                table: "Items",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items",
                column: "CollectionId",
                principalTable: "Collections",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTag_Items_ItemsId",
                table: "ItemTag",
                column: "ItemsId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTag_Tags_TagsId",
                table: "ItemTag",
                column: "TagsId",
                principalTable: "Tags",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringItemFields_Items_ItemId",
                table: "StringItemFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemField_Items_ItemId",
                table: "TextItemField",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolItemFields_Items_ItemId",
                table: "BoolItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_CollectionFields_Collections_CollectionId",
                table: "CollectionFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_AspNetUsers_UserId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_AspNetUsers_UserId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_Comments_Items_ItemId",
                table: "Comments");

            migrationBuilder.DropForeignKey(
                name: "FK_DatetimeItemFields_Items_ItemId",
                table: "DatetimeItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegerItemFields_Items_ItemId",
                table: "IntegerItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_AspNetUsers_UserId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_Items_Collections_CollectionId",
                table: "Items");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTag_Items_ItemsId",
                table: "ItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_ItemTag_Tags_TagsId",
                table: "ItemTag");

            migrationBuilder.DropForeignKey(
                name: "FK_StringItemFields_Items_ItemId",
                table: "StringItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_TextItemField_Items_ItemId",
                table: "TextItemField");

            migrationBuilder.DropUniqueConstraint(
                name: "AK_Tags_Name",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Tags",
                table: "Tags");

            migrationBuilder.DropPrimaryKey(
                name: "PK_StringItemFields",
                table: "StringItemFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Items",
                table: "Items");

            migrationBuilder.DropPrimaryKey(
                name: "PK_IntegerItemFields",
                table: "IntegerItemFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_DatetimeItemFields",
                table: "DatetimeItemFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Comments",
                table: "Comments");

            migrationBuilder.DropPrimaryKey(
                name: "PK_Collections",
                table: "Collections");

            migrationBuilder.DropPrimaryKey(
                name: "PK_CollectionFields",
                table: "CollectionFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_BoolItemFields",
                table: "BoolItemFields");

            migrationBuilder.RenameTable(
                name: "Tags",
                newName: "Tag");

            migrationBuilder.RenameTable(
                name: "StringItemFields",
                newName: "StringItemField");

            migrationBuilder.RenameTable(
                name: "Items",
                newName: "Item");

            migrationBuilder.RenameTable(
                name: "IntegerItemFields",
                newName: "IntegerItemField");

            migrationBuilder.RenameTable(
                name: "DatetimeItemFields",
                newName: "DatetimeItemField");

            migrationBuilder.RenameTable(
                name: "Comments",
                newName: "Comment");

            migrationBuilder.RenameTable(
                name: "Collections",
                newName: "Collection");

            migrationBuilder.RenameTable(
                name: "CollectionFields",
                newName: "CollectionField");

            migrationBuilder.RenameTable(
                name: "BoolItemFields",
                newName: "BoolItemField");

            migrationBuilder.RenameIndex(
                name: "IX_StringItemFields_ItemId",
                table: "StringItemField",
                newName: "IX_StringItemField_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_UserId",
                table: "Item",
                newName: "IX_Item_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Items_CollectionId",
                table: "Item",
                newName: "IX_Item_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_IntegerItemFields_ItemId",
                table: "IntegerItemField",
                newName: "IX_IntegerItemField_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_DatetimeItemFields_ItemId",
                table: "DatetimeItemField",
                newName: "IX_DatetimeItemField_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_UserId",
                table: "Comment",
                newName: "IX_Comment_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_Comments_ItemId",
                table: "Comment",
                newName: "IX_Comment_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_Collections_UserId",
                table: "Collection",
                newName: "IX_Collection_UserId");

            migrationBuilder.RenameIndex(
                name: "IX_CollectionFields_CollectionId",
                table: "CollectionField",
                newName: "IX_CollectionField_CollectionId");

            migrationBuilder.RenameIndex(
                name: "IX_BoolItemFields_ItemId",
                table: "BoolItemField",
                newName: "IX_BoolItemField_ItemId");

            migrationBuilder.AddUniqueConstraint(
                name: "AK_Tag_Name",
                table: "Tag",
                column: "Name");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Tag",
                table: "Tag",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_StringItemField",
                table: "StringItemField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Item",
                table: "Item",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_IntegerItemField",
                table: "IntegerItemField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_DatetimeItemField",
                table: "DatetimeItemField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Comment",
                table: "Comment",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_Collection",
                table: "Collection",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_CollectionField",
                table: "CollectionField",
                column: "Id");

            migrationBuilder.AddPrimaryKey(
                name: "PK_BoolItemField",
                table: "BoolItemField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolItemField_Item_ItemId",
                table: "BoolItemField",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collection_AspNetUsers_UserId",
                table: "Collection",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_CollectionField_Collection_CollectionId",
                table: "CollectionField",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_AspNetUsers_UserId",
                table: "Comment",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Comment_Item_ItemId",
                table: "Comment",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_DatetimeItemField_Item_ItemId",
                table: "DatetimeItemField",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_IntegerItemField_Item_ItemId",
                table: "IntegerItemField",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Item_AspNetUsers_UserId",
                table: "Item",
                column: "UserId",
                principalTable: "AspNetUsers",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_Item_Collection_CollectionId",
                table: "Item",
                column: "CollectionId",
                principalTable: "Collection",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTag_Item_ItemsId",
                table: "ItemTag",
                column: "ItemsId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_ItemTag_Tag_TagsId",
                table: "ItemTag",
                column: "TagsId",
                principalTable: "Tag",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_StringItemField_Item_ItemId",
                table: "StringItemField",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemField_Item_ItemId",
                table: "TextItemField",
                column: "ItemId",
                principalTable: "Item",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
