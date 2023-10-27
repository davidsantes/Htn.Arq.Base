PRINT 'Inicio creación de base de datos';
GO

-- Verificar si la base de datos existe
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'TiendaDb')
BEGIN
	PRINT 'La base de datos TiendaDb existe, se eliminará y volverá a crear';
END
ELSE
BEGIN
	PRINT 'La base de datos TiendaDb no existe, se volverá a crear';
END

-- Eliminar la base de datos si existe
IF EXISTS (SELECT 1 FROM sys.databases WHERE name = 'TiendaDb')
	DROP DATABASE TiendaDb;
GO

-- Crear la base de datos
CREATE DATABASE TiendaDb;
GO

USE [TiendaDb]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Productos]') AND type in (N'U'))
ALTER TABLE [dbo].[Productos] DROP CONSTRAINT IF EXISTS [FK__Productos__IdCat__2D27B809]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PedidosDetalle]') AND type in (N'U'))
ALTER TABLE [dbo].[PedidosDetalle] DROP CONSTRAINT IF EXISTS [FK_PedidosDetalle_Productos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[PedidosDetalle]') AND type in (N'U'))
ALTER TABLE [dbo].[PedidosDetalle] DROP CONSTRAINT IF EXISTS [FK_PedidosDetalle_Pedidos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedidos]') AND type in (N'U'))
ALTER TABLE [dbo].[Pedidos] DROP CONSTRAINT IF EXISTS [FK__Pedidos__IdProve__35BCFE0A]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Categorias]') AND type in (N'U'))
ALTER TABLE [dbo].[Categorias] DROP CONSTRAINT IF EXISTS [DF_Categorias_FechaAlta]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 27/10/2023 11:53:37 ******/
DROP TABLE IF EXISTS [dbo].[Proveedores]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 27/10/2023 11:53:37 ******/
DROP TABLE IF EXISTS [dbo].[Productos]
GO
/****** Object:  Table [dbo].[PedidosDetalle]    Script Date: 27/10/2023 11:53:37 ******/
DROP TABLE IF EXISTS [dbo].[PedidosDetalle]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 27/10/2023 11:53:37 ******/
DROP TABLE IF EXISTS [dbo].[Pedidos]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 27/10/2023 11:53:37 ******/
DROP TABLE IF EXISTS [dbo].[Categorias]
GO
/****** Object:  Table [dbo].[Categorias]    Script Date: 27/10/2023 11:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Categorias](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 27/10/2023 11:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[Id] [uniqueidentifier] NOT NULL,
	[IdProveedor] [uniqueidentifier] NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[FechaEnvio] [datetime] NULL,
	[FechaEntrega] [datetime] NULL,
	[FechaCancelacion] [datetime] NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[PedidosDetalle]    Script Date: 27/10/2023 11:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[PedidosDetalle](
	[IdPedido] [uniqueidentifier] NOT NULL,
	[IdProducto] [uniqueidentifier] NOT NULL,
	[Unidades] [int] NOT NULL
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 27/10/2023 11:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[PrecioUnitario] [money] NOT NULL,
	[CantidadPorUnidad] [varchar](50) NULL,
	[IdCategoriaProducto] [uniqueidentifier] NOT NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 27/10/2023 11:53:37 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[Id] [uniqueidentifier] NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Direccion] [varchar](150) NOT NULL,
	[CodigoPostal] [varchar](15) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Telefono] [varchar](18) NULL,
PRIMARY KEY CLUSTERED 
(
	[Id] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, OPTIMIZE_FOR_SEQUENTIAL_KEY = OFF) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[Categorias] ADD  CONSTRAINT [DF_Categorias_FechaAlta]  DEFAULT (getdate()) FOR [FechaAlta]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedores] ([Id])
GO
ALTER TABLE [dbo].[PedidosDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidosDetalle_Pedidos] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedidos] ([Id])
GO
ALTER TABLE [dbo].[PedidosDetalle] CHECK CONSTRAINT [FK_PedidosDetalle_Pedidos]
GO
ALTER TABLE [dbo].[PedidosDetalle]  WITH CHECK ADD  CONSTRAINT [FK_PedidosDetalle_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([Id])
GO
ALTER TABLE [dbo].[PedidosDetalle] CHECK CONSTRAINT [FK_PedidosDetalle_Productos]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD FOREIGN KEY([IdCategoriaProducto])
REFERENCES [dbo].[Categorias] ([Id])
GO

/****** Procedimiento almacenado [dbo].[Get_Categoria_By_Id] ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Categoria_By_Id];
GO

SET ANSI_NULLS ON;
GO
SET QUOTED_IDENTIFIER ON;
GO

CREATE PROCEDURE [dbo].[Get_Categoria_By_Id]
    @CategoryId uniqueidentifier
AS
BEGIN
    SELECT 
        [Id] = [Categorias].[Id],
        [Nombre] = [Categorias].[Nombre],
        [Descripcion] = [Categorias].[Descripcion]
    FROM Categorias
    WHERE [Categorias].[Id] = @CategoryId;
END;
GO

PRINT 'Fin creación de base de datos';
GO