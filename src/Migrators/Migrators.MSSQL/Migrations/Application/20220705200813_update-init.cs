using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updateinit : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Notifications_Shippers_ShipperId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropIndex(
                name: "IX_Notifications_ShipperId",
                schema: "Catalog",
                table: "Notifications");

            migrationBuilder.DropColumn(
                name: "ShipperId",
                schema: "Catalog",
                table: "Notifications");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "ShipperId",
                schema: "Catalog",
                table: "Notifications",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Notifications_ShipperId",
                schema: "Catalog",
                table: "Notifications",
                column: "ShipperId");

            migrationBuilder.AddForeignKey(
                name: "FK_Notifications_Shippers_ShipperId",
                schema: "Catalog",
                table: "Notifications",
                column: "ShipperId",
                principalSchema: "Catalog",
                principalTable: "Shippers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
