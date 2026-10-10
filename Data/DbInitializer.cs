using LaToxicaCRUD.Models;
using Microsoft.EntityFrameworkCore;

namespace LaToxicaCRUD.Data
{
    // Datos iniciales del menú de La Tóxica (solo se cargan si la base está vacía)
    public static class DbInitializer
    {
        public static void Sembrar(ToxicaContext context)
        {
            if (!context.Categorias.Any())
                SembrarMenu(context);

            AsignarImagenes(context);
        }

        // Los platos que no tienen imagen reciben una según su categoría
        private static void AsignarImagenes(ToxicaContext context)
        {
            var sinImagen = context.Platos
                .Include(p => p.Categoria)
                .Where(p => p.ImagenUrl == null)
                .ToList();

            foreach (var plato in sinImagen)
            {
                var categoria = plato.Categoria.Nombre;

                if (categoria.Contains("Salchi"))
                    plato.ImagenUrl = "/img/salchipapa.jpg";
                else if (categoria.Contains("Hamburg"))
                    plato.ImagenUrl = "/img/hamburguesa.jpg";
                else
                    plato.ImagenUrl = "/img/hamburguesa-dibujo.jpg";
            }

            if (sinImagen.Any())
                context.SaveChanges();
        }

        private static void SembrarMenu(ToxicaContext context)
        {

            var hamburguesas = new Categoria { Nombre = "Hamburguesas", Descripcion = "Hamburguesas artesanales" };
            var salchipapas = new Categoria { Nombre = "Salchipapas Tóxicas", Descripcion = "Salchipapas cargadas" };
            var extras = new Categoria { Nombre = "Extras", Descripcion = "Porciones adicionales" };

            context.Categorias.AddRange(hamburguesas, salchipapas, extras);

            context.Platos.AddRange(
                new Plato { Nombre = "La Tóxica", Descripcion = "Hamburguesa clásica de 150g artesanal", Precio = 15.00m, Stock = 20, Categoria = hamburguesas },
                new Plato { Nombre = "La Gringa Tóxica", Descripcion = "Hamburguesa artesanal con toque especial", Precio = 16.00m, Stock = 20, Categoria = hamburguesas },
                new Plato { Nombre = "La Auténtica Tóxica", Descripcion = "Hamburguesa artesanal receta original", Precio = 18.00m, Stock = 15, Categoria = hamburguesas },
                new Plato { Nombre = "La Tóxica en Hawái", Descripcion = "Hamburguesa artesanal con piña", Precio = 18.00m, Stock = 15, Categoria = hamburguesas },
                new Plato { Nombre = "La Tóxica a lo Pobre", Descripcion = "Hamburguesa artesanal con huevo y plátano", Precio = 19.00m, Stock = 15, Categoria = hamburguesas },
                new Plato { Nombre = "Doblemente Tóxica", Descripcion = "Hamburguesa doble carne", Precio = 24.00m, Stock = 10, Categoria = hamburguesas },
                new Plato { Nombre = "Intensamente Tóxica", Descripcion = "Hamburguesa cargada al extremo", Precio = 25.00m, Stock = 10, Categoria = hamburguesas },
                new Plato { Nombre = "La Súper Tóxica", Descripcion = "Hamburguesa top de la carta", Precio = 29.00m, Stock = 8, Categoria = hamburguesas },
                new Plato { Nombre = "La Salchitóxica", Descripcion = "Salchipapa clásica cargada", Precio = 16.00m, Stock = 20, Categoria = salchipapas },
                new Plato { Nombre = "Salchitriplemente Tóxica", Descripcion = "Salchipapa con triple ingrediente", Precio = 19.00m, Stock = 15, Categoria = salchipapas },
                new Plato { Nombre = "Salchitóxica a lo Pobre", Descripcion = "Salchipapa con huevo y plátano", Precio = 20.00m, Stock = 15, Categoria = salchipapas },
                new Plato { Nombre = "Salchitóxica Contundente", Descripcion = "Salchipapa full ingredientes", Precio = 24.00m, Stock = 10, Categoria = salchipapas },
                new Plato { Nombre = "Extra queso", Descripcion = "Porción adicional de queso", Precio = 3.00m, Stock = 50, Categoria = extras },
                new Plato { Nombre = "Bacon", Descripcion = "Porción adicional de bacon", Precio = 4.00m, Stock = 50, Categoria = extras },
                new Plato { Nombre = "Huevo", Descripcion = "Huevo frito adicional", Precio = 2.00m, Stock = 50, Categoria = extras },
                new Plato { Nombre = "Doble carne", Descripcion = "Carne adicional de 150g", Precio = 7.00m, Stock = 30, Categoria = extras }
            );

            context.SaveChanges();
        }
    }
}
