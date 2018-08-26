using Microsoft.EntityFrameworkCore.Migrations;

namespace OMX.Data.Migrations
{
    public partial class AddReprotss : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Reports",
                nullable: false,
                oldClrType: typeof(string),
                oldNullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Url",
                table: "Reports",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Url",
                table: "Reports");

            migrationBuilder.AlterColumn<string>(
                name: "Description",
                table: "Reports",
                nullable: true,
                oldClrType: typeof(string));
        }
    }
}
