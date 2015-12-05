using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class Species : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
            migrationBuilder.CreateTable(
                name: "Species",
                columns: table => new
                {
                    Id = table.Column<Guid>(nullable: false),
                    Description = table.Column<string>(nullable: true),
                    Name = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Species", x => x.Id);
                });
            migrationBuilder.AddColumn<Guid>(
                name: "SpeciesId",
                table: "Character",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Planet_HomePlanetId",
                table: "Character",
                column: "HomePlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Species_SpeciesId",
                table: "Character",
                column: "SpeciesId",
                principalTable: "Species",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
            migrationBuilder.DropForeignKey(name: "FK_Character_Species_SpeciesId", table: "Character");
            migrationBuilder.DropColumn(name: "SpeciesId", table: "Character");
            migrationBuilder.DropTable("Species");
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
