using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Data.Migrations
{
    public partial class TextItemFieldRenamedToTextItemFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextItemField_CollectionFields_CollectionFieldId",
                table: "TextItemField");

            migrationBuilder.DropForeignKey(
                name: "FK_TextItemField_Items_ItemId",
                table: "TextItemField");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextItemField",
                table: "TextItemField");

            migrationBuilder.RenameTable(
                name: "TextItemField",
                newName: "TextItemFields");

            migrationBuilder.RenameIndex(
                name: "IX_TextItemField_ItemId",
                table: "TextItemFields",
                newName: "IX_TextItemFields_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TextItemField_CollectionFieldId",
                table: "TextItemFields",
                newName: "IX_TextItemFields_CollectionFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextItemFields",
                table: "TextItemFields",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemFields_CollectionFields_CollectionFieldId",
                table: "TextItemFields",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemFields_Items_ItemId",
                table: "TextItemFields",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_TextItemFields_CollectionFields_CollectionFieldId",
                table: "TextItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_TextItemFields_Items_ItemId",
                table: "TextItemFields");

            migrationBuilder.DropPrimaryKey(
                name: "PK_TextItemFields",
                table: "TextItemFields");

            migrationBuilder.RenameTable(
                name: "TextItemFields",
                newName: "TextItemField");

            migrationBuilder.RenameIndex(
                name: "IX_TextItemFields_ItemId",
                table: "TextItemField",
                newName: "IX_TextItemField_ItemId");

            migrationBuilder.RenameIndex(
                name: "IX_TextItemFields_CollectionFieldId",
                table: "TextItemField",
                newName: "IX_TextItemField_CollectionFieldId");

            migrationBuilder.AddPrimaryKey(
                name: "PK_TextItemField",
                table: "TextItemField",
                column: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemField_CollectionFields_CollectionFieldId",
                table: "TextItemField",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemField_Items_ItemId",
                table: "TextItemField",
                column: "ItemId",
                principalTable: "Items",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
