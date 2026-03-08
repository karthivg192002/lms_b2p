using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iucs.lms.domain.Migrations
{
    /// <inheritdoc />
    public partial class UserSessionAdded : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "is_revoked",
                table: "user_sessions",
                type: "boolean",
                nullable: false,
                defaultValue: false);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "is_revoked",
                table: "user_sessions");
        }
    }
}
