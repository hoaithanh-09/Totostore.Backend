using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updatenotification : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Customers_CustomerId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_CustomerId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Catalog",
                table: "Notifications",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_UserId",
                schema: "Catalog",
                table: "Notifications",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Users_UserId",
                schema: "Catalog",
                table: "Notifications",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Users_UserId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_UserId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Catalog",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_CustomerId",
                schema: "Catalog",
                table: "Notifications",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Customers_CustomerId",
                schema: "Catalog",
                table: "Notifications",
                column: "CustomerId",
                principalSchema: "Catalog",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
