using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class MissingFK : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_HomePlanetId", table: "Character");
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
