using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Forum.Migrations
{
    /// <inheritdoc />
    public partial class AddGroupReferenceToThread : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_ThreadGroups_ThreadGroupId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_ThreadGroupId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "ThreadGroupId",
                table: "Threads");

            migrationBuilder.AddColumn<int>(
                name: "GroupId",
                table: "Threads",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_GroupId",
                table: "Threads",
                column: "GroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_ThreadGroups_GroupId",
                table: "Threads",
                column: "GroupId",
                principalTable: "ThreadGroups",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Threads_ThreadGroups_GroupId",
                table: "Threads");

            migrationBuilder.DropIndex(
                name: "IX_Threads_GroupId",
                table: "Threads");

            migrationBuilder.DropColumn(
                name: "GroupId",
                table: "Threads");

            migrationBuilder.AddColumn<int>(
                name: "ThreadGroupId",
                table: "Threads",
                type: "int",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Threads_ThreadGroupId",
                table: "Threads",
                column: "ThreadGroupId");

            migrationBuilder.AddForeignKey(
                name: "FK_Threads_ThreadGroups_ThreadGroupId",
                table: "Threads",
                column: "ThreadGroupId",
                principalTable: "ThreadGroups",
                principalColumn: "Id");
        }
    }
}
