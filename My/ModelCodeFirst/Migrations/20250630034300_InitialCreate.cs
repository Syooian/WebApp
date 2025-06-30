using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace ModelCodeFirst.Migrations
{
    /// <inheritdoc />
    public partial class InitialCreate : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Book",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    Title = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Photo = table.Column<string>(type: "nvarchar(max)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Book", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReBook",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    BookID = table.Column<string>(type: "nvarchar(450)", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_ReBook", x => x.ID);
                    table.ForeignKey(
                        name: "FK_ReBook_Book_BookID",
                        column: x => x.BookID,
                        principalTable: "Book",
                        principalColumn: "ID");
                });

            migrationBuilder.CreateIndex(
                name: "IX_ReBook_BookID",
                table: "ReBook",
                column: "BookID");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "ReBook");

            migrationBuilder.DropTable(
                name: "Book");
        }
    }
}
