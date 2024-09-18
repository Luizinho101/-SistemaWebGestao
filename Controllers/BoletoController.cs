using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaWebGestao.Data.ApplicationDbContext;
using SistemaWebGestao.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaWebGestao.Controllers
{
    public class BoletoController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BoletoController(ApplicationDbContext context)
        {
            _context = context;
        }

        // Tela de boletos
        public async Task<IActionResult> Index()
        {
            var mensageiroId = HttpContext.Session.GetString("MensageiroId");

            if (string.IsNullOrEmpty(mensageiroId))
            {
                return RedirectToAction("Login", "Account");
            }

            var boletos = await _context.Contribuicoes
                .Include(c => c.Contribuinte)
                .Include(c => c.TipoPagamento)
                .Include(c => c.Mensageiro)
                .Where(c => c.MensageiroId == int.Parse(mensageiroId))
                .OrderByDescending(c => c.DataPrevista) // Ordena por data prevista, mais recente primeiro
                .ToListAsync();
            ViewData["ShowContribuinteNav"] = true;
            return View(boletos);
        }

        public IActionResult Detalhes(int id)
        {
            var boleto = _context.Contribuicoes
                .Include(c => c.Contribuinte)
                .Include(c => c.TipoPagamento)
                .FirstOrDefault(c => c.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }
            ViewData["ShowContribuinteNav"] = true;
            return View(boleto);
        }

        // GET: Exibe o formulário de preenchimento do boleto
        public IActionResult PreencherBoleto()
        {
            return View();
        }

        // POST: Recebe os dados do boleto preenchido
        [HttpPost]
        public IActionResult PreencherBoleto(Contribuicao model)
        {
            if (ModelState.IsValid)
            {
                _context.Contribuicoes.Add(model);
                _context.SaveChanges();

                return RedirectToAction("ConfirmacaoBoleto"); // Redirecionar após preencher
            }

            return View(model);
        }

        // GET: Exibe uma mensagem de confirmação após o envio do boleto
        public IActionResult ConfirmacaoBoleto()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cancelado(int id)
        {
            try
            {
                var boleto = _context.Contribuicoes.Find(id);

                if (boleto == null)
                {
                    return NotFound();
                }

                // Verifica se o status atual é "Pendente"
                if (boleto.Status == "Pendente")
                {
                    boleto.Status = "Cancelado";
                    _context.SaveChanges();
                }
                

                return RedirectToAction("Index");
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return BadRequest("Ocorreu um erro ao salvar as alterações.");
            }
        }


       
       [HttpPost]
        public IActionResult Recebido(int id)
        {
            // Buscar o boleto com base no ID
            var boleto = _context.Contribuicoes
                .Include(c => c.Contribuinte)
                .Include(c => c.TipoPagamento)
                .FirstOrDefault(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            // Verificar se o status atual é "Pendente"
            if (boleto.Status != "Pendente")
            {
                // Se o status não for "Pendente", simplesmente retornar sem executar a ação
                return RedirectToAction("Index");
            }

            // Criar um novo registro em MovimentoDiario
            var movimentoDiario = new MovimentoDiario
            {
                Recibo = boleto.Recibo,  // Certifique-se de que o Recibo está correto
                Valor = boleto.Valor,
                DataMovimento = DateTime.Now,
                Status = "Recebido",
                ContribuinteId = boleto.ContribuinteId,
                TipoPagamentoId = boleto.TipoPagamentoId,
                MensageiroId = boleto.MensageiroId,
                ContribuicaoId = boleto.Id  // Certifique-se de que o ContribuicaoId é correto
            };

            _context.MovimentoDiarios.Add(movimentoDiario);

            // Atualizar o status do boleto para 'Recebido'
            boleto.Status = "Recebido";

            // Salvar alterações no banco de dados
            _context.SaveChanges();

            // Redirecionar para uma página que mostra o comprovante do recibo
            return RedirectToAction("GerarComprovante", new { id = boleto.Id });
        }



        public IActionResult Comprovante(int id)
        {
            // Buscar o boleto novamente para gerar o comprovante
            var boleto = _context.Contribuicoes
                .Include(c => c.Contribuinte)
                .Include(c => c.TipoPagamento)
                .FirstOrDefault(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            // Retorna a visualização com os dados do boleto (comprovante)
            return View(boleto);
        }






        public IActionResult GerarComprovante(int id)
        {
            var boleto = _context.Contribuicoes
                                .Include(b => b.Contribuinte)
                                .Include(b => b.Mensageiro)
                                .FirstOrDefault(b => b.Id == id);

            if (boleto == null)
            {
                return NotFound();
            }

            return View(boleto);
        }

        
    public IActionResult FinalizarTrabalho(int mensageiroId)
    {
        var dataAtual = DateTime.Today;

        var dadosMovimentoDiario = _context.MovimentoDiarios
            .Where(m => m.DataMovimento.Date == dataAtual && m.MensageiroId == mensageiroId)
            .ToList();

        var extrato = dadosMovimentoDiario
            .GroupBy(m => m.Status)
            .Select(g => new 
            {
                Status = g.Key,
                Quantidade = g.Count(),
                ValorTotal = g.Sum(m => m.Valor)
            })
            .ToList();

        ViewBag.MensageiroId = mensageiroId;
        return View(extrato);
    }
    }
}



