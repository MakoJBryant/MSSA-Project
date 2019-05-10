using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class CorrectDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePosted",
                table: "Videos",
                nullable: true,
                defaultValue: "05/09/2019 00:00:00",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "05/09/2019 10:27:28");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePosted",
                table: "Videos",
                nullable: true,
                defaultValue: "05/09/2019 10:27:28",
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "05/09/2019 00:00:00");
        }
    }
}
