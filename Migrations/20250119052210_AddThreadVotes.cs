using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddThreadVotes : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Downvotes",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "Upvotes",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateTable(
                name: "ThreadVote",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    UserId = table.Column<string>(type: "nvarchar(450)", nullable: true),
                    ThreadId = table.Column<int>(type: "int", nullable: false),
                    VoteType = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ThreadVote", x => x.Id);
                    table.ForeignKey(
                        name: "FK_ThreadVote_AspNetUsers_UserId",
                        column: x => x.UserId,
                        principalTable: "AspNetUsers",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_ThreadVote_Threads_ThreadId",
                        column: x => x.ThreadId,
                        principalTable: "Threads",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_ThreadVote_ThreadId",
                table: "ThreadVote",
                column: "ThreadId");

            migrationBuilder.CreateIndex(
                name: "IX_ThreadVote_UserId",
                table: "ThreadVote",
                column: "UserId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ThreadVote");

            migrationBuilder.DropColumn(
                name: "Downvotes",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "Upvotes",
                table: "Threads");
        }
    }
}
