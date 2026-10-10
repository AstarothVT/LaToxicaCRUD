using Microsoft.AspNetCore.Mvc;

namespace LaToxicaCRUD.Helpers
{
    public static class ImagenHelper
    {
        public const string ImagenPorDefecto = "/img/hamburguesa-dibujo.jpg";

        // Devuelve la URL lista para usar en <img>.
        // Si empieza con "/" se arma con Url.Content para que funcione también publicado en IIS.
        public static string ImagenSrc(this IUrlHelper url, string imagenUrl)
        {
            var img = string.IsNullOrWhiteSpace(imagenUrl) ? ImagenPorDefecto : imagenUrl.Trim();
            return img.StartsWith("/") ? url.Content("~" + img) : img;
        }
    }
}
