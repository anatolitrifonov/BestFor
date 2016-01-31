using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BestFor.Data.Migrations
{
    public partial class M8 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "AnswerId",
                table: "AnswerDescription",
                nullable: false,
                defaultValue: 0);
            migrationBuilder.AddForeignKey(
                name: "FK_AnswerDescription_Answer_AnswerId",
                table: "AnswerDescription",
                column: "AnswerId",
                principalTable: "Answer",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(name: "FK_AnswerDescription_Answer_AnswerId", table: "AnswerDescription");
            migrationBuilder.DropColumn(name: "AnswerId", table: "AnswerDescription");
        }
    }
}
