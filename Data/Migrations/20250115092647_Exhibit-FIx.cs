using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nicopolis_Ad_Istrum.Data.Migrations
{
    /// <inheritdoc />
    public partial class ExhibitFIx : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Origin",
                table: "Exhibits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "PhotoFileName",
                table: "Exhibits",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Origin",
                table: "Exhibits");

            migrationBuilder.DropColumn(
                name: "PhotoFileName",
                table: "Exhibits");
        }
    }
}
