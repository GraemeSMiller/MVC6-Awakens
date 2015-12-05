using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class AddedCharacterCreator : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
            migrationBuilder.AddColumn<string>(
                name: "CreatorId",
                table: "Character",
                nullable: true);
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
            migrationBuilder.DropColumn(name: "CreatorId", table: "Character");
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
