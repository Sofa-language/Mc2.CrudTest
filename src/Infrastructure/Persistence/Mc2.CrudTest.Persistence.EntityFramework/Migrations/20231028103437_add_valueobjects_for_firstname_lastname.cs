using Microsoft.EntityFrameworkCore.Migrations;

namespace Mc2.CrudTest.Persistence.EntityFramework.Migrations
{
    public partial class add_valueobjects_for_firstname_lastname : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Lastname",
                table: "Customer",
                newName: "LastName");

            migrationBuilder.RenameColumn(
                name: "Firstname",
                table: "Customer",
                newName: "FirstName");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "LastName",
                table: "Customer",
                newName: "Lastname");

            migrationBuilder.RenameColumn(
                name: "FirstName",
                table: "Customer",
                newName: "Firstname");
        }
    }
}
