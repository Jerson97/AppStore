using AppStore.Models.Domain;
using AppStore.Models.DTO;

namespace AppStore.Repositories.Abstract;

public interface ICategoriaService
{
    IQueryable<Categoria> List();
    bool Insertar(Categoria categoria);
    bool Actualizar(Categoria categoria);
    Categoria ObtenerId(int id);
    bool Eliminar(int id);
    CategoriaListVm Listar(string term="", bool paging = false, int currentPage = 0);
}