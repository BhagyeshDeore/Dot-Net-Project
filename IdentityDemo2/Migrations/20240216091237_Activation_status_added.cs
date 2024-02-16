using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace IdentityDemo2.Migrations
{
    /// <inheritdoc />
    public partial class Activation_status_added : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "sTATUS",
                table: "AspNetUsers",
                type: "int",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "sTATUS",
                table: "AspNetUsers");
        }
    }
}
