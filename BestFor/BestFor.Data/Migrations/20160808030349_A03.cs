using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore.Migrations;
using System.Diagnostics.CodeAnalysis;

namespace BestFor.Data.Migrations
{
    [ExcludeFromCodeCoverage]
    public partial class A03 : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "NumberOfAnswers",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfComments",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfDescriptions",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfFlags",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "NumberOfVotes",
                table: "AspNetUsers",
                nullable: false,
                defaultValue: 0);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "NumberOfAnswers",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfComments",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfDescriptions",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfFlags",
                table: "AspNetUsers");

            migrationBuilder.DropColumn(
                name: "NumberOfVotes",
                table: "AspNetUsers");
        }
    }
}
