using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Store.DAL.Migrations
{
    /// <inheritdoc />
    public partial class mig_7_add_filename_column : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "UpdatedAt",
                table: "AppFiles");

            migrationBuilder.AddColumn<string>(
                name: "FileName",
                table: "AppFiles",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "FileName",
                table: "AppFiles");

            migrationBuilder.AddColumn<DateTime>(
                name: "UpdatedAt",
                table: "AppFiles",
                type: "datetime2",
                nullable: true);
        }
    }
}
