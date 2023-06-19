using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Frame.Apps.TargetBoard.Data.Migrations
{
    public partial class initial12 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aspects_MainBoards_MainBoardId",
                table: "Aspects");

            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Aspects_AspectId",
                table: "Targets");

            migrationBuilder.AddForeignKey(
                name: "FK_Aspects_MainBoards_MainBoardId",
                table: "Aspects",
                column: "MainBoardId",
                principalTable: "MainBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Aspects_AspectId",
                table: "Targets",
                column: "AspectId",
                principalTable: "Aspects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Aspects_MainBoards_MainBoardId",
                table: "Aspects");

            migrationBuilder.DropForeignKey(
                name: "FK_Targets_Aspects_AspectId",
                table: "Targets");

            migrationBuilder.AddForeignKey(
                name: "FK_Aspects_MainBoards_MainBoardId",
                table: "Aspects",
                column: "MainBoardId",
                principalTable: "MainBoards",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);

            migrationBuilder.AddForeignKey(
                name: "FK_Targets_Aspects_AspectId",
                table: "Targets",
                column: "AspectId",
                principalTable: "Aspects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
