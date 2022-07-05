using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Migrators.MSSQL.Migrations.Application
{
    public partial class updateproductprice : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_OrderProducts_ProductPrices_ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts");

            migrationBuilder.DropTable(
                name: "Brands",
                schema: "Catalog");

            migrationBuilder.DropIndex(
                name: "IX_OrderProducts_ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts");

            migrationBuilder.DropColumn(
                name: "ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts");

            migrationBuilder.AddColumn<Guid>(
                name: "CouponId",
                schema: "Catalog",
                table: "ProductPrices",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.AddColumn<Guid>(
                name: "UserId",
                schema: "Catalog",
                table: "Customers",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateIndex(
                name: "IX_ProductPrices_CouponId",
                schema: "Catalog",
                table: "ProductPrices",
                column: "CouponId");

            migrationBuilder.AddForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices",
                column: "CouponId",
                principalSchema: "Catalog",
                principalTable: "Coupons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_ProductPrices_Coupons_CouponId",
                schema: "Catalog",
                table: "ProductPrices");

            migrationBuilder.DropIndex(
                name: "IX_ProductPrices_CouponId",
                schema: "Catalog",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "CouponId",
                schema: "Catalog",
                table: "ProductPrices");

            migrationBuilder.DropColumn(
                name: "UserId",
                schema: "Catalog",
                table: "Customers");

            migrationBuilder.AddColumn<Guid>(
                name: "ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts",
                type: "uniqueidentifier",
                nullable: false,
                defaultValue: new Guid("00000000-0000-0000-0000-000000000000"));

            migrationBuilder.CreateTable(
                name: "Brands",
                schema: "Catalog",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    CreatedOn = table.Column<DateTime>(type: "datetime2", nullable: false),
                    DeletedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: true),
                    DeletedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: true),
                    LastModifiedBy = table.Column<Guid>(type: "uniqueidentifier", nullable: false),
                    LastModifiedOn = table.Column<DateTime>(type: "datetime2", nullable: true),
                    Name = table.Column<string>(type: "nvarchar(256)", maxLength: 256, nullable: false),
                    TenantId = table.Column<string>(type: "nvarchar(64)", maxLength: 64, nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Brands", x => x.Id);
                });

            migrationBuilder.CreateIndex(
                name: "IX_OrderProducts_ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts",
                column: "ProductPriceId");

            migrationBuilder.AddForeignKey(
                name: "FK_OrderProducts_ProductPrices_ProductPriceId",
                schema: "Catalog",
                table: "OrderProducts",
                column: "ProductPriceId",
                principalSchema: "Catalog",
                principalTable: "ProductPrices",
                principalColumn: "Id");
        }
    }
}
