using Microsoft.EntityFrameworkCore.Migrations;

namespace Project.Migrations
{
    public partial class setDate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePosted",
                table: "Videos",
                nullable: true,
                defaultValue: "05/09/2019 10:27:28",
                oldClrType: typeof(string),
                oldNullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "DatePosted",
                table: "Videos",
                nullable: true,
                oldClrType: typeof(string),
                oldNullable: true,
                oldDefaultValue: "05/09/2019 10:27:28");
        }
    }
}
