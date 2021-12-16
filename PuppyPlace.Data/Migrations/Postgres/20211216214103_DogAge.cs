using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace PuppyPlace.Data.Migrations.Postgres
{
    public partial class DogAge : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropColumn(
                name: "Age",
                table: "Dogs");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "Age",
                table: "Dogs",
                type: "integer",
                nullable: false,
                defaultValue: 0);
        }
    }
}
