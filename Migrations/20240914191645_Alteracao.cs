using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaWebGestao.Migrations
{
    /// <inheritdoc />
    public partial class Alteracao : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoDiarios_Mensageiros_MensageiroId",
                table: "MovimentoDiarios");

            migrationBuilder.AlterColumn<int>(
                name: "MensageiroId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: true,
                oldClrType: typeof(int),
                oldType: "int");

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoDiarios_Mensageiros_MensageiroId",
                table: "MovimentoDiarios",
                column: "MensageiroId",
                principalTable: "Mensageiros",
                principalColumn: "Id");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoDiarios_Mensageiros_MensageiroId",
                table: "MovimentoDiarios");

            migrationBuilder.AlterColumn<int>(
                name: "MensageiroId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: false,
                defaultValue: 0,
                oldClrType: typeof(int),
                oldType: "int",
                oldNullable: true);

            migrationBuilder.AddForeignKey(
                name: "FK_MovimentoDiarios_Mensageiros_MensageiroId",
                table: "MovimentoDiarios",
                column: "MensageiroId",
                principalTable: "Mensageiros",
                principalColumn: "Id",
                onDelete: ReferentialAction.Cascade);
        }
    }
}
