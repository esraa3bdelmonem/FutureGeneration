using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace Future_Generation.Migrations
{
    /// <inheritdoc />
    public partial class Validationphone : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropIndex(
                name: "IX_Students_Telephone",
                table: "Students");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateIndex(
                name: "IX_Students_Telephone",
                table: "Students",
                column: "Telephone",
                unique: true);
        }
    }
}
