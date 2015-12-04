using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace MVC6Awakens.Migrations.Domain
{
    public partial class AddedWeaponOfChoiceForCharacter : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_PlanetId", table: "Character");
            migrationBuilder.AddColumn<string>(
                name: "WeaponOfChoice",
                table: "Character",
                nullable: true);
            migrationBuilder.AddForeignKey(
                name: "FK_Character_Planet_PlanetId",
                table: "Character",
                column: "PlanetId",
                principalTable: "Planet",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_Character_Planet_PlanetId", table: "Character");
            migrationBuilder.DropColumn(name: "WeaponOfChoice", table: "Character");
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
