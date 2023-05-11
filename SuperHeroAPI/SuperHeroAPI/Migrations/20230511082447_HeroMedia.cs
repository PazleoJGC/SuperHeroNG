using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SuperHeroAPI.Migrations
{
    /// <inheritdoc />
    public partial class HeroMedia : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "HeroMedia",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Type = table.Column<int>(type: "int", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    ReleaseDate = table.Column<DateTime>(type: "datetime2", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroMedia", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "HeroMediaSuperHero",
                columns: table => new
                {
                    CharactersId = table.Column<int>(type: "int", nullable: false),
                    MediaId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_HeroMediaSuperHero", x => new { x.CharactersId, x.MediaId });
                    table.ForeignKey(
                        name: "FK_HeroMediaSuperHero_HeroMedia_MediaId",
                        column: x => x.MediaId,
                        principalTable: "HeroMedia",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_HeroMediaSuperHero_SuperHeroes_CharactersId",
                        column: x => x.CharactersId,
                        principalTable: "SuperHeroes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_HeroMediaSuperHero_MediaId",
                table: "HeroMediaSuperHero",
                column: "MediaId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "HeroMediaSuperHero");

            migrationBuilder.DropTable(
                name: "HeroMedia");
        }
    }
}
