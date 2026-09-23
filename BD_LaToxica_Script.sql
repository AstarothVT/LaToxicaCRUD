-- =========================================
-- EA2 - Aplicacion en capas La Toxica
-- Script de base de datos
-- =========================================

IF EXISTS (SELECT name FROM sys.databases WHERE name = 'BD_LaToxica')
    DROP DATABASE BD_LaToxica;
GO

CREATE DATABASE BD_LaToxica;
GO

USE BD_LaToxica;
GO

-- =========================================
-- Tablas
-- =========================================

CREATE TABLE Categorias
(
    IdCategoria INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(50) NOT NULL
);
GO

CREATE TABLE Platos
(
    IdPlato INT IDENTITY(1,1) PRIMARY KEY,
    Nombre VARCHAR(100) NOT NULL,
    Descripcion VARCHAR(200),
    Precio DECIMAL(10,2) NOT NULL,
    Stock INT NOT NULL,
    CategoriaId INT NOT NULL,
    CONSTRAINT FK_Platos_Categorias FOREIGN KEY (CategoriaId) REFERENCES Categorias(IdCategoria)
);
GO

CREATE TABLE Pedidos
(
    IdPedido INT IDENTITY(1,1) PRIMARY KEY,
    Cliente VARCHAR(100) NOT NULL,
    Fecha DATETIME NOT NULL DEFAULT GETDATE(),
    Total DECIMAL(10,2) NOT NULL DEFAULT 0
);
GO

CREATE TABLE DetallePedidos
(
    IdDetalle INT IDENTITY(1,1) PRIMARY KEY,
    PedidoId INT NOT NULL,
    PlatoId INT NOT NULL,
    Cantidad INT NOT NULL,
    Subtotal DECIMAL(10,2) NOT NULL,
    CONSTRAINT FK_Detalle_Pedidos FOREIGN KEY (PedidoId) REFERENCES Pedidos(IdPedido),
    CONSTRAINT FK_Detalle_Platos FOREIGN KEY (PlatoId) REFERENCES Platos(IdPlato)
);
GO

CREATE TABLE Usuarios
(
    IdUsuario INT IDENTITY(1,1) PRIMARY KEY,
    NombreUsuario VARCHAR(50) NOT NULL UNIQUE,
    Password VARCHAR(100) NOT NULL
);
GO

-- =========================================
-- Datos de prueba
-- =========================================

INSERT INTO Categorias (Nombre) VALUES
('Hamburguesas'),
('Salchipapas Toxicas'),
('Extras');
GO

INSERT INTO Platos (Nombre, Descripcion, Precio, Stock, CategoriaId) VALUES
('La Toxica', 'Hamburguesa clasica de 150g artesanal', 15.00, 20, 1),
('La Gringa Toxica', 'Hamburguesa artesanal con toque especial', 16.00, 20, 1),
('La Autentica Toxica', 'Hamburguesa artesanal receta original', 18.00, 15, 1),
('La Toxica en Hawai', 'Hamburguesa artesanal con pina', 18.00, 15, 1),
('La Toxica a lo Pobre', 'Hamburguesa artesanal con huevo y platano', 19.00, 15, 1),
('Doblemente Toxica', 'Hamburguesa doble carne', 24.00, 10, 1),
('Intensamente Toxica', 'Hamburguesa cargada al extremo', 25.00, 10, 1),
('La Super Toxica', 'Hamburguesa top de la carta', 29.00, 8, 1),
('La Salchitoxica', 'Salchipapa clasica cargada', 16.00, 20, 2),
('Salchitriplemente Toxica', 'Salchipapa con triple ingrediente', 19.00, 15, 2),
('Salchitoxica a lo Pobre', 'Salchipapa con huevo y platano', 20.00, 15, 2),
('Salchitoxica Contundente', 'Salchipapa full ingredientes', 24.00, 10, 2),
('Extra queso', 'Porcion adicional de queso', 3.00, 50, 3),
('Bacon', 'Porcion adicional de bacon', 4.00, 50, 3),
('Huevo', 'Huevo frito adicional', 2.00, 50, 3),
('Doble carne', 'Carne adicional de 150g', 7.00, 30, 3);
GO

-- Usuario de prueba para el login (usuario: admin / clave: admin123)
INSERT INTO Usuarios (NombreUsuario, Password) VALUES
('admin', 'admin123');
GO

-- Un pedido de ejemplo con su detalle, para mostrar la relacion entre tablas
INSERT INTO Pedidos (Cliente, Fecha, Total) VALUES ('Cliente de prueba', GETDATE(), 34.00);
GO
INSERT INTO DetallePedidos (PedidoId, PlatoId, Cantidad, Subtotal) VALUES
(1, 1, 1, 15.00),
(1, 9, 1, 16.00),
(1, 13, 1, 3.00);
GO

-- =========================================
-- Procedimientos almacenados
-- =========================================

CREATE PROCEDURE sp_ListarPlatos
AS
BEGIN
    SELECT p.IdPlato, p.Nombre, p.Descripcion, p.Precio, p.Stock, c.Nombre AS Categoria
    FROM Platos p
    INNER JOIN Categorias c ON p.CategoriaId = c.IdCategoria;
END;
GO

CREATE PROCEDURE sp_BuscarPlato
    @IdPlato INT
AS
BEGIN
    SELECT * FROM Platos WHERE IdPlato = @IdPlato;
END;
GO

CREATE PROCEDURE sp_PlatosPorCategoria
    @CategoriaId INT
AS
BEGIN
    SELECT * FROM Platos WHERE CategoriaId = @CategoriaId;
END;
GO

-- =========================================
-- Pruebas de los procedimientos
-- =========================================

EXEC sp_ListarPlatos;
GO
EXEC sp_BuscarPlato 1;
GO
EXEC sp_PlatosPorCategoria 1;
GO
