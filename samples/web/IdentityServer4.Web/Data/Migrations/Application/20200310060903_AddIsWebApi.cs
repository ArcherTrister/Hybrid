using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer4.Web.Data.Migrations.Application
{
    public partial class AddIsWebApi : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsWebApi",
                table: "Function",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsWebApi",
                table: "Function");
        }
    }
}
