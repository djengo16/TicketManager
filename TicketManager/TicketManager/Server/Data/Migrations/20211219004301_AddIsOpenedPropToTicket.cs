using Microsoft.EntityFrameworkCore.Migrations;

namespace TicketManager.Server.Data.Migrations
{
    public partial class AddIsOpenedPropToTicket : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "IsOpened",
                table: "Tickets",
                type: "bit",
                nullable: false,
                defaultValue: false);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "IsOpened",
                table: "Tickets");
        }
    }
}
