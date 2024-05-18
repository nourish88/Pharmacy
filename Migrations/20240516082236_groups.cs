using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Pharmacy.Migrations
{
    /// <inheritdoc />
    public partial class groups : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Medicines_GroupId",
                table: "Medicines",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Medicines_Groups_GroupId",
                table: "Medicines",
                column: "GroupId",
                principalTable: "Groups",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Medicines_Groups_GroupId",
                table: "Medicines");

            migrationBuilder.DropIndex(
                name: "IX_Medicines_GroupId",
                table: "Medicines");
        }
    }
}
