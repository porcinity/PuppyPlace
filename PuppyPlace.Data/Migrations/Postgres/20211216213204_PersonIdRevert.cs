using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyPlace.Data.Migrations.Postgres
{
    public partial class PersonIdRevert : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Persons_OwnerId1",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_OwnerId1",
                table: "Dogs");

            migrationBuilder.DropColumn(
                name: "OwnerId1",
                table: "Dogs");

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_OwnerId",
                table: "Dogs",
                column: "OwnerId");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Persons_OwnerId",
                table: "Dogs",
                column: "OwnerId",
                principalTable: "Persons",
                principalColumn: "Id");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Persons_OwnerId",
                table: "Dogs");

            migrationBuilder.DropIndex(
                name: "IX_Dogs_OwnerId",
                table: "Dogs");

            migrationBuilder.AddColumn<Guid>(
                name: "OwnerId1",
                table: "Dogs",
                type: "uuid",
                nullable: true);

            migrationBuilder.CreateIndex(
                name: "IX_Dogs_OwnerId1",
                table: "Dogs",
                column: "OwnerId1");

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Persons_OwnerId1",
                table: "Dogs",
                column: "OwnerId1",
                principalTable: "Persons",
                principalColumn: "Id");
        }
    }
}
