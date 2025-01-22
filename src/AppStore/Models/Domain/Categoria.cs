using System.ComponentModel.DataAnnotations;

namespace AppStore.Models.Domain;

public class Categoria
{
    [Key]
    [Required]
    public int Id { get; set; }
    public string? Nombre { get; set; }

    public virtual ICollection<Libro>? LibroList { get; set; }
    public virtual ICollection<LibroCategoria>? LibroCategoriaList { get; set; }

}