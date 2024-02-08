using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Financify.Migrations
{
    public partial class Category : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Category",
                table: "Transactions",
                newName: "CategoryName");

            migrationBuilder.CreateTable(
                name: "Categories",
                columns: table => new
                {
                    Name = table.Column<string>(type: "TEXT", nullable: false),
                    ColorHEX = table.Column<string>(type: "TEXT", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Categories", x => x.Name);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Transactions_CategoryName",
                table: "Transactions",
                column: "CategoryName");

            migrationBuilder.AddForeignKey(
                name: "FK_Transactions_Categories_CategoryName",
                table: "Transactions",
                column: "CategoryName",
                principalTable: "Categories",
                principalColumn: "Name",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Transactions_Categories_CategoryName",
                table: "Transactions");

            migrationBuilder.DropTable(
                name: "Categories");

            migrationBuilder.DropIndex(
                name: "IX_Transactions_CategoryName",
                table: "Transactions");

            migrationBuilder.RenameColumn(
                name: "CategoryName",
                table: "Transactions",
                newName: "Category");
        }
    }
}
