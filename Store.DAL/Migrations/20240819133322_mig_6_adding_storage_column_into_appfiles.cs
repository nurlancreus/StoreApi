using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_6_adding_storage_column_into_appfiles : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Storage",
                table: "AppFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Storage",
                table: "AppFiles");
        }
    }
}
