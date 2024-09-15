using System;
using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace SistemaWebGestao.Migrations
{
    /// <inheritdoc />
    public partial class AddNewTable : Migration
    {
        /// <inheritdoc />
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.CreateTable(
                name: "Contribuintes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Endereco = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Telefone = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribuintes", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "MovimentoDiarios",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    DataMovimento = table.Column<DateTime>(type: "datetime2", nullable: false),
                    MensageiroId = table.Column<int>(type: "int", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_MovimentoDiarios", x => x.Id);
                    table.ForeignKey(
                        name: "FK_MovimentoDiarios_Mensageiros_MensageiroId",
                        column: x => x.MensageiroId,
                        principalTable: "Mensageiros",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateTable(
                name: "TiposPagamentos",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Nome = table.Column<string>(type: "nvarchar(max)", nullable: false)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_TiposPagamentos", x => x.Id);
                });

            migrationBuilder.CreateTable(
                name: "Contribuicoes",
                columns: table => new
                {
                    Id = table.Column<int>(type: "int", nullable: false)
                        .Annotation("SqlServer:Identity", "1, 1"),
                    Recibo = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    Valor = table.Column<decimal>(type: "decimal(18,2)", nullable: false),
                    DataPrevista = table.Column<DateTime>(type: "datetime2", nullable: false),
                    Status = table.Column<string>(type: "nvarchar(max)", nullable: false),
                    DataRecebimento = table.Column<DateTime>(type: "datetime2", nullable: true),
                    ContribuinteId = table.Column<int>(type: "int", nullable: false),
                    TipoPagamentoId = table.Column<int>(type: "int", nullable: false),
                    MensageiroId = table.Column<int>(type: "int", nullable: true)
                },
                constraints: table =>
                {
                    table.PrimaryKey("PK_Contribuicoes", x => x.Id);
                    table.ForeignKey(
                        name: "FK_Contribuicoes_Contribuintes_ContribuinteId",
                        column: x => x.ContribuinteId,
                        principalTable: "Contribuintes",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                    table.ForeignKey(
                        name: "FK_Contribuicoes_Mensageiros_MensageiroId",
                        column: x => x.MensageiroId,
                        principalTable: "Mensageiros",
                        principalColumn: "Id");
                    table.ForeignKey(
                        name: "FK_Contribuicoes_TiposPagamentos_TipoPagamentoId",
                        column: x => x.TipoPagamentoId,
                        principalTable: "TiposPagamentos",
                        principalColumn: "Id",
                        onDelete: ReferentialAction.Cascade);
                });

            migrationBuilder.CreateIndex(
                name: "IX_Contribuicoes_ContribuinteId",
                table: "Contribuicoes",
                column: "ContribuinteId");

            migrationBuilder.CreateIndex(
                name: "IX_Contribuicoes_MensageiroId",
                table: "Contribuicoes",
                column: "MensageiroId");

            migrationBuilder.CreateIndex(
                name: "IX_Contribuicoes_TipoPagamentoId",
                table: "Contribuicoes",
                column: "TipoPagamentoId");

            migrationBuilder.CreateIndex(
                name: "IX_MovimentoDiarios_MensageiroId",
                table: "MovimentoDiarios",
                column: "MensageiroId");
        }

        /// <inheritdoc />
        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.DropTable(
                name: "Contribuicoes");

            migrationBuilder.DropTable(
                name: "MovimentoDiarios");

            migrationBuilder.DropTable(
                name: "Contribuintes");

            migrationBuilder.DropTable(
                name: "TiposPagamentos");
        }
    }
}
