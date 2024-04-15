using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;
using LooneyTunes.Models;

namespace LooneyTunes.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;

    public HomeController(ILogger<HomeController> logger)
    {
        _logger = logger;
    }

    public IActionResult Index()
    {
        List<Personagens> personagens = GetPersonagens();
        List<Animais> animais = GetAnimais();
        ViewData["especies"] = animais;
        return View(personagens);
    }

    
    public IActionResult Details(int id)
    {
        List<Personagens> looneyTunes = GetPersonagens();
        List<Animais> animais = GetAnimais();
        DetailsVM details = new()
        {
            Animais = animais,
            Atual = looneyTunes.FirstOrDefault(p => p.Numero == id),
            Anterior = looneyTunes.OrderByDescending(p => p.Numero).FirstOrDefault(p => p.Numero < id),
            Proximo = looneyTunes.OrderBy(p => p.Numero).FirstOrDefault(p => p.Numero > id)
        };
        return View(details);
    }

    private List<Personagens> GetPersonagens()
    {
        using(StreamReader leitor = new ("Data\\personagens.json"))
        {
            string dados = leitor.ReadToEnd();
            return JsonSerializer.Deserialize<List<Personagens>>(dados);
        }
    }

    private List<Animais> GetAnimais()
    {
        using(StreamReader leitor = new("Data\\animais.json"))
        {
            string dados = leitor.ReadToEnd();
            return JsonSerializer.Deserialize<List<Animais>>(dados);
        }
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
