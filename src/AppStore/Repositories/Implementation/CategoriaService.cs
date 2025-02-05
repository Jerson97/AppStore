using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class CategoriaService : ICategoriaService
{
    private readonly DatabaseContext _context;

    public CategoriaService(DatabaseContext context)
    {
        _context = context;
    }

    public bool Actualizar(Categoria categoria)
    {
        try
        {
            var categoriasparaEliminar = _context.LibrosCategorias!.Where(a => a.LibroId == categoria.Id);


            _context.Categorias!.Update(categoria);
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

            _context.Categorias!.Remove(data);
            _context.SaveChanges();

            return true;
        }
        catch (Exception)
        {
            
            return false;
        }
    }

    public bool Insertar(Categoria categoria)
    {
        try
        {
            _context.Categorias!.Add(categoria);
            _context.SaveChanges();

            _context.SaveChanges();
            return true;
        }
        catch (Exception)
        {
            
            return false;
        }
    }

    public IQueryable<Categoria> List()
    {
        return _context.Categorias!.AsQueryable();
    }

    public CategoriaListVm Listar(string term = "", bool paging = false, int currentPage = 0)
    {
        var data = new CategoriaListVm();
        var list = _context.Categorias!.ToList();

        if (!string.IsNullOrEmpty(term))
        {
            term = term.ToLower();
            list = list.Where(a => a.Nombre!.ToLower().StartsWith(term)).ToList();
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
            data.CategoriaList = list.AsQueryable();
             return data;
    }

    public List<int> ObtenerCategoriaPorLibroId(int libroId)
    {
        throw new NotImplementedException();
    }

    public Categoria ObtenerId(int id)
    {
        return _context.Categorias!.Find(id)!;
    }
}