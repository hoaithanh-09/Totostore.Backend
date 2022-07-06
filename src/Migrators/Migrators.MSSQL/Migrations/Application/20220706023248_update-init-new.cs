using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updateinitnew : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices",
                column: "CouponId",
                principalSchema: "Catalog",
                principalTable: "Coupons",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices",
                column: "CouponId",
                principalSchema: "Catalog",
                principalTable: "Coupons",
                principalColumn: "Id");
        }
    }
}
