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

        // Tela de login
        public IActionResult Entrar()
        {
            ViewData["ShowContribuinteNav"] = false; // Define para não mostrar o item de navegação
            return View();
        }

        // Método para autenticar
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
                // Armazena o ID do mensageiro na sessão
                HttpContext.Session.SetString("MensageiroId", mensageiro.Id.ToString());

                // Redireciona para a página de boletos
                return RedirectToAction("Index", "Boleto");
            }
            else
            {
                TempData["MensagemErro"] = "Número de celular ou senha incorretos.";
                return View();
            }
        }

        // Tela de cadastro
        public IActionResult Cadastrar()
        {
            return View();
        }

        // Método para cadastrar novo usuário
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Cadastrar(Mensageiro mensageiro)
        {
            if (ModelState.IsValid)
            {
                // Verifica se o telefone já está cadastrado
                var telefoneExistente = await _context.Mensageiros
                    .FirstOrDefaultAsync(m => m.Telefone == mensageiro.Telefone);

                if (telefoneExistente == null)
                {
                    // Salva o novo mensageiro no banco de dados
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
                DataPrevista = DateTime.Now, // Define data e hora atual como padrão
                DataRecebimento = DateTime.Now // Define data e hora atual como padrão
            };

            return View(model);
        }

        // POST: Processa o envio dos dados da contribuição
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Contribuicao(Contribuicao contribuicao)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    // Adiciona a nova contribuição ao banco de dados
                    _context.Contribuicoes.Add(contribuicao);
                    await _context.SaveChangesAsync();

                    // Redireciona para uma página de sucesso
                    return RedirectToAction("ContribuicaoSucesso");
                }
                catch (Exception ex)
                {
                    // Adiciona uma mensagem de erro ao ModelState se ocorrer um problema
                    ModelState.AddModelError("", "Não foi possível salvar a contribuição. Erro: " + ex.Message);
                }
            }

            // Se o modelo não for válido, retorna à view com os erros
            return View(contribuicao);
        }

        // GET: Exibe a página de confirmação após o envio da contribuição
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










