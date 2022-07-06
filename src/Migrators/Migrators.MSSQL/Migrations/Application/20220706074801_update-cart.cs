using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updatecart : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_ApplicationUserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_ApplicationUserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Carts_CustomerId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "ApplicationUserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.DropColumn(
                name: "CustomerId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.AlterColumn<string>(
                name: "UserId",
                schema: "Catalog",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: false,
                oldClrType: typeof(Guid),
                oldType: "uniqueidentifier");

            migrationBuilder.AddColumn<string>(
                name: "UserId",
                schema: "Catalog",
                table: "Carts",
                type: "nvarchar(450)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.CreateIndex(
                name: "IX_Customers_UserId",
                schema: "Catalog",
                table: "Customers",
                column: "UserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_UserId",
                schema: "Catalog",
                table: "Carts",
                column: "UserId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Users_UserId",
                schema: "Catalog",
                table: "Carts",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_UserId",
                schema: "Catalog",
                table: "Customers",
                column: "UserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Carts_Users_UserId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.DropForeignKey(
                name: "FK_Customers_Users_UserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Customers_UserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.DropIndex(
                name: "IX_Carts_UserId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Catalog",
                table: "Carts");

            migrationBuilder.AlterColumn<Guid>(
                name: "UserId",
                schema: "Catalog",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                oldClrType: typeof(string),
                oldType: "nvarchar(450)");

            migrationBuilder.AddColumn<string>(
                name: "ApplicationUserId",
                schema: "Catalog",
                table: "Customers",
                type: "nvarchar(450)",
                nullable: true);

            migrationBuilder.AddColumn<Guid>(
                name: "CustomerId",
                schema: "Catalog",
                table: "Carts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_Customers_ApplicationUserId",
                schema: "Catalog",
                table: "Customers",
                column: "ApplicationUserId");

            migrationBuilder.CreateIndex(
                name: "IX_Carts_CustomerId",
                schema: "Catalog",
                table: "Carts",
                column: "CustomerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Carts_Customers_CustomerId",
                schema: "Catalog",
                table: "Carts",
                column: "CustomerId",
                principalSchema: "Catalog",
                principalTable: "Customers",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Customers_Users_ApplicationUserId",
                schema: "Catalog",
                table: "Customers",
                column: "ApplicationUserId",
                principalSchema: "Identity",
                principalTable: "Users",
                principalColumn: "Id");
        }
    }
}
