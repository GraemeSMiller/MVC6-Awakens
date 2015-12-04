using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class AddedRenameHomePlanetId : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_PlanetId", table: "Character");
            migrationBuilder.RenameColumn(name: "PlanetId", newName: "HomePlanetId", table: "Character");
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
            migrationBuilder.RenameColumn(name: "HomePlanetId", newName: "PlanetId", table: "Character");
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Planet_PlanetId",
                table: "Character",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
