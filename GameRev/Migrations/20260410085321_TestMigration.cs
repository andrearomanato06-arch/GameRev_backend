using System;
using Microsoft.EntityFrameworkCore.Metadata;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace GameRev.Migrations
{
    /// <inheritdoc />
    public partial class TestMigration : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "JwtToken");

            migrationBuilder.DropColumn(
                name: "TokenId",
                table: "Users");

            migrationBuilder.AddColumn<bool>(
                name: "released",
                table: "Videogames",
                type: "tinyint(1)",
                nullable: false,
                defaultValue: true);

            migrationBuilder.AddColumn<long>(
                name: "Jtid",
                table: "Users",
                type: "bigint",
                nullable: true);

            migrationBuilder.AlterColumn<double>(
                name: "score",
                table: "Reviews",
                type: "double",
                nullable: false,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.CreateTable(
                name: "UserSessions",
                columns: table => new
                {
                    jwt_token_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    IssuedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    UserId = table.Column<long>(type: "bigint", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_UserSessions", x => x.jwt_token_id);
                    table.ForeignKey(
                        name: "FK_UserSessions_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_Platforms_name",
                table: "Platforms",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_Authors_name",
                table: "Authors",
                column: "name",
                unique: true);

            migrationBuilder.CreateIndex(
                name: "IX_UserSessions_UserId",
                table: "UserSessions",
                column: "UserId",
                unique: true);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "UserSessions");

            migrationBuilder.DropIndex(
                name: "IX_Platforms_name",
                table: "Platforms");

            migrationBuilder.DropIndex(
                name: "IX_Authors_name",
                table: "Authors");

            migrationBuilder.DropColumn(
                name: "released",
                table: "Videogames");

            migrationBuilder.DropColumn(
                name: "Jtid",
                table: "Users");

            migrationBuilder.AddColumn<long>(
                name: "TokenId",
                table: "Users",
                type: "bigint",
                nullable: false,
                defaultValue: 0L);

            migrationBuilder.AlterColumn<int>(
                name: "score",
                table: "Reviews",
                type: "int",
                nullable: false,
                oldClrType: typeof(double),
                oldType: "double");

            migrationBuilder.CreateTable(
                name: "JwtToken",
                columns: table => new
                {
                    jwt_token_id = table.Column<long>(type: "bigint", nullable: false)
                        .Annotation("MySql:ValueGenerationStrategy", MySqlValueGenerationStrategy.IdentityColumn),
                    UserId = table.Column<long>(type: "bigint", nullable: false),
                    IssuedAt = table.Column<DateTime>(type: "datetime(6)", nullable: false),
                    token = table.Column<string>(type: "longtext", nullable: false)
                        .Annotation("MySql:CharSet", "utf8mb4")
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_JwtToken", x => x.jwt_token_id);
                    table.ForeignKey(
                        name: "FK_JwtToken_Users_UserId",
                        column: x => x.UserId,
                        principalTable: "Users",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                })
                .Annotation("MySql:CharSet", "utf8mb4");

            migrationBuilder.CreateIndex(
                name: "IX_JwtToken_UserId",
                table: "JwtToken",
                column: "UserId",
                unique: true);
        }
    }
}
