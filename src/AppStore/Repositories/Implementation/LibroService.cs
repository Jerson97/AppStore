using AppStore.Models.Domain;
using AppStore.Models.DTO;
using AppStore.Repositories.Abstract;

namespace AppStore.Repositories.Implementation;

public class LibroService : ILibroservice
{
    public bool Actualizar(Libro libro)
    {
        throw new NotImplementedException();
    }

    public bool Eliminar(int id)
    {
        throw new NotImplementedException();
    }

    public bool Insertar(Libro libro)
    {
        throw new NotImplementedException();
    }

    public LibroListVm Listar(string term = "", bool paging = false, int currentPage = 0)
    {
        throw new NotImplementedException();
    }

    public List<int> ObtenerCategoriaPorLibroId(int libroId)
    {
        throw new NotImplementedException();
    }

    public Libro ObtenerId(int id)
    {
        throw new NotImplementedException();
    }
}