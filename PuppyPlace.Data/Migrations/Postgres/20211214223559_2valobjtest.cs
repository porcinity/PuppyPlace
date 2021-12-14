using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyPlace.Data.Migrations.Postgres
{
    public partial class _2valobjtest : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Dogs_Person_OwnerId",
                table: "Dogs");

            migrationBuilder.DropTable(
                name: "Person");

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

            migrationBuilder.CreateTable(
                name: "Person",
                columns: table => new
                {
                    Id = table.Column<Guid>(type: "uuid", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Person", x => x.Id);
                });

            migrationBuilder.AddForeignKey(
                name: "FK_Dogs_Person_OwnerId",
                table: "Dogs",
                column: "OwnerId",
                principalTable: "Person",
                principalColumn: "Id");
        }
    }
}
