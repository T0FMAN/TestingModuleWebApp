using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TestingModuleWebApp.Migrations
{
    public partial class UpdUserFK_Tutors : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "INTEGER");

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers");

            migrationBuilder.AlterColumn<int>(
                name: "TutorId",
                table: "AspNetUsers",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "INTEGER",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_AspNetUsers_Tutors_TutorId",
                table: "AspNetUsers",
                column: "TutorId",
                principalTable: "Tutors",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
