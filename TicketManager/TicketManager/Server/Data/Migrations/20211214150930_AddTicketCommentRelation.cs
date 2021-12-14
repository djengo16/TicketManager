using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManager.Server.Data.Migrations
{
    public partial class AddTicketCommentRelation : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Tickets");

            migrationBuilder.DropColumn(
                name: "ImgUrl",
                table: "Comments");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Tickets",
                type: "nvarchar(max)",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "ImgUrl",
                table: "Comments",
                type: "nvarchar(max)",
                nullable: true);
        }
    }
}
