using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class A02 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<string>(
                name: "Category",
                table: "Answers",
                nullable: true);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AlterColumn<bool>(
                name: "Category",
                table: "Answers",
                nullable: false);
        }
    }
}
