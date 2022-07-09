using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updateOrderdelete : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                schema: "Catalog",
                table: "Orders");

            migrationBuilder.DropIndex(
                name: "IX_Orders_CouponId",
                schema: "Catalog",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "CouponId",
                schema: "Catalog",
                table: "Orders");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                schema: "Catalog",
                table: "Orders",
                type: "uniqueidentifier",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Orders_CouponId",
                schema: "Catalog",
                table: "Orders",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_Orders_Coupons_CouponId",
                schema: "Catalog",
                table: "Orders",
                column: "CouponId",
                principalSchema: "Catalog",
                principalTable: "Coupons",
                principalColumn: "Id");
        }
    }
}
