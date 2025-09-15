using Microsoft.AspNetCore.Identity;

namespace AppStore.Models.Domain;

public class LoadDatabase{

    public static async Task InsertarData(DatabaseContext context, UserManager<ApplicationUser> usuarioManager, RoleManager<IdentityRole> roleManager)
    {
        if (!roleManager.Roles.Any())
        {
            await roleManager.CreateAsync(new IdentityRole("ADMIN"));
        }

        if (!usuarioManager.Users.Any())
        {
            var usuario = new ApplicationUser{
                Nombre = "Jerson Ramirez",
                Email = "jerson.ramirez@gmail.com",
                UserName = "jerson.ramirez"
            };

            await usuarioManager.CreateAsync(usuario, "PasswordJersonRamirez123$");
            await usuarioManager.AddToRoleAsync(usuario, "ADMIN");

        }

        if (!context.Categorias!.Any())
        {
            await context.Categorias!.AddRangeAsync(
                new Categoria {Nombre = "Drama"},
                new Categoria {Nombre = "Comedia"},
                new Categoria {Nombre = "Accion"},
                new Categoria {Nombre = "Terror"},
                new Categoria {Nombre = "Aventura"}
            );

            await context.SaveChangesAsync();
        }

        if (!context.Libros!.Any())
        {
            await context.Libros!.AddRangeAsync(
                new Libro{
                    Titulo = "El Quijote de la Mancha",
                    CreateDate = "06/06/2020",
                    Imagen = "quijote.jpg",
                    Autor = "Miguel de Cervantes",
                    Resumen = "Lorem ipsum dolor sit amet consectetur adipiscing elit mus praesent taciti sociosqu fames, " +
                    "purus penatibus litora eget torquent molestie vivamus quisque potenti feugiat turpis, convallis himenaeos " +
                    "tempus placerat dis ut malesuada varius phasellus natoque eu. Congue in ornare iaculis feugiat conubia et " +
                    "sagittis convallis consequat, auctor class dapibus sodales placerat felis nisi dictumst fringilla nascetur, nostra pellentesque facilisi penatibus luctus litora augue vivamus. Hac taciti aenean vel scelerisque nullam vulputate, sagittis nec eros auctor cum augue tempus, sollicitudin feugiat nisl porta porttitor phasellus, aliquam ridiculus sapien vitae turpis. At interdum gravida orci ante lobortis ridiculus augue, himenaeos morbi venenatis magnis dignissim placerat nunc, praesent nisl lacus montes aptent parturient."
                },

                new Libro{
                    Titulo = "Harry Potter",
                    CreateDate = "06/01/2021",
                    Imagen = "harry.jpg",
                    Autor = "Juan de la Vega"
                }
            );
        }

        if (!context.LibrosCategorias!.Any())
        {
            await context.LibrosCategorias!.AddRangeAsync(
                new LibroCategoria { CategoriaId = 1, LibroId = 1},
                new LibroCategoria { CategoriaId = 1, LibroId = 2}
            );

            await context.SaveChangesAsync();
        }
        context.SaveChanges();
    }
}