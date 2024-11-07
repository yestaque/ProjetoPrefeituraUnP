using Microsoft.AspNetCore.Mvc;
using CadastroPrefeitura.Models;

namespace NomeDoSeuProjeto.Controllers
{
    public class PrefeituraController : Controller
    {
        [HttpGet]
        public IActionResult Cadastro()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Cadastro(PrefeituraModel model)
        {
            if (ModelState.IsValid)
            {
                // Adicione a lógica para salvar no banco de dados ou em outro local
                TempData["SuccessMessage"] = "Cadastro realizado com sucesso!";
                return RedirectToAction("Cadastro");
            }

            return View(model);
        }
    }
}
