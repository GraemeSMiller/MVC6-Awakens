using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class MoviesTable : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
            migrationBuilder.CreateTable(
                name: "Movie",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    ReleaseDate = table.Column<DateTime>(nullable: false),
                    Title = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Movie", x => x.Id);
                });
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Planet_HomePlanetId",
                table: "Character",
                column: "HomePlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
            migrationBuilder.DropTable("Movie");
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Planet_HomePlanetId",
                table: "Character",
                column: "HomePlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
