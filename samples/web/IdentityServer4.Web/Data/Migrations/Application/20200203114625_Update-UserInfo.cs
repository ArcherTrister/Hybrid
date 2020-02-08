using System;
using Microsoft.EntityFrameworkCore.Migrations;

namespace IdentityServer4.Web.Data.Migrations.Application
{
    public partial class UpdateUserInfo : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("3a95c920-d49a-4e63-b944-af95d694e221"));

            migrationBuilder.AddColumn<int>(
                name: "Gender",
                table: "User",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "IsLocked", "RoleId", "UserId" },
                values: new object[] { new Guid("9bb55628-8a8e-4e89-8ccd-d12de128cba8"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"), new Guid("8d86feea-83d5-4a0c-9733-305ac6cfe58d") });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DeleteData(
                table: "UserRole",
                keyColumn: "Id",
                keyValue: new Guid("9bb55628-8a8e-4e89-8ccd-d12de128cba8"));

            migrationBuilder.DropColumn(
                name: "Gender",
                table: "User");

            migrationBuilder.InsertData(
                table: "UserRole",
                columns: new[] { "Id", "CreatedTime", "IsDeleted", "IsLocked", "RoleId", "UserId" },
                values: new object[] { new Guid("3a95c920-d49a-4e63-b944-af95d694e221"), new DateTime(1970, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified), false, false, new Guid("4f10b9ba-2391-4eb2-a378-aaf3012fb2d3"), new Guid("8d86feea-83d5-4a0c-9733-305ac6cfe58d") });
        }
    }
}
