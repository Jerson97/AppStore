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
        var model = _libroService.Listar(term, paging: true, currentPage: currentPage);
        return View(model);
    }

    public IActionResult LibroDetail(int libroId)
    {
        var libro = _libroService.ObtenerId(libroId);
        if (libro == null) return NotFound();

        return View(libro);
    }

    public IActionResult About()
    {
        return View();
    }
}