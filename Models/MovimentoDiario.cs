using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaWebGestao.Models
{
    public class MovimentoDiario
    {
        public int Id { get; set; }
        
        public DateTime DataMovimento { get; set; }
        
        public int? MensageiroId { get; set; }
        public Mensageiro Mensageiro { get; set; }
        
        public int ContribuicaoId { get; set; }
        public Contribuicao Contribuicao { get; set; }
        
        public int ContribuinteId { get; set; }
        public Contribuinte Contribuinte { get; set; }
        
        public int ReciboId { get; set; }
        public String Recibo { get; set; }
        
        public decimal Valor { get; set; }
        
        public int TipoPagamentoId { get; set; }
        public TiposPagamento TipoPagamento { get; set; }
        
        public DateTime DataPrevista { get; set; }
        
        public string Status { get; set; }
        
        public DateTime? DataRecebimento { get; set; }
    }
}