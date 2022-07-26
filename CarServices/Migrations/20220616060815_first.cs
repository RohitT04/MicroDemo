using Microsoft.EntityFrameworkCore.Migrations;

namespace CarServices.Migrations
{
    public partial class first : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "cars",
                columns: table => new
                {
                    carid = table.Column<string>(nullable: false),
                    carname = table.Column<string>(nullable: true),
                    cartype = table.Column<string>(nullable: true),
                    carbrand = table.Column<string>(nullable: true),
                    carAvg = table.Column<string>(nullable: true), 
                    carMax = table.Column<string>(nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_cars", x => x.carid);
                });
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "cars");
        }
    }
}
