using AppStore.Models.Domain;
using AppStore.Repositories.Abstract;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Controllers;

[Authorize]
public class LibroController : Controller
{
    private readonly ILibroservice _libroService;
    private readonly IFileService _fileService;
    private readonly ICategoriaService _categoriaService;

    public LibroController(ILibroservice libroService, IFileService fileService, ICategoriaService categoriaService)
    {
        _libroService = libroService;
        _fileService = fileService;
        _categoriaService = categoriaService;
    }

    [HttpPost]
    public IActionResult Add(Libro libro)
    {
        libro.CategoriaLista = _categoriaService.List()
                            .Select(a => new SelectListItem {Text = a.Nombre, Value = a.Id.ToString()});
        
        if (!ModelState.IsValid)
        {
            return View(libro);

        }

        if (libro.ImageFile != null)
        {
            var resultado = _fileService.SaveImage(libro.ImageFile);
            if (resultado.Item1 == 0)
            {
                TempData["msg"] = "La imagen no pudo guardarse exitosamente";
                return View(libro);
            }

            var imagenName = resultado.Item2;
            libro.Imagen = imagenName;
        }

        var resultadoLibro = _libroService.Insertar(libro);
        if (resultadoLibro)
        {
            TempData["msg"] = "Se agrego el libro exitosamente";
            return RedirectToAction(nameof(Add));
        }

        TempData["msg"] = "Errores guardando el libro";
        return View(libro);
    }

    public IActionResult Add()
    {
        var libro = new Libro();
        libro.CategoriaLista = _categoriaService.List()
                            .Select(a => new SelectListItem {Text = a.Nombre, Value = a.Id.ToString()});
        
        return View(libro);
    }

    public IActionResult Edit(int id)
    {
        var libro = _libroService.ObtenerId(id);
        var categoriasLibro = _libroService.ObtenerCategoriaPorLibroId(id);
        var multiSelectListCategorias = new MultiSelectList(_categoriaService.List(), "Id", "Nombre", categoriasLibro);

        libro.MultiCategoriaList = multiSelectListCategorias;
        return View(libro);
    }

    [HttpPost]
     public IActionResult Edit(Libro libro)
    {
        var categoriasLibro = _libroService.ObtenerCategoriaPorLibroId(libro.Id);
        var multiSelectListCategorias = new MultiSelectList(_categoriaService.List(), "Id", "Nombre", categoriasLibro);

        libro.MultiCategoriaList = multiSelectListCategorias;

        if (!ModelState.IsValid)
        {
            return View(libro);
        }

        if (libro.ImageFile != null)
        {
            var fileResultado = _fileService.SaveImage(libro.ImageFile);
            if (fileResultado.Item1 == 0)
            {
                TempData["msg"] = "La imagen no fue guardada";
                return View(libro);

            }

            var imagenName = fileResultado.Item2;
            libro.Imagen = imagenName;
        }

        var resultadoLibro = _libroService.Actualizar(libro);
        if (!resultadoLibro)
        {
            TempData["msg"] = "Errores, no se pudo actualizar el libro";
            return View(libro);
        }

        TempData["msg"] = "Se actualizo correctamente el libro";
        return View(libro);
        
    }

    public IActionResult LibroList(string term = "", int currentPage = 1)
    {
        var libros = _libroService.Listar(term, true, currentPage);
        return View(libros);
    }

    public IActionResult Delete(int id)
    {
        _libroService.Eliminar(id);
        return RedirectToAction(nameof(LibroList));
    }
}