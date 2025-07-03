using System;
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
                    ID = table.Column<string>(type: "varchar(36)", unicode: false, maxLength: 36, nullable: false, defaultValue: "False"),
                    Title = table.Column<string>(type: "nvarchar(50)", maxLength: 50, nullable: false),
                    Photo = table.Column<string>(type: "varchar(40)", unicode: false, maxLength: 40, nullable: true),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(20)", maxLength: 20, nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_BookID", x => x.ID);
                });

            migrationBuilder.CreateTable(
                name: "ReBook",
                columns: table => new
                {
                    ID = table.Column<string>(type: "nvarchar(450)", nullable: false),
                    ReID = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Description = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Author = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    CreatedDate = table.Column<DateTime>(type: "datetime2", nullable: false),
                    BookID = table.Column<string>(type: "varchar(36)", nullable: true)
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
