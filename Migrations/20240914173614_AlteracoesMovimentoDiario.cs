using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaWebGestao.Migrations
{
    /// <inheritdoc />
    public partial class AlteracoesMovimentoDiario : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.AddColumn<int>(
                name: "ContribuicaoId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<int>(
                name: "ContribuinteId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<DateTime>(
                name: "DataPrevista",
                table: "MovimentoDiarios",
                type: "datetime2",
                nullable: false,
                defaultValue: new DateTime(1, 1, 1, 0, 0, 0, 0, DateTimeKind.Unspecified));

            migrationBuilder.AddColumn<DateTime>(
                name: "DataRecebimento",
                table: "MovimentoDiarios",
                type: "datetime2",
                nullable: true);

            migrationBuilder.AddColumn<string>(
                name: "Recibo",
                table: "MovimentoDiarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "ReciboId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<string>(
                name: "Status",
                table: "MovimentoDiarios",
                type: "nvarchar(max)",
                nullable: false,
                defaultValue: "");

            migrationBuilder.AddColumn<int>(
                name: "TipoPagamentoId",
                table: "MovimentoDiarios",
                type: "int",
                nullable: false,
                defaultValue: 0);

            migrationBuilder.AddColumn<decimal>(
                name: "Valor",
                table: "MovimentoDiarios",
                type: "decimal(18,2)",
                nullable: false,
                defaultValue: 0m);

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoDiarios_ContribuicaoId",
                table: "MovimentoDiarios",
                column: "ContribuicaoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoDiarios_ContribuinteId",
                table: "MovimentoDiarios",
                column: "ContribuinteId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoDiarios_TipoPagamentoId",
                table: "MovimentoDiarios",
                column: "TipoPagamentoId");

             migrationBuilder.AddForeignKey(
            name: "FK_MovimentoDiarios_Contribuicoes_ContribuicaoId",
            table: "MovimentoDiarios",
            column: "ContribuicaoId",
            principalTable: "Contribuicoes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict); // Altere para Restrict ou NoAction

        // Ajustar a constraint de ContribuinteId
        migrationBuilder.AddForeignKey(
            name: "FK_MovimentoDiarios_Contribuintes_ContribuinteId",
            table: "MovimentoDiarios",
            column: "ContribuinteId",
            principalTable: "Contribuintes",
            principalColumn: "Id",
            onDelete: ReferentialAction.Restrict); // Altere para Restrict ou NoAction

        // Ajustar a constraint de TipoPagamentoId
        migrationBuilder.AddForeignKey(
            name: "FK_MovimentoDiarios_TiposPagamentos_TipoPagamentoId",
            table: "MovimentoDiarios",
            column: "TipoPagamentoId",
            principalTable: "TiposPagamentos",
            principalColumn: "Id",
            onDelete: ReferentialAction.Cascade);
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoDiarios_Contribuicoes_ContribuicaoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoDiarios_Contribuintes_ContribuinteId",
                table: "MovimentoDiarios");

            migrationBuilder.DropForeignKey(
                name: "FK_MovimentoDiarios_TiposPagamentos_TipoPagamentoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropIndex(
                name: "IX_MovimentoDiarios_ContribuicaoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropIndex(
                name: "IX_MovimentoDiarios_ContribuinteId",
                table: "MovimentoDiarios");

            migrationBuilder.DropIndex(
                name: "IX_MovimentoDiarios_TipoPagamentoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "ContribuicaoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "ContribuinteId",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "DataPrevista",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "DataRecebimento",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "Recibo",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "ReciboId",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "Status",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "TipoPagamentoId",
                table: "MovimentoDiarios");

            migrationBuilder.DropColumn(
                name: "Valor",
                table: "MovimentoDiarios");
        }
    }
}
