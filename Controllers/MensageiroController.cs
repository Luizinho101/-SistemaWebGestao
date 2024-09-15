using System.Collections.Generic;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using SistemaWebGestao.Data; // Namespace para ApplicationDbContext
using SistemaWebGestao.Data.ApplicationDbContext;
using SistemaWebGestao.Models; // Namespace para o modelo Mensageiro

namespace SistemaWebGestao.Controllers
{
    public class MensageiroController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<MensageiroController> _logger;

        public MensageiroController(ApplicationDbContext context, ILogger<MensageiroController> logger)
        {
            _context = context;
            _logger = logger;
        }

        // Método para exibir a lista de mensageiros
        public async Task<IActionResult> Index()
        {
            IEnumerable<Mensageiro> mensageiros = await _context.Mensageiros.ToListAsync();
            return View(mensageiros);
        }

        // Método para exibir o formulário de criação
        public IActionResult Create()
        {
            return View();
        }

        // Método para gravar uma nova contribuição no banco de dados
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Mensageiro mensageiro)
        {
            if (ModelState.IsValid)
            {
                _context.Add(mensageiro);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Mensageiro criado com sucesso!";
                return RedirectToAction(nameof(Index));
            }
            return View(mensageiro);
        }
    }
}



