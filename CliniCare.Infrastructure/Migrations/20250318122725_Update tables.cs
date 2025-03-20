using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CliniCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Updatetables : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "SchedulingId",
                table: "VeterinaryCares",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.CreateIndex(
                name: "IX_VeterinaryCares_SchedulingId",
                table: "VeterinaryCares",
                column: "SchedulingId");

            migrationBuilder.AddForeignKey(
                name: "FK_VeterinaryCares_Schedulings_SchedulingId",
                table: "VeterinaryCares",
                column: "SchedulingId",
                principalTable: "Schedulings",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_VeterinaryCares_Schedulings_SchedulingId",
                table: "VeterinaryCares");

            migrationBuilder.DropIndex(
                name: "IX_VeterinaryCares_SchedulingId",
                table: "VeterinaryCares");

            migrationBuilder.DropColumn(
                name: "SchedulingId",
                table: "VeterinaryCares");
        }
    }
}
