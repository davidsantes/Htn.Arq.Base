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
/****** Object:  StoredProcedure [dbo].[Upd_Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Upd_Pedidos]
GO
/****** Object:  StoredProcedure [dbo].[Upd_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Upd_LineasPedidos]
GO
/****** Object:  StoredProcedure [dbo].[Ins_Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Ins_Pedidos]
GO
/****** Object:  StoredProcedure [dbo].[Ins_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Ins_LineasPedidos]
GO
/****** Object:  StoredProcedure [dbo].[Get_Proveedores]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Proveedores]
GO
/****** Object:  StoredProcedure [dbo].[Get_Productos_By_IdCategoriaProducto]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Productos_By_IdCategoriaProducto]
GO
/****** Object:  StoredProcedure [dbo].[Get_Productos_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Productos_By_Id]
GO
/****** Object:  StoredProcedure [dbo].[Get_Pedidos_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Pedidos_By_Id]
GO
/****** Object:  StoredProcedure [dbo].[Get_Pedidos_By_Filtro]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Pedidos_By_Filtro]
GO
/****** Object:  StoredProcedure [dbo].[Get_LineasPedidos_By_IdPedido]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_LineasPedidos_By_IdPedido]
GO
/****** Object:  StoredProcedure [dbo].[Get_CategoriasProductos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_CategoriasProductos]
GO
/****** Object:  StoredProcedure [dbo].[Get_Categoria_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Get_Categoria_By_Id]
GO
/****** Object:  StoredProcedure [dbo].[Del_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP PROCEDURE IF EXISTS [dbo].[Del_LineasPedidos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Productos]') AND type in (N'U'))
ALTER TABLE [dbo].[Productos] DROP CONSTRAINT IF EXISTS [FK_Productos_CategoriasProductos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[Pedidos]') AND type in (N'U'))
ALTER TABLE [dbo].[Pedidos] DROP CONSTRAINT IF EXISTS [FK_Pedidos_Proveedores]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineasPedidos]') AND type in (N'U'))
ALTER TABLE [dbo].[LineasPedidos] DROP CONSTRAINT IF EXISTS [FK_LineasPedidos_Productos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[LineasPedidos]') AND type in (N'U'))
ALTER TABLE [dbo].[LineasPedidos] DROP CONSTRAINT IF EXISTS [FK_LineasPedidos_Pedidos]
GO
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[CategoriasProductos]') AND type in (N'U'))
ALTER TABLE [dbo].[CategoriasProductos] DROP CONSTRAINT IF EXISTS [DF__Categoria__Fecha__37A5467C]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 24/10/2023 18:55:51 ******/
DROP TABLE IF EXISTS [dbo].[Proveedores]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 24/10/2023 18:55:51 ******/
DROP TABLE IF EXISTS [dbo].[Productos]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP TABLE IF EXISTS [dbo].[Pedidos]
GO
/****** Object:  Table [dbo].[LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
DROP TABLE IF EXISTS [dbo].[LineasPedidos]
GO
/****** Object:  Table [dbo].[CategoriasProductos]    Script Date: 24/10/2023 18:55:51 ******/
DROP TABLE IF EXISTS [dbo].[CategoriasProductos]
GO
/****** Object:  Table [dbo].[CategoriasProductos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[CategoriasProductos](
	[IdCategoriaProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
 CONSTRAINT [PK_CategoriasProductos] PRIMARY KEY CLUSTERED 
(
	[IdCategoriaProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[LineasPedidos](
	[IdPedido] [int] NOT NULL,
	[IdProducto] [int] NOT NULL,
	[Unidades] [int] NOT NULL,
 CONSTRAINT [PK_LineasPedidos] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC,
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Pedidos](
	[IdPedido] [int] IDENTITY(1,1) NOT NULL,
	[IdProveedor] [int] NOT NULL,
	[FechaAlta] [datetime] NOT NULL,
	[FechaEnvio] [datetime] NULL,
	[FechaEntrega] [datetime] NULL,
	[FechaCancelacion] [datetime] NULL,
 CONSTRAINT [PK_Pedidos] PRIMARY KEY CLUSTERED 
(
	[IdPedido] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Productos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Productos](
	[IdProducto] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Descripcion] [varchar](100) NULL,
	[PrecioUnitario] [money] NOT NULL,
	[CantidadPorUnidad] [varchar](50) NULL,
	[IdCategoriaProducto] [int] NOT NULL,
 CONSTRAINT [PK_Productos] PRIMARY KEY CLUSTERED 
(
	[IdProducto] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
/****** Object:  Table [dbo].[Proveedores]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE TABLE [dbo].[Proveedores](
	[IdProveedor] [int] IDENTITY(1,1) NOT NULL,
	[Nombre] [varchar](50) NOT NULL,
	[Direccion] [varchar](150) NOT NULL,
	[CodigoPostal] [varchar](15) NOT NULL,
	[Email] [varchar](150) NOT NULL,
	[Telefono] [varchar](18) NULL,
 CONSTRAINT [PK_Proveedores] PRIMARY KEY CLUSTERED 
(
	[IdProveedor] ASC
)WITH (PAD_INDEX = OFF, STATISTICS_NORECOMPUTE = OFF, IGNORE_DUP_KEY = OFF, ALLOW_ROW_LOCKS = ON, ALLOW_PAGE_LOCKS = ON, FILLFACTOR = 90) ON [PRIMARY]
) ON [PRIMARY]
GO
ALTER TABLE [dbo].[CategoriasProductos] ADD  CONSTRAINT [DF__Categoria__Fecha__37A5467C]  DEFAULT (getdate()) FOR [FechaAlta]
GO
ALTER TABLE [dbo].[LineasPedidos]  WITH CHECK ADD  CONSTRAINT [FK_LineasPedidos_Pedidos] FOREIGN KEY([IdPedido])
REFERENCES [dbo].[Pedidos] ([IdPedido])
GO
ALTER TABLE [dbo].[LineasPedidos] CHECK CONSTRAINT [FK_LineasPedidos_Pedidos]
GO
ALTER TABLE [dbo].[LineasPedidos]  WITH CHECK ADD  CONSTRAINT [FK_LineasPedidos_Productos] FOREIGN KEY([IdProducto])
REFERENCES [dbo].[Productos] ([IdProducto])
GO
ALTER TABLE [dbo].[LineasPedidos] CHECK CONSTRAINT [FK_LineasPedidos_Productos]
GO
ALTER TABLE [dbo].[Pedidos]  WITH CHECK ADD  CONSTRAINT [FK_Pedidos_Proveedores] FOREIGN KEY([IdProveedor])
REFERENCES [dbo].[Proveedores] ([IdProveedor])
GO
ALTER TABLE [dbo].[Pedidos] CHECK CONSTRAINT [FK_Pedidos_Proveedores]
GO
ALTER TABLE [dbo].[Productos]  WITH CHECK ADD  CONSTRAINT [FK_Productos_CategoriasProductos] FOREIGN KEY([IdCategoriaProducto])
REFERENCES [dbo].[CategoriasProductos] ([IdCategoriaProducto])
GO
ALTER TABLE [dbo].[Productos] CHECK CONSTRAINT [FK_Productos_CategoriasProductos]
GO
/****** Object:  StoredProcedure [dbo].[Del_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Del_LineasPedidos]
	  @IdPedido int
	, @IdProducto int
AS
BEGIN

	DELETE 
	FROM [dbo].[LineasPedidos]
	WHERE
			[IdPedido] = @IdPedido
		AND [IdProducto] = @IdProducto
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Categoria_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Categoria_By_Id]
	@Id INT
AS
BEGIN
	SELECT IdCategoriaProducto AS Id,
		   Nombre,
		   Descripcion
	FROM CategoriasProductos
	WHERE IdCategoriaProducto = @Id;
END
GO
/****** Object:  StoredProcedure [dbo].[Get_CategoriasProductos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_CategoriasProductos]
AS

SET NOCOUNT ON

SELECT
	  [IdCategoriaProducto]
	, [Nombre]
	, [Descripcion]
FROM
	[dbo].[CategoriasProductos]
GO
/****** Object:  StoredProcedure [dbo].[Get_LineasPedidos_By_IdPedido]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_LineasPedidos_By_IdPedido]
	 @IdPedido INT
	
AS
BEGIN
	SET NOCOUNT ON;

	SELECT 
		  [IdProducto]			        = [LineasPedidos].[IdProducto]
		, [Unidades]			        = [LineasPedidos].[Unidades]
		, [NombreProducto]		        = [Productos].[Nombre] 
		, [DescripcionProducto]         = [Productos].[Descripcion] 
		, [PrecioUnitarioProducto]      = [Productos].[PrecioUnitario]
		, [CantidadPorUnidadProducto]   = [Productos].[CantidadPorUnidad]
		, [IdCategoriaProducto]		    = [Productos].[IdCategoriaProducto]
		, [NombreCategoriaProducto]		= [CategoriasProductos].[Nombre]
		, [DescripcionCategoriaProducto]= [CategoriasProductos].[Descripcion]

FROM
	LineasPedidos
	INNER JOIN Productos ON LineasPedidos.IdProducto = Productos.IdProducto
	INNER JOIN CategoriasProductos ON CategoriasProductos.IdCategoriaProducto = Productos.IdCategoriaProducto

WHERE
	(LineasPedidos.IdPedido = @IdPedido)


--ORDER BY Productos.IdProducto ASC
END
GO
/****** Object:  StoredProcedure [dbo].[Get_Pedidos_By_Filtro]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Pedidos_By_Filtro]
	  @IdCategoriaProducto INT = NULL
	, @IdProducto INT = NULL
	, @IdPedido INT = NULL
	, @IdProveedor INT = NULL
	, @NumMaximoResultados INT
AS
BEGIN
	SET NOCOUNT ON;

	SELECT DISTINCT TOP (@NumMaximoResultados)
		  [IdPedido]	 		  = [Pedidos].[IdPedido]
		, [FechaAlta]			  = [Pedidos].[FechaAlta]
		, [FechaEnvio]			  = [Pedidos].[FechaEnvio] 
		, [FechaEntrega]	  	  = [Pedidos].[FechaEntrega]
		, [FechaCancelacion]	  = [Pedidos].[FechaCancelacion] 
		, [IdProveedor]			  =	[Proveedores].[IdProveedor] 
		, [NombreProveedor]		  = [Proveedores].[Nombre] 
		, [DireccionProveedor]	  = [Proveedores].[Direccion] 
		, [CodigoPostalProveedor] = [Proveedores].[CodigoPostal] 
		, [EmailProveedor]		  = [Proveedores].[Email]
		, [TelefonoProveedor]	  = [Proveedores].[Telefono] 

FROM
	Pedidos
	INNER JOIN Proveedores ON Pedidos.IdProveedor= Proveedores.IdProveedor
	LEFT JOIN LineasPedidos ON LineasPedidos.IdPedido = Pedidos.IdPedido
	LEFT JOIN Productos ON LineasPedidos.IdProducto = Productos.IdProducto
	LEFT JOIN CategoriasProductos ON CategoriasProductos.IdCategoriaProducto = Productos.IdCategoriaProducto
	

WHERE
	(@IdPedido IS NULL OR Pedidos.IdPedido = @IdPedido)
AND (@IdProducto IS NULL OR LineasPedidos.IdProducto = @IdProducto)
AND (@IdCategoriaProducto IS NULL OR Productos.IdCategoriaProducto = @IdCategoriaProducto)
AND (@IdProveedor IS NULL OR Pedidos.IdProveedor = @IdProveedor)

ORDER BY Pedidos.IdPedido ASC

END

GO
/****** Object:  StoredProcedure [dbo].[Get_Pedidos_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Pedidos_By_Id]
	@IdPedido INT
AS

SET NOCOUNT ON

SELECT
		  [IdPedido]	 		  = [Pedidos].[IdPedido]
		, [FechaAlta]			  = [Pedidos].[FechaAlta]
		, [FechaEnvio]			  = [Pedidos].[FechaEnvio] 
		, [FechaEntrega]	  	  = [Pedidos].[FechaEntrega]
		, [FechaCancelacion]	  = [Pedidos].[FechaCancelacion] 
		, [IdProveedor]			  =	[Proveedores].[IdProveedor] 
		, [NombreProveedor]		  = [Proveedores].[Nombre] 
		, [DireccionProveedor]	  = [Proveedores].[Direccion] 
		, [CodigoPostalProveedor] = [Proveedores].[CodigoPostal] 
		, [EmailProveedor]		  = [Proveedores].[Email]
		, [TelefonoProveedor]	  = [Proveedores].[Telefono] 
FROM
	[dbo].[Pedidos] 
	INNER JOIN [dbo].[Proveedores] ON [Pedidos].[IdProveedor] = [Proveedores].[IdProveedor]
WHERE 
	IdPedido = @IdPedido
GO
/****** Object:  StoredProcedure [dbo].[Get_Productos_By_Id]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[Get_Productos_By_Id]
	@IdProducto INT
AS

SET NOCOUNT ON

SELECT
	  [IdProducto]					 = [Productos].[IdProducto]
	, [Nombre]						 = [Productos].[Nombre]
	, [Descripcion]					 = [Productos].[Descripcion]
	, [PrecioUnitario]				 = [Productos].[PrecioUnitario]
	, [CantidadPorUnidad]			 = [Productos].[CantidadPorUnidad]
	, [IdCategoriaProducto]			 = [CategoriasProductos].[IdCategoriaProducto]
	, [NombreCategoriaProducto]		 = [CategoriasProductos].[Nombre]
	, [DescripcionCategoriaProducto] = [CategoriasProductos].[Descripcion]
FROM
			   [dbo].[Productos] 
	INNER JOIN [dbo].[CategoriasProductos] ON Productos.IdCategoriaProducto = CategoriasProductos.IdCategoriaProducto
WHERE
	[Productos].[IdProducto] = @IdProducto
GO
/****** Object:  StoredProcedure [dbo].[Get_Productos_By_IdCategoriaProducto]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Productos_By_IdCategoriaProducto]
	@IdCategoriaProducto INT
AS

SET NOCOUNT ON

SELECT
	  [IdProducto]					 = [Productos].[IdProducto]
	, [Nombre]						 = [Productos].[Nombre]
	, [Descripcion]					 = [Productos].[Descripcion]
	, [PrecioUnitario]				 = [Productos].[PrecioUnitario]
	, [CantidadPorUnidad]			 = [Productos].[CantidadPorUnidad]
	, [IdCategoriaProducto]			 = [CategoriasProductos].[IdCategoriaProducto]
	, [NombreCategoriaProducto]		 = [CategoriasProductos].[Nombre]
	, [DescripcionCategoriaProducto] = [CategoriasProductos].[Descripcion]
FROM
			   [dbo].[Productos] 
	INNER JOIN [dbo].[CategoriasProductos] ON Productos.IdCategoriaProducto = CategoriasProductos.IdCategoriaProducto
WHERE
	[dbo].[Productos].[IdCategoriaProducto] = @IdCategoriaProducto
GO
/****** Object:  StoredProcedure [dbo].[Get_Proveedores]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Get_Proveedores]
AS

SET NOCOUNT ON

SELECT
	  [IdProveedor]
	, [Nombre]
	, [Direccion]
	, [CodigoPostal]
	, [Email]
	, [Telefono]
FROM
	[dbo].[Proveedores]

GO
/****** Object:  StoredProcedure [dbo].[Ins_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Ins_LineasPedidos]
	  @IdPedido INT
	, @IdProducto INT
	, @Unidades INT
AS

BEGIN

	INSERT INTO [dbo].[LineasPedidos] (
		  [IdPedido]
		, [IdProducto]
		, [Unidades]
	) VALUES (
		  @IdPedido
		, @IdProducto
		, @Unidades
	)

END
GO
/****** Object:  StoredProcedure [dbo].[Ins_Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Ins_Pedidos]
	   @IdPedido INT OUTPUT
	 , @FechaAlta DATETIME
	 , @FechaEnvio DATETIME
	 , @FechaEntrega DATETIME
	 , @FechaCancelacion DATETIME
	 , @IdProveedor INT

AS
BEGIN

	INSERT INTO [dbo].[Pedidos] (
		  FechaAlta
		, FechaEnvio
		, FechaEntrega
		, FechaCancelacion
		, IdProveedor)
	VALUES (
		  @FechaAlta
		, @FechaEnvio
		, @FechaEntrega
		, @FechaCancelacion
		, @IdProveedor
	)
	
	SET @IdPedido = SCOPE_IDENTITY()
END
GO
/****** Object:  StoredProcedure [dbo].[Upd_LineasPedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Upd_LineasPedidos]
	  @IdPedido INT
	, @IdProducto INT
	, @Unidades INT
AS
BEGIN

	UPDATE 
			[dbo].[LineasPedidos] 
	SET
			[Unidades] = @Unidades
	WHERE
			[IdPedido]	 = @IdPedido
		AND [IdProducto] = @IdProducto
END
GO
/****** Object:  StoredProcedure [dbo].[Upd_Pedidos]    Script Date: 24/10/2023 18:55:51 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
CREATE PROCEDURE [dbo].[Upd_Pedidos]
	   @IdPedido INT
	 , @FechaAlta DATETIME
	 , @FechaEnvio DATETIME
	 , @FechaEntrega DATETIME
	 , @FechaCancelacion DATETIME
	 , @IdProveedor INT

AS
BEGIN

	UPDATE [dbo].[Pedidos] 
	SET FechaAlta		 = @FechaAlta
	  , FechaEnvio		 = @FechaEnvio
	  , FechaEntrega	 = @FechaEntrega
	  , FechaCancelacion = @FechaCancelacion
	  , IdProveedor		 = @IdProveedor
	WHERE IdPedido = @IdPedido

END
GO

PRINT 'Finalizada la creación';
GO