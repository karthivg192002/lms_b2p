using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace iucs.lms.domain.Migrations
{
    /// <inheritdoc />
    public partial class SecondMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<bool>(
                name: "email_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<DateTime>(
                name: "last_login_at",
                table: "users",
                type: "timestamp with time zone",
                nullable: true);

            migrationBuilder.AddColumn<bool>(
                name: "phone_verified",
                table: "users",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<string>(
                name: "profile_image",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<string>(
                name: "username",
                table: "users",
                type: "text",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<bool>(
                name: "can_create",
                table: "role_menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "can_delete",
                table: "role_menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "can_read",
                table: "role_menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "can_update",
                table: "role_menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<bool>(
                name: "is_visible",
                table: "menus",
                type: "boolean",
                nullable: false,
                defaultValue: false);

            migrationBuilder.AddColumn<int>(
                name: "sequence",
                table: "menus",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "email_verified",
                table: "users");

            migrationBuilder.DropColumn(
                name: "last_login_at",
                table: "users");

            migrationBuilder.DropColumn(
                name: "phone_verified",
                table: "users");

            migrationBuilder.DropColumn(
                name: "profile_image",
                table: "users");

            migrationBuilder.DropColumn(
                name: "username",
                table: "users");

            migrationBuilder.DropColumn(
                name: "can_create",
                table: "role_menus");

            migrationBuilder.DropColumn(
                name: "can_delete",
                table: "role_menus");

            migrationBuilder.DropColumn(
                name: "can_read",
                table: "role_menus");

            migrationBuilder.DropColumn(
                name: "can_update",
                table: "role_menus");

            migrationBuilder.DropColumn(
                name: "is_visible",
                table: "menus");

            migrationBuilder.DropColumn(
                name: "sequence",
                table: "menus");
        }
    }
}
