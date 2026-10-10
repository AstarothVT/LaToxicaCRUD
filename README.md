# La Tóxica - CRUD de Platos (ASP.NET Core MVC)

Proyecto final del curso **Aplicaciones Web** (Instituto SISE, Evaluación Final).

## Integrantes

- Eddy Rodrigo Dioses Taboada
- (agregar aquí a los demás integrantes del grupo)

## Descripción del caso de negocio

**La Tóxica** es un restaurante de hamburguesas y salchipapas artesanales. El local
llevaba el menú y el stock en papel, así que los precios y las cantidades se
desactualizaban rápido. Esta aplicación permite al administrador:

- Mostrar al público la **carta**, los **platos típicos** de una hamburguesería y los destacados (páginas públicas).
- Entrar con **login** al panel de administración.
- Registrar, ver, editar y eliminar **platos** del menú (con imagen por URL).
- Organizar los platos por **categorías** (Hamburguesas, Salchipapas Tóxicas, Extras).
- Ver un **reporte de inventario** con filtros (categoría, rango de precios, stock bajo),
  imprimirlo o exportarlo a Excel (CSV).

**Usuarios:** administrador / encargado del local.

## Tecnologías

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core 8 - Code First con migraciones
- SQL Server (SQL Server Express)
- Bootstrap 5, Bootstrap Icons, jQuery Validation
- Autenticación con cookies (login del administrador)
- Visual Studio 2026

## Acceso al panel de administración

- Páginas públicas: Inicio, Carta, Platos típicos.
- Panel (requiere login): Platos, Categorías y Reportes.
- Usuario: `admin` · Contraseña: `Toxica2026` (se cambian en `appsettings.json`, sección `Admin`).

## Imágenes

Cada plato tiene un campo **Imagen (URL)**: sirve un enlace `https://...` o una imagen de la carpeta
`wwwroot/img` (por ejemplo `/img/hamburguesa.jpg`). Si el enlace falla, se muestra una imagen por defecto.

## Estructura (MVC)

| Carpeta | Contenido |
| --- | --- |
| `Models/` | Entidades `Plato`, `Categoria` y view models del reporte |
| `Views/` | Vistas Razor, `_Layout` y vistas parciales (`_Navbar`, `_Footer`, `_Mensajes`, `_Form`) |
| `Controllers/` | `Home`, `Account` (login), `Platos`, `Categorias`, `Reportes` |
| `Data/` | `ToxicaContext` (DbContext) y `DbInitializer` (datos iniciales) |
| `Services/` | `ReporteService` (consultas y exportación del reporte) |
| `Migrations/` | `CrearBaseLaToxica` y `AgregarImagenUrl` |
| `wwwroot/` | CSS, JS, imágenes y favicon |
| `Scripts/` | Script SQL para recrear la base de datos |

## Cómo ejecutar el proyecto

1. Instalar Visual Studio con la carga de trabajo **ASP.NET y desarrollo web**, el SDK de **.NET 8** y **SQL Server Express**.
2. Clonar el repositorio y abrir `LaToxicaCRUD.csproj` (o la solución) en Visual Studio.
3. Revisar la cadena de conexión en `appsettings.json` (`ConexionToxica`). Por defecto usa
   `Server=.\SQLEXPRESS01`. Si tu instancia se llama distinto, cámbiala.
4. Crear la base de datos con **una** de estas opciones:
   - **Migraciones (recomendado):** en *Consola del Administrador de paquetes* ejecutar
     `Update-Database`. (Al iniciar la app también se aplican las migraciones automáticamente.)
   - **Script SQL:** abrir `Scripts/BD_LaToxicaFinal.sql` en SSMS y ejecutarlo completo.
5. Ejecutar con **F5**. La página inicial es `/` y el menú se carga solo la primera vez.

La base se llama `BD_LaToxicaFinal`.

## Publicación en IIS local

1. Activar IIS (Panel de control > Características de Windows > Internet Information Services) e instalar el **.NET 8 Hosting Bundle**.
2. En Visual Studio: clic derecho al proyecto > **Publicar** > **Carpeta** > publicar en `C:\inetpub\LaToxica`.
3. En IIS Manager: crear un sitio nuevo apuntando a esa carpeta (Application Pool sin código administrado).
4. Dar acceso a SQL Server al usuario del Application Pool (`IIS APPPOOL\NombreDelPool`)
   o cambiar la cadena de conexión a un usuario SQL.
5. Abrir el sitio en el navegador y comprobar CRUD, reporte y vista en celular.

## Reporte

Menú **Reportes**: inventario de platos con filtros por categoría, precio mínimo/máximo y stock bajo.
Se puede exportar a Excel (CSV) y a PDF con el botón *Imprimir / Guardar como PDF*.

## Pruebas

Los casos de prueba y la ficha de prueba cruzada están en el documento
`Casos_de_Prueba_LaToxica.docx`.
