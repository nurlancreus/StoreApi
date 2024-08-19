using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_5_adding_new_files_tables_and_relations_between_them_and_product : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct");

            migrationBuilder.RenameColumn(
                name: "CategoryId",
                table: "CategoryProduct",
                newName: "CategoriesId");

            migrationBuilder.CreateTable(
                name: "AppFiles",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    Extension = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    UpdatedAt = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Discriminator = table.Column<string>(type: "nvarchar(21)", maxLength: 21, nullable: false),
                    IsMain = table.Column<bool>(type: "bit", nullable: true),
                    ImageUrl = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    ProductId = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    IsDeleted = table.Column<bool>(type: "bit", nullable: false),
                    CreatedAt = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedAt = table.Column<DateTime>(type: "datetime2", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_AppFiles", x => x.Id);
                    table.ForeignKey(
                        name: "FK_AppFiles_Products_ProductId",
                        column: x => x.ProductId,
                        principalTable: "Products",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_AppFiles_ProductId",
                table: "AppFiles",
                column: "ProductId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct",
                column: "CategoriesId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_CategoryProduct_Categories_CategoriesId",
                table: "CategoryProduct");

            migrationBuilder.DropTable(
                name: "AppFiles");

            migrationBuilder.RenameColumn(
                name: "CategoriesId",
                table: "CategoryProduct",
                newName: "CategoryId");

            migrationBuilder.AddForeignKey(
                name: "FK_CategoryProduct_Categories_CategoryId",
                table: "CategoryProduct",
                column: "CategoryId",
                principalTable: "Categories",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
