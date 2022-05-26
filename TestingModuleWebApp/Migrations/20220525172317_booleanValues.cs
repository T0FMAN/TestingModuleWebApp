using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingModuleWebApp.Migrations
{
    public partial class booleanValues : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_A",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_Ek",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_Ek0",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_F",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_N",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_S",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_a0",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_p0",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_u",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_u0",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "is_x0",
                table: "PhysicTasks",
                type: "INTEGER",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_A",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_Ek",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_Ek0",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_F",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_N",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_S",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_a0",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_p0",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_u",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_u0",
                table: "PhysicTasks");

            migrationBuilder.DropColumn(
                name: "is_x0",
                table: "PhysicTasks");
        }
    }
}
