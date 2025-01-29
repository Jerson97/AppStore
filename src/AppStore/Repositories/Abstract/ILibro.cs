using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract;

public interface ILibroservice
{
    bool Insertar(Libro libro);
    bool Actualizar(Libro libro);
    Libro ObtenerId(int id);
    bool Eliminar(int id);
    LibroListVm Listar(string term="", bool paging = false, int currentPage = 0);
    List<int> ObtenerCategoriaPorLibroId(int libroId);
}