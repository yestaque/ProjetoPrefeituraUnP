using CadastroPrefeitura.Models; 
using Microsoft.AspNetCore.Mvc; 
using MongoDB.Driver; 

namespace CadastroPrefeitura.Controllers 
{ 
    public class CadastroController : Controller 
    { 
        private readonly IMongoCollection<Prefeitura> _prefeituraCollection;

        public CadastroController(IMongoDatabase database) 
        { 
            _prefeituraCollection = database.GetCollection<Prefeitura>("cadastrar.prefeitura"); 
        }

        public IActionResult Index() 
        { 
            var prefeituras = _prefeituraCollection.Find(_ => true).ToList(); 
            return View(prefeituras); 
        }

        [HttpPost] 
        public IActionResult Registrar(Prefeitura prefeitura) 
        { 
            _prefeituraCollection.InsertOne(prefeitura); 
            return RedirectToAction("Index"); 
        } 
    } 
}
