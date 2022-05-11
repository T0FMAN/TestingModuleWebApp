using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingModuleWebApp.Migrations
{
    public partial class AddTutorField : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Tutor",
                table: "AspNetUsers",
                type: "TEXT",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Tutor",
                table: "AspNetUsers");
        }
    }
}
