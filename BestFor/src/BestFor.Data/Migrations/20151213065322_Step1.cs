using System;
using System.Collections.Generic;
using Microsoft.Data.Entity.Migrations;

namespace BestFor.Data.Migrations
{
    public partial class Step1 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(name: "Key", table: "Answer");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<string>(
                name: "Key",
                table: "Answer",
                nullable: true);
        }
    }
}
