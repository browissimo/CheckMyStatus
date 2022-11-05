using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CheckMyStatus.Migrations
{
    public partial class i2 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "LocalStatus",
                table: "UserRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "RemoteStatus",
                table: "UserRequests",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "LocalStatus",
                table: "UserRequests");

            migrationBuilder.DropColumn(
                name: "RemoteStatus",
                table: "UserRequests");
        }
    }
}
