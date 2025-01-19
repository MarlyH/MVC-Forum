using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddReplyVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "ThreadReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "ThreadReplies",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ReplyVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ReplyId = table.Column<int>(type: "int", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReplyVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ReplyVote_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ReplyVote_ThreadReplies_ReplyId",
                        column: x => x.ReplyId,
                        principalTable: "ThreadReplies",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReplyVote_ReplyId",
                table: "ReplyVote",
                column: "ReplyId");

            migrationBuilder.CreateIndex(
                name: "IX_ReplyVote_UserId",
                table: "ReplyVote",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReplyVote");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "ThreadReplies");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "ThreadReplies");
        }
    }
}
