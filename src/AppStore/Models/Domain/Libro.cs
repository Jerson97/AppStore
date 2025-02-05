using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace AppStore.Models.Domain;

public class Libro
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Titulo { get; set; }
    public string? CreateDate { get; set; }
    public string? Imagen { get; set; }
    [Required]
    public string? Autor { get; set; }
    public virtual ICollection<Categoria>? CategoriaList { get; set; }
    public virtual ICollection<LibroCategoria>? LibroCategoriaList { get; set; }

    [NotMapped]
    public List<int>? Categorias {get; set;}
    [NotMapped]
    public string? CategoriaNombre {get; set;}
    [NotMapped]
    public IFormFile? ImageFile {get; set;}
    [NotMapped]
    public IEnumerable<SelectListItem>? CategoriaLista {get; set;}
    [NotMapped]
    public MultiSelectList? MultiCategoriaList {get; set;}
}