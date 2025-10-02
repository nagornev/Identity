using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Otp.Persistence.Migrations
{
    /// <inheritdoc />
    public partial class ChannelNamesChanged : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "Type",
                table: "OneTimePasswords",
                newName: "ChannelType");

            migrationBuilder.RenameColumn(
                name: "Channel",
                table: "OneTimePasswords",
                newName: "ChannelValue");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "ChannelValue",
                table: "OneTimePasswords",
                newName: "Channel");

            migrationBuilder.RenameColumn(
                name: "ChannelType",
                table: "OneTimePasswords",
                newName: "Type");
        }
    }
}
