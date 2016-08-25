using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;

namespace BestFor.Data.Migrations
{
    public partial class A06 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Suggestions",
                nullable: false);

            migrationBuilder.CreateIndex(
                name: "IX_Suggestions_Phrase",
                table: "Suggestions",
                column: "Phrase",
                unique: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Suggestions_Phrase",
                table: "Suggestions");

            migrationBuilder.AlterColumn<string>(
                name: "Phrase",
                table: "Suggestions",
                nullable: false);
        }
    }
}
