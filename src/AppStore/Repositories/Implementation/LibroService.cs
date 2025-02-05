using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class LibroService : ILibroservice
{
    private readonly DatabaseContext _context;
    public LibroService(DatabaseContext context)
    {
        _context = context;
    }
    public bool Actualizar(Libro libro)
    {
        try
        {
            var categoriasparaEliminar = _context.LibrosCategorias!.Where(a => a.LibroId == libro.Id);
            foreach (var categoria in categoriasparaEliminar)
            {
                _context.LibrosCategorias!.Remove(categoria);
            }
            foreach (var categoriaId in libro.Categorias!)
            {
                var libroCategoria = new LibroCategoria { CategoriaId = categoriaId, LibroId = libro.Id};
                _context.LibrosCategorias!.Add(libroCategoria);
            }

            _context.Libros!.Update(libro);
            _context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            
            return false;
        }
    }

    public bool Eliminar(int id)
    {
        try
        {
            var data = ObtenerId(id);
            if (data is null)
            {
                return false;
            }

            var libroCategorias = _context.LibrosCategorias!.Where(a => a.LibroId == data.Id);
            _context.LibrosCategorias!.RemoveRange(libroCategorias);
            _context.Libros!.Remove(data);
            _context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            
            return false;
        }
    }

    public bool Insertar(Libro libro)
    {
        try
        {
            _context.Libros!.Add(libro);
            _context.SaveChanges();

            foreach (int categoriaId in libro.Categorias!)
            {
                var libroCategoria = new LibroCategoria
                {
                    LibroId = libro.Id,
                    CategoriaId = categoriaId
                };
                _context.LibrosCategorias!.Add(libroCategoria);
            }
            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            
            return false;
        }
    }

    public LibroListVm Listar(string term = "", bool paging = false, int currentPage = 0)
    {
        var data = new LibroListVm();
        var list = _context.Libros!.ToList();

        if (!string.IsNullOrEmpty(term))
        {
            term = term.ToLower();
            list = list.Where(a => a.Titulo!.ToLower().StartsWith(term)).ToList();
        }

        if (paging)
        {
            int pageSize = 5;
            int count = list.Count;
            int TotalPages = (int)Math.Ceiling(count/(double)pageSize);
            list = list.Skip((currentPage-1)*pageSize).Take(pageSize).ToList();
            data.PageSize = pageSize;
            data.Currentpage = currentPage;
            data.TotalPages = TotalPages;
        }
            foreach (var libro in list)
            {
                var categorias = (
                    from categoria in  _context.Categorias
                    join lc in _context.LibrosCategorias!
                    on categoria.Id equals lc.CategoriaId
                    where lc.LibroId == libro.Id
                    select categoria.Nombre
                ).ToList();
                string categoriaNombres = string.Join(",", categorias);
                libro.CategoriaNombre = categoriaNombres;
            }

            data.LibroList = list.AsQueryable();
             return data;
       
    }

    public List<int> ObtenerCategoriaPorLibroId(int libroId)
    {
        return _context.LibrosCategorias!.Where(a => a.LibroId == libroId).Select(a => a.CategoriaId).ToList();
    }

    public Libro ObtenerId(int id)
    {
       return _context.Libros!.Find(id)!;
    }
}