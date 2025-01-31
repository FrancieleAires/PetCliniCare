using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace CliniCare.Infrastructure.Migrations
{
    /// <inheritdoc />
    public partial class Inicial : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_VeterinaryProcedures_ServiceId",
                table: "Schedulings");

            migrationBuilder.RenameColumn(
                name: "ServiceId",
                table: "Schedulings",
                newName: "ProcedureId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedulings_ServiceId",
                table: "Schedulings",
                newName: "IX_Schedulings_ProcedureId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_VeterinaryProcedures_ProcedureId",
                table: "Schedulings",
                column: "ProcedureId",
                principalTable: "VeterinaryProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_Schedulings_VeterinaryProcedures_ProcedureId",
                table: "Schedulings");

            migrationBuilder.RenameColumn(
                name: "ProcedureId",
                table: "Schedulings",
                newName: "ServiceId");

            migrationBuilder.RenameIndex(
                name: "IX_Schedulings_ProcedureId",
                table: "Schedulings",
                newName: "IX_Schedulings_ServiceId");

            migrationBuilder.AddForeignKey(
                name: "FK_Schedulings_VeterinaryProcedures_ServiceId",
                table: "Schedulings",
                column: "ServiceId",
                principalTable: "VeterinaryProcedures",
                principalColumn: "Id",
                onDelete: ReferentialAction.Restrict);
        }
    }
}
