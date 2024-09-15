using Microsoft.AspNetCore.Mvc;
using SistemaWebGestao.Models;
using SistemaWebGestao.Data.ApplicationDbContext;
using System.Linq;

namespace SistemaWebGestao.Controllers
{
    [Route("[controller]")]
    public class ContribuintesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContribuintesController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Exibe o formulário de login
        public IActionResult LoginContribuinte()
        {
            return View();
        }

        // POST: Processa o login do contribuinte
        [HttpPost]
        public IActionResult LoginContribuinte(int id, string telefone)
        {
            var contribuinte = _context.Contribuintes
                .FirstOrDefault(c => c.Id == id && c.Telefone == telefone);

            if (contribuinte != null)
            {
                // Redireciona para a página de preenchimento da contribuição
                return RedirectToAction("PreencherContribuicao", "Contribuicoes", new { contribuinteId = contribuinte.Id });
            }

            // Se houver erro, mostra uma mensagem de erro
            TempData["MensagemErro"] = "ID ou telefone inválidos.";
            return View();
        }
    }
}



