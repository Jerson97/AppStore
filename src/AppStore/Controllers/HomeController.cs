using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers;

public class HomeController : Controller
{
    private readonly ILibroservice _libroService;
    public HomeController(ILibroservice libroService)
    {
        _libroService = libroService;
    }
    public IActionResult Index(string term="", int currentPage =1)
    {
        var libros = _libroService.Listar(term, true, currentPage);

        return View(libros);
    }

    public IActionResult LibroDetail(int libroId)
    {
        var libro = _libroService.ObtenerId(libroId);
        return View(libro);
    }

    public IActionResult About()
    {
        return View();
    }
}