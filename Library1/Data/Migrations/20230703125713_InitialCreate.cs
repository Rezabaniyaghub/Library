using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Library1.Data.Migrations
{
    public partial class InitialCreate : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalNuber",
                table: "Books",
                newName: "TotalNumber");
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.RenameColumn(
                name: "TotalNumber",
                table: "Books",
                newName: "TotalNuber");
        }
    }
}
