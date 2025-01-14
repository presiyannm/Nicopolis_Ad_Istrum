using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Nicopolis_Ad_Istrum.Data.Migrations
{
    /// <inheritdoc />
    public partial class Updated_Collection : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "EraId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "LocationId",
                table: "Collections",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Collections_EraId",
                table: "Collections",
                column: "EraId");

            migrationBuilder.CreateIndex(
                name: "IX_Collections_LocationId",
                table: "Collections",
                column: "LocationId");

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Eras_EraId",
                table: "Collections",
                column: "EraId",
                principalTable: "Eras",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Collections_Locations_LocationId",
                table: "Collections",
                column: "LocationId",
                principalTable: "Locations",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Eras_EraId",
                table: "Collections");

            migrationBuilder.DropForeignKey(
                name: "FK_Collections_Locations_LocationId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_EraId",
                table: "Collections");

            migrationBuilder.DropIndex(
                name: "IX_Collections_LocationId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "EraId",
                table: "Collections");

            migrationBuilder.DropColumn(
                name: "LocationId",
                table: "Collections");
        }
    }
}
