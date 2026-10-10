-- =========================================================
-- La Tóxica - Script para recrear la base de datos BD_LaToxicaFinal
-- Equivale a la migración CrearBaseLaToxica + datos iniciales del menú.
-- (Opción 2: si prefieres no usar las migraciones de EF Core)
-- =========================================================

IF DB_ID(N'BD_LaToxicaFinal') IS NULL
    CREATE DATABASE BD_LaToxicaFinal;
GO

USE BD_LaToxicaFinal;
GO

IF OBJECT_ID(N'[__EFMigrationsHistory]') IS NULL
BEGIN
    CREATE TABLE [__EFMigrationsHistory] (
        [MigrationId] nvarchar(150) NOT NULL,
        [ProductVersion] nvarchar(32) NOT NULL,
        CONSTRAINT [PK___EFMigrationsHistory] PRIMARY KEY ([MigrationId])
    );
END;
GO

IF NOT EXISTS (SELECT 1 FROM [__EFMigrationsHistory] WHERE [MigrationId] = N'20261007150000_CrearBaseLaToxica')
BEGIN
    CREATE TABLE [Categorias] (
        [IdCategoria] int NOT NULL IDENTITY(1,1),
        [Nombre] nvarchar(50) NOT NULL,
        [Descripcion] nvarchar(200) NULL,
        CONSTRAINT [PK_Categorias] PRIMARY KEY ([IdCategoria])
    );

    CREATE TABLE [Platos] (
        [IdPlato] int NOT NULL IDENTITY(1,1),
        [Nombre] nvarchar(100) NOT NULL,
        [Descripcion] nvarchar(200) NULL,
        [Precio] decimal(10,2) NOT NULL,
        [Stock] int NOT NULL,
        [CategoriaId] int NOT NULL,
        [ImagenUrl] nvarchar(300) NULL,
        CONSTRAINT [PK_Platos] PRIMARY KEY ([IdPlato]),
        CONSTRAINT [FK_Platos_Categorias_CategoriaId] FOREIGN KEY ([CategoriaId]) REFERENCES [Categorias] ([IdCategoria]) ON DELETE NO ACTION
    );

    CREATE INDEX [IX_Platos_CategoriaId] ON [Platos] ([CategoriaId]);

    INSERT INTO [__EFMigrationsHistory] ([MigrationId], [ProductVersion])
    VALUES (N'20261007150000_CrearBaseLaToxica', N'8.0.0'),
           (N'20261008120000_AgregarImagenUrl', N'8.0.0');
END;
GO

-- Datos iniciales del menú
IF NOT EXISTS (SELECT 1 FROM Categorias)
BEGIN
    SET IDENTITY_INSERT Categorias ON;
    INSERT INTO Categorias (IdCategoria, Nombre, Descripcion) VALUES
        (1, N'Hamburguesas', N'Hamburguesas artesanales'),
        (2, N'Salchipapas Tóxicas', N'Salchipapas cargadas'),
        (3, N'Extras', N'Porciones adicionales');
    SET IDENTITY_INSERT Categorias OFF;

    INSERT INTO Platos (Nombre, Descripcion, Precio, Stock, CategoriaId) VALUES
    (N'La Tóxica', N'Hamburguesa clásica de 150g artesanal', 15.00, 20, 1),
    (N'La Gringa Tóxica', N'Hamburguesa artesanal con toque especial', 16.00, 20, 1),
    (N'La Auténtica Tóxica', N'Hamburguesa artesanal receta original', 18.00, 15, 1),
    (N'La Tóxica en Hawái', N'Hamburguesa artesanal con piña', 18.00, 15, 1),
    (N'La Tóxica a lo Pobre', N'Hamburguesa artesanal con huevo y plátano', 19.00, 15, 1),
    (N'Doblemente Tóxica', N'Hamburguesa doble carne', 24.00, 10, 1),
    (N'Intensamente Tóxica', N'Hamburguesa cargada al extremo', 25.00, 10, 1),
    (N'La Súper Tóxica', N'Hamburguesa top de la carta', 29.00, 8, 1),
    (N'La Salchitóxica', N'Salchipapa clásica cargada', 16.00, 20, 2),
    (N'Salchitriplemente Tóxica', N'Salchipapa con triple ingrediente', 19.00, 15, 2),
    (N'Salchitóxica a lo Pobre', N'Salchipapa con huevo y plátano', 20.00, 15, 2),
    (N'Salchitóxica Contundente', N'Salchipapa full ingredientes', 24.00, 10, 2),
    (N'Extra queso', N'Porción adicional de queso', 3.00, 50, 3),
    (N'Bacon', N'Porción adicional de bacon', 4.00, 50, 3),
    (N'Huevo', N'Huevo frito adicional', 2.00, 50, 3),
    (N'Doble carne', N'Carne adicional de 150g', 7.00, 30, 3);
END;
GO

-- Imágenes por categoría (están en wwwroot/img)
UPDATE Platos SET ImagenUrl = N'/img/hamburguesa.jpg' WHERE CategoriaId = 1 AND ImagenUrl IS NULL;
UPDATE Platos SET ImagenUrl = N'/img/salchipapa.jpg' WHERE CategoriaId = 2 AND ImagenUrl IS NULL;
UPDATE Platos SET ImagenUrl = N'/img/hamburguesa-dibujo.jpg' WHERE CategoriaId = 3 AND ImagenUrl IS NULL;
GO

SELECT c.Nombre AS Categoria, COUNT(*) AS Platos
FROM Platos p INNER JOIN Categorias c ON c.IdCategoria = p.CategoriaId
GROUP BY c.Nombre;
GO
