using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AppStore.Models.Domain;

public class DatabaseContext : IdentityDbContext<ApplicationUser>
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    {
        
    }
    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Libro>()
            .HasMany(x => x.CategoriaList)
            .WithMany(y => y.LibroList)
            .UsingEntity<LibroCategoria>(
                j => j
                .HasOne(p => p.Categoria)
                .WithMany(p => p.LibroCategoriaList)
                .HasForeignKey(p => p.CategoriaId),
                j => j
                .HasOne(p => p.Libro)
                .WithMany(p => p.LibroCategoriaList)
                .HasForeignKey(p => p.LibroId),
                j => 
                {
                    j.HasKey(t => new {t.LibroId, t.CategoriaId});
                }
            );
    }

    public DbSet<Categoria>? Categorias {get; set;}
    public DbSet<Libro>? Libros {get; set;}
    public DbSet<LibroCategoria>? LibrosCategorias {get; set;}
}