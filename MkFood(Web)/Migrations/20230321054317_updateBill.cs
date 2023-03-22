using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace MkFoodWeb.Migrations
{
    /// <inheritdoc />
    public partial class updateBill : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "Cost",
                table: "Bills",
                type: "float",
                nullable: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Cost",
                table: "Bills");
        }
    }
}
