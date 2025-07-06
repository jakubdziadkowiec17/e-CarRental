using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace e_CarRentalAPI.Database.Migrations
{
    /// <inheritdoc />
    public partial class Migration3 : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "isArchive",
                table: "Car",
                newName: "IsArchive");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "IsArchive",
                table: "Car",
                newName: "isArchive");
        }
    }
}
