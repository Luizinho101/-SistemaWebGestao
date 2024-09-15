using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaWebGestao.Models
{
    public class Contribuicao
    {
        public int Id { get; set; }
        public string Recibo { get; set; }
        public decimal Valor { get; set; }
        public DateTime DataPrevista { get; set; }
        public string Status { get; set; } // Pendente, Recebido, Cancelado
        public DateTime? DataRecebimento { get; set; }
        public int ContribuinteId { get; set; }
        public Contribuinte Contribuinte { get; set; }
        public int TipoPagamentoId { get; set; }
        public TiposPagamento TipoPagamento { get; set; }
        public int? MensageiroId { get; set; }
        public Mensageiro Mensageiro { get; set; }
    }
}
