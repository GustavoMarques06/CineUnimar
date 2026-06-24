using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Api_Venda_Ingressos.Migrations
{
    /// <inheritdoc />
    public partial class SecurityFixes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<double>(
                name: "price",
                table: "events",
                type: "float",
                nullable: false,
                defaultValue: 0.0);

            migrationBuilder.AddColumn<byte[]>(
                name: "row_version",
                table: "chairs_in_event",
                type: "rowversion",
                rowVersion: true,
                nullable: false,
                defaultValue: new byte[0]);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "price",
                table: "events");

            migrationBuilder.DropColumn(
                name: "row_version",
                table: "chairs_in_event");
        }
    }
}
