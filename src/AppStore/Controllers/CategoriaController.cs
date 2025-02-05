using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Mvc;

namespace AppStore.Controllers;

public class CategoriaController : Controller
{
    private readonly ILibroservice _libroService;
    private readonly IFileService _fileService;
    private readonly ICategoriaService _categoriaService;

    public CategoriaController(ILibroservice libroService, IFileService fileService, ICategoriaService categoriaService)
    {
        _libroService = libroService;
        _fileService = fileService;
        _categoriaService = categoriaService;
    }

    [HttpPost]
    public IActionResult Add(Categoria categoria)
    {

        if (!ModelState.IsValid)
        {
            return View(categoria);

        }

        var resultadoCategoria = _categoriaService.Insertar(categoria);
        if (resultadoCategoria)
        {
            TempData["msg"] = "Se agrego la categoria exitosamente";
            return RedirectToAction(nameof(Add));
        }

        TempData["msg"] = "Errores guardando la categoria";
        return View(categoria);
    }

    public IActionResult Add()
    {
        return View();
    }

     public IActionResult CategoriaList()
    {
        var categoria = _categoriaService.Listar();
        return View(categoria);
    }

    public IActionResult Edit(int id)
    {
        var libro = _categoriaService.ObtenerId(id);
        return View(libro);
    }

    [HttpPost]
     public IActionResult Edit(Categoria categoria)
    {

        if (!ModelState.IsValid)
        {
            return View(categoria);
        }


        var resultadoCategoria = _categoriaService.Actualizar(categoria);
        if (!resultadoCategoria)
        {
            TempData["msg"] = "Errores, no se pudo actualizar la categoria";
            return View(categoria);
        }

        TempData["msg"] = "Se actualizo correctamente la categoria";
        return View(categoria);
        
    }

    public IActionResult Delete(int id)
    {
        _categoriaService.Eliminar(id);
        return RedirectToAction(nameof(CategoriaList));
    }


}