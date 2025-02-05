using AppStore.Models.Domain;

namespace AppStore.Models.DTO;

public class CategoriaListVm
{
    public IQueryable<Categoria>? CategoriaList {get; set;}
    public int PageSize {get; set;}
    public int Currentpage {get; set;}
    public int TotalPages {get; set;}
    public string? Term {get; set;}
}