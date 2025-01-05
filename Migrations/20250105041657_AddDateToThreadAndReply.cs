using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddDateToThreadAndReply : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Message",
                table: "ThreadReplies");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "Threads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastEdited",
                table: "Threads",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<string>(
                name: "Content",
                table: "ThreadReplies",
                type: "nvarchar(max)",
                maxLength: 5000,
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<DateTime>(
                name: "DateCreated",
                table: "ThreadReplies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DateLastEdited",
                table: "ThreadReplies",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "DateLastEdited",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Content",
                table: "ThreadReplies");

            migrationBuilder.DropColumn(
                name: "DateCreated",
                table: "ThreadReplies");

            migrationBuilder.DropColumn(
                name: "DateLastEdited",
                table: "ThreadReplies");

            migrationBuilder.AddColumn<string>(
                name: "Message",
                table: "ThreadReplies",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");
        }
    }
}
