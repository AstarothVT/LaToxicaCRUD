using System;
using System.IO;
using Microsoft.AspNetCore.Hosting;

namespace LaToxicaCRUD.Services
{
    // Servicio simple de logging: escribe los errores en un archivo de texto
    // dentro de la carpeta Logs del proyecto (requisito de manejo de errores y logs)
    public class LogService : ILogService
    {
        private readonly string _rutaArchivo;

        public LogService(IWebHostEnvironment env)
        {
            var carpetaLogs = Path.Combine(env.ContentRootPath, "Logs");
            if (!Directory.Exists(carpetaLogs))
            {
                Directory.CreateDirectory(carpetaLogs);
            }
            _rutaArchivo = Path.Combine(carpetaLogs, "errores.log");
        }

        public void RegistrarError(string origen, Exception ex)
        {
            try
            {
                var linea = $"[{DateTime.Now:yyyy-MM-dd HH:mm:ss}] Origen: {origen} | Error: {ex.Message}{Environment.NewLine}";
                File.AppendAllText(_rutaArchivo, linea);
            }
            catch
            {
                // Si ni siquiera se puede escribir el log, no hacemos nada mas
                // (evitamos que un error de logging tumbe la aplicacion)
            }
        }
    }
}
