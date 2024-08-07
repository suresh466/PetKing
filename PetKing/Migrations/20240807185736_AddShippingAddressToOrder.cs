using Microsoft.EntityFrameworkCore.Migrations;

namespace PetKing.Migrations
{
    public partial class AddShippingAddressToOrder : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ShippingAddress",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingCity",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingName",
                table: "Orders",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ShippingPostalCode",
                table: "Orders",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ShippingAddress",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingCity",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingName",
                table: "Orders");

            migrationBuilder.DropColumn(
                name: "ShippingPostalCode",
                table: "Orders");
        }
    }
}
