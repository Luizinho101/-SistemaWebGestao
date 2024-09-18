using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SistemaWebGestao.Data.ApplicationDbContext;
using SistemaWebGestao.Models;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace SistemaWebGestao.Controllers
{
    public class ContaController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ContaController(ApplicationDbContext context)
        {
            _context = context;
        }

       
        public IActionResult Entrar()
        {
            ViewData["ShowContribuinteNav"] = false; 
            return View();
        }

    
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Entrar(string telefone, string senha)
        {
            if (string.IsNullOrEmpty(telefone) || string.IsNullOrEmpty(senha))
            {
                TempData["MensagemErro"] = "Número de celular e senha são obrigatórios.";
                return View();
            }

            var mensageiro = await _context.Mensageiros
                .FirstOrDefaultAsync(m => m.Telefone == telefone && m.Senha == senha);

            if (mensageiro != null)
            {
                
                HttpContext.Session.SetString("MensageiroId", mensageiro.Id.ToString());

                
                return RedirectToAction("Index", "Boleto");
            }
            else
            {
                TempData["MensagemErro"] = "Número de celular ou senha incorretos.";
                return View();
            }
        }

  
        public IActionResult Cadastrar()
        {
            return View();
        }

       
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Mensageiro mensageiro)
        {
            if (ModelState.IsValid)
            {
               
                var telefoneExistente = await _context.Mensageiros
                    .FirstOrDefaultAsync(m => m.Telefone == mensageiro.Telefone);

                if (telefoneExistente == null)
                {
                   
                    _context.Add(mensageiro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Entrar");
                }
                else
                {
                    TempData["MensagemErro"] = "Este número de celular já está cadastrado.";
                }
            }

            return View(mensageiro);
        }

       

       
        public IActionResult Contribuicao()
        {
            var model = new Contribuicao
            {
                DataPrevista = DateTime.Now, 
                DataRecebimento = DateTime.Now 
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contribuicao(Contribuicao contribuicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.Contribuicoes.Add(contribuicao);
                    await _context.SaveChangesAsync();

                  
                    return RedirectToAction("ContribuicaoSucesso");
                }
                catch (Exception ex)
                {
                   
                    ModelState.AddModelError("", "Não foi possível salvar a contribuição. Erro: " + ex.Message);
                }
            }

          
            return View(contribuicao);
        }

      
        public IActionResult ContribuicaoConfirmacao()
        {
            return View();
        }

        public IActionResult ContribuicaoSucesso()
        {
            return View();
        }
    }
}










