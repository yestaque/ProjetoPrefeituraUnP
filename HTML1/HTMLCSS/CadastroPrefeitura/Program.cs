using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using MongoDB.Driver;
using CadastroPrefeitura.Models; // Adicione para usar o modelo Prefeitura

var builder = WebApplication.CreateBuilder(args);
builder.WebHost.UseUrls("https://localhost:5001", "http://localhost:5000");

// Adicione a configuração do MongoDB
builder.Services.AddSingleton<IMongoClient, MongoClient>(sp =>
{
    var config = sp.GetRequiredService<IConfiguration>();
    var connectionString = config.GetConnectionString("MongoDb");
    return new MongoClient(connectionString);
});

// Adicione o IMongoDatabase
builder.Services.AddSingleton(sp =>
{
    var client = sp.GetRequiredService<IMongoClient>();
    return client.GetDatabase("cadastrar"); // Nome do seu banco de dados
});

// Adicione o serviço para a coleção cadastrar.prefeitura
builder.Services.AddSingleton<MongoDBService>();

// Adiciona os serviços de controlador e visualização (MVC)
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configuração do pipeline de requisição HTTP
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Define a rota padrão
app.MapControllerRoute(
    name: "default",
    pattern: "{controller=Home}/{action=Index}/{id?}");

app.Run();

// Serviço MongoDB
public class MongoDBService
{
    private readonly IMongoCollection<Prefeitura> _prefeituraCollection;

    public MongoDBService(IMongoDatabase database)
    {
        // Usar a coleção 'cadastrar.prefeitura'
        _prefeituraCollection = database.GetCollection<Prefeitura>("cadastrar.prefeitura");
    }

    public async Task InserirPrefeituraAsync(Prefeitura prefeitura)
    {
        await _prefeituraCollection.InsertOneAsync(prefeitura);
    }
}

public class CadastroController : Controller
{
    private readonly MongoDBService _mongoDBService;

    // Injeta o serviço MongoDBService
    public CadastroController(MongoDBService mongoDBService)
    {
        _mongoDBService = mongoDBService;
    }

    // Método para registrar uma nova prefeitura
    [HttpPost]
    public async Task<IActionResult> Prosseguir(string CNPJ, string CNES, string Endereco, string Especialidades, string NumeroConvenio, string ResponsavelPrefeitura, string TelefoneResponsavel, string EmailResponsavel)
    {
        // Cria um objeto Prefeitura com os dados recebidos
        var prefeitura = new Prefeitura
        {
            CNPJ = CNPJ,
            CNES = CNES,
            Endereco = Endereco,
            Especialidades = Especialidades,
            NumeroConvenio = NumeroConvenio,
            ResponsavelPrefeitura = ResponsavelPrefeitura,
            TelefoneResponsavel = TelefoneResponsavel,
            EmailResponsavel = EmailResponsavel
        };

        // Chama o serviço MongoDB para inserir a prefeitura
        await _mongoDBService.InserirPrefeituraAsync(prefeitura);

        // Retorna uma resposta de sucesso
        return Ok("Cadastro realizado com sucesso!");
    }

}
