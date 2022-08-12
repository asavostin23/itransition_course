using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Course.Data.Migrations
{
    public partial class AddedCollectionItemFieldIdInItemFields : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "CollectionFieldId",
                table: "TextItemField",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CollectionFieldId",
                table: "StringItemFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CollectionFieldId",
                table: "IntegerItemFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CollectionFieldId",
                table: "DatetimeItemFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "CollectionFieldId",
                table: "BoolItemFields",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_TextItemField_CollectionFieldId",
                table: "TextItemField",
                column: "CollectionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_StringItemFields_CollectionFieldId",
                table: "StringItemFields",
                column: "CollectionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_IntegerItemFields_CollectionFieldId",
                table: "IntegerItemFields",
                column: "CollectionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_DatetimeItemFields_CollectionFieldId",
                table: "DatetimeItemFields",
                column: "CollectionFieldId");

            migrationBuilder.CreateIndex(
                name: "IX_BoolItemFields_CollectionFieldId",
                table: "BoolItemFields",
                column: "CollectionFieldId");

            migrationBuilder.AddForeignKey(
                name: "FK_BoolItemFields_CollectionFields_CollectionFieldId",
                table: "BoolItemFields",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_DatetimeItemFields_CollectionFields_CollectionFieldId",
                table: "DatetimeItemFields",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_IntegerItemFields_CollectionFields_CollectionFieldId",
                table: "IntegerItemFields",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_StringItemFields_CollectionFields_CollectionFieldId",
                table: "StringItemFields",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");

            migrationBuilder.AddForeignKey(
                name: "FK_TextItemField_CollectionFields_CollectionFieldId",
                table: "TextItemField",
                column: "CollectionFieldId",
                principalTable: "CollectionFields",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_BoolItemFields_CollectionFields_CollectionFieldId",
                table: "BoolItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_DatetimeItemFields_CollectionFields_CollectionFieldId",
                table: "DatetimeItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_IntegerItemFields_CollectionFields_CollectionFieldId",
                table: "IntegerItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_StringItemFields_CollectionFields_CollectionFieldId",
                table: "StringItemFields");

            migrationBuilder.DropForeignKey(
                name: "FK_TextItemField_CollectionFields_CollectionFieldId",
                table: "TextItemField");

            migrationBuilder.DropIndex(
                name: "IX_TextItemField_CollectionFieldId",
                table: "TextItemField");

            migrationBuilder.DropIndex(
                name: "IX_StringItemFields_CollectionFieldId",
                table: "StringItemFields");

            migrationBuilder.DropIndex(
                name: "IX_IntegerItemFields_CollectionFieldId",
                table: "IntegerItemFields");

            migrationBuilder.DropIndex(
                name: "IX_DatetimeItemFields_CollectionFieldId",
                table: "DatetimeItemFields");

            migrationBuilder.DropIndex(
                name: "IX_BoolItemFields_CollectionFieldId",
                table: "BoolItemFields");

            migrationBuilder.DropColumn(
                name: "CollectionFieldId",
                table: "TextItemField");

            migrationBuilder.DropColumn(
                name: "CollectionFieldId",
                table: "StringItemFields");

            migrationBuilder.DropColumn(
                name: "CollectionFieldId",
                table: "IntegerItemFields");

            migrationBuilder.DropColumn(
                name: "CollectionFieldId",
                table: "DatetimeItemFields");

            migrationBuilder.DropColumn(
                name: "CollectionFieldId",
                table: "BoolItemFields");
        }
    }
}
