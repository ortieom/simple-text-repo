using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace TextRepo.DataAccessLayer.Migrations
{
    /// <inheritdoc />
    public partial class DocumentsToProjectFix : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Projects_Id",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .Annotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddColumn<int>(
                name: "ProjectId",
                table: "Documents",
                type: "INTEGER",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_Documents_ProjectId",
                table: "Documents",
                column: "ProjectId");

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Projects_ProjectId",
                table: "Documents",
                column: "ProjectId",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Documents_Projects_ProjectId",
                table: "Documents");

            migrationBuilder.DropIndex(
                name: "IX_Documents_ProjectId",
                table: "Documents");

            migrationBuilder.DropColumn(
                name: "ProjectId",
                table: "Documents");

            migrationBuilder.AlterColumn<int>(
                name: "Id",
                table: "Documents",
                type: "INTEGER",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "INTEGER")
                .OldAnnotation("Sqlite:Autoincrement", true);

            migrationBuilder.AddForeignKey(
                name: "FK_Documents_Projects_Id",
                table: "Documents",
                column: "Id",
                principalTable: "Projects",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
