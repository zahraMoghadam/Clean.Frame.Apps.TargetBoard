using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Clean.Frame.Apps.TargetBoard.Data.Migrations
{
    public partial class initial1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainBoards_Units_UnitId",
                table: "MainBoards");

            migrationBuilder.AddForeignKey(
                name: "FK_MainBoards_Units_UnitId",
                table: "MainBoards",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MainBoards_Units_UnitId",
                table: "MainBoards");

            migrationBuilder.AddForeignKey(
                name: "FK_MainBoards_Units_UnitId",
                table: "MainBoards",
                column: "UnitId",
                principalTable: "Units",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
