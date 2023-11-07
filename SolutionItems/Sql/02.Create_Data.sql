USE [TiendaDb]
GO

delete [PedidosDetalle]
delete [Pedidos]
delete [Productos]
delete [Categorias]
delete [Proveedores]

-------------------------------------------------------- Inserción de Categorías ----------------------------------------------------------------------------------------------
INSERT INTO [dbo].[Categorias] ([Id], [Nombre], [Descripcion]) 
VALUES 
    (NEWID(), N'Beverages', N'Soft drinks, coffees, teas, beers, and ales'),
    (NEWID(), N'Condiments', N'Sweet and savory sauces, relishes, spreads, and seasonings'),
    (NEWID(), N'Confections', N'Desserts, candies, and sweet breads'),
    (NEWID(), N'Dairy Products', N'Cheeses'),
    (NEWID(), N'Grains/Cereals', N'Breads, crackers, pasta, and cereal'),
    (NEWID(), N'Meat/Poultry', N'Prepared meats'),
    (NEWID(), N'Produce', N'Dried fruit and bean curd'),
    (NEWID(), N'Seafood', N'Seaweed and fish');
GO

-------------------------------------------------------- Inserción de Productos ----------------------------------------------------------------------------------------------

-- Declaración de una variable de tabla para almacenar GUIDs de Categorias
DECLARE @CategoriasGuids TABLE (Id uniqueidentifier);

-- Insertar GUIDs existentes de Categorias en la variable de tabla
INSERT INTO @CategoriasGuids (Id)
SELECT Id
FROM Categorias;

INSERT INTO [dbo].[Productos] ([Id], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto])
SELECT NEWID(), N'Chai', N'Esta es una descripción tonta', 18.0000, N'10 boxes x 20 bags', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Chang', N'Esta es una descripción tonta', 19.0000, N'24 - 12 oz bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Aniseed Syrup', N'Esta es una descripción tonta', 10.0000, N'12 - 550 ml bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Chef Anton''s Cajun Seasoning', N'Esta es una descripción tonta', 22.0000, N'48 - 6 oz jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Chef Anton''s Gumbo Mix', N'Esta es una descripción tonta', 21.3500, N'36 boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Grandma''s Boysenberry Spread', N'Esta es una descripción tonta', 25.0000, N'12 - 8 oz jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Uncle Bob''s Organic Dried Pears', N'Esta es una descripción tonta', 30.0000, N'12 - 1 lb pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Northwoods Cranberry Sauce', N'Esta es una descripción tonta', 40.0000, N'12 - 12 oz jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Mishi Kobe Niku', N'Esta es una descripción tonta', 97.0000, N'18 - 500 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Ikura', N'Esta es una descripción tonta', 31.0000, N'12 - 200 ml jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Queso Cabrales', N'Esta es una descripción tonta', 21.0000, N'1 kg pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Queso Manchego La Pastora', N'Esta es una descripción tonta', 38.0000, N'10 - 500 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Konbu', N'Esta es una descripción tonta', 6.0000, N'2 kg box', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Tofu', N'Esta es una descripción tonta', 23.2500, N'40 - 100 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Genen Shouyu', N'Esta es una descripción tonta', 15.5000, N'24 - 250 ml bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Pavlova', N'Esta es una descripción tonta', 17.4500, N'32 - 500 g boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Alice Mutton', N'Esta es una descripción tonta', 39.0000, N'20 - 1 kg tins', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Carnarvon Tigers', N'Esta es una descripción tonta', 62.5000, N'16 kg pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Teatime Chocolate Biscuits', N'Esta es una descripción tonta', 9.2000, N'10 boxes x 12 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Sir Rodney''s Marmalade', N'Esta es una descripción tonta', 81.0000, N'30 gift boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Sir Rodney''s Scones', N'Esta es una descripción tonta', 10.0000, N'24 pkgs. x 4 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gustaf''s Knäckebröd', N'Esta es una descripción tonta', 21.0000, N'24 - 500 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Tunnbröd', N'Esta es una descripción tonta', 9.0000, N'12 - 250 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Guaraná Fantástica', N'Esta es una descripción tonta', 4.5000, N'12 - 355 ml cans', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'NuNuCa Nuß-Nougat-Creme', N'Esta es una descripción tonta', 14.0000, N'20 - 450 g glasses', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gumbär Gummibärchen', N'Esta es una descripción tonta', 31.2300, N'100 - 250 g bags', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Schoggi Schokolade', N'Esta es una descripción tonta', 43.9000, N'100 - 100 g pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Rössle Sauerkraut', N'Esta es una descripción tonta', 45.6000, N'25 - 825 g cans', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Thüringer Rostbratwurst', N'Esta es una descripción tonta', 123.7900, N'50 bags x 30 sausgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Nord-Ost Matjeshering', N'Esta es una descripción tonta', 25.8900, N'10 - 200 g glasses', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gorgonzola Telino', N'Esta es una descripción tonta', 12.5000, N'12 - 100 g pkgs', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Mascarpone Fabioli', N'Esta es una descripción tonta', 32.0000, N'24 - 200 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Geitost', N'Esta es una descripción tonta', 2.5000, N'500 g', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Sasquatch Ale', N'Esta es una descripción tonta', 14.0000, N'24 - 12 oz bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Steeleye Stout', N'Esta es una descripción tonta', 18.0000, N'24 - 12 oz bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Inlagd Sill', N'Esta es una descripción tonta', 19.0000, N'24 - 250 g  jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gravad lax', N'Esta es una descripción tonta', 26.0000, N'12 - 500 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Côte de Blaye', N'Esta es una descripción tonta', 263.5000, N'12 - 75 cl bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Chartreuse verte', N'Esta es una descripción tonta', 18.0000, N'750 cc per bottle', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Boston Crab Meat', N'Esta es una descripción tonta', 18.4000, N'24 - 4 oz tins', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Jack''s New England Clam Chowder', N'Esta es una descripción tonta', 9.6500, N'12 - 12 oz cans', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Singaporean Hokkien Fried Mee', N'Esta es una descripción tonta', 14.0000, N'32 - 1 kg pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Ipoh Coffee', N'Esta es una descripción tonta', 46.0000, N'16 - 500 g tins', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gula Malacca', N'Esta es una descripción tonta', 19.4500, N'20 - 2 kg bags', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Rogede sild', N'Esta es una descripción tonta', 9.5000, N'1k pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Spegesild', N'Esta es una descripción tonta', 12.0000, N'4 - 450 g glasses', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Zaanse koeken', N'Esta es una descripción tonta', 9.5000, N'10 - 4 oz boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Chocolade', N'Esta es una descripción tonta', 12.7500, N'10 pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Maxilaku', N'Esta es una descripción tonta', 20.0000, N'24 - 50 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Valkoinen suklaa', N'Esta es una descripción tonta', 16.2500, N'12 - 100 g bars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Manjimup Dried Apples', N'Esta es una descripción tonta', 53.0000, N'50 - 300 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Filo Mix', N'Esta es una descripción tonta', 7.0000, N'16 - 2 kg boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Perth Pasties', N'Esta es una descripción tonta', 32.8000, N'48 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Tourtière', N'Esta es una descripción tonta', 7.4500, N'16 pies', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Pâté chinois', N'Esta es una descripción tonta', 24.0000, N'24 boxes x 2 pies', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gnocchi di nonna Alice', N'Esta es una descripción tonta', 38.0000, N'24 - 250 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Ravioli Angelo', N'Esta es una descripción tonta', 19.5000, N'24 - 250 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Escargots de Bourgogne', N'Esta es una descripción tonta', 13.2500, N'24 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Raclette Courdavault', N'Esta es una descripción tonta', 55.0000, N'5 kg pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Camembert Pierrot', N'Esta es una descripción tonta', 34.0000, N'15 - 300 g rounds', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Sirop d''érable', N'Esta es una descripción tonta', 28.5000, N'24 - 500 ml bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Tarte au sucre', N'Esta es una descripción tonta', 49.3000, N'48 pies', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Vegie-spread', N'Esta es una descripción tonta', 43.9000, N'15 - 625 g jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Wimmers gute Semmelknödel', N'Esta es una descripción tonta', 33.2500, N'20 bags x 4 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Louisiana Fiery Hot Pepper Sauce', N'Esta es una descripción tonta', 21.0500, N'32 - 8 oz bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Louisiana Hot Spiced Okra', N'Esta es una descripción tonta', 17.0000, N'24 - 8 oz jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Laughing Lumberjack Lager', N'Esta es una descripción tonta', 14.0000, N'24 - 12 oz bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Scottish Longbreads', N'Esta es una descripción tonta', 12.5000, N'10 boxes x 8 pieces', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Gudbrandsdalsost', N'Esta es una descripción tonta', 36.0000, N'10 kg pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Outback Lager', N'Esta es una descripción tonta', 15.0000, N'24 - 355 ml bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Flotemysost', N'Esta es una descripción tonta', 21.5000, N'10 - 500 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Mozzarella di Giovanni', N'Esta es una descripción tonta', 34.8000, N'24 - 200 g pkgs.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Röd Kaviar', N'Esta es una descripción tonta', 15.0000, N'24 - 150 g jars', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Longlife Tofu', N'Esta es una descripción tonta', 10.0000, N'5 kg pkg.', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Rhönbräu Klosterbier', N'Esta es una descripción tonta', 7.7500, N'24 - 0.5 l bottles', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Lakkalikööri', N'Esta es una descripción tonta', 18.0000, N'500 ml', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())
UNION ALL SELECT NEWID(), N'Original Frankfurter grüne Soße', N'Esta es una descripción tonta', 13.0000, N'12 boxes', (SELECT TOP 1 Id FROM @CategoriasGuids ORDER BY NEWID())

GO

-------------------------------------------------------- Inserción de Proveedores ----------------------------------------------------------------------------------------------
INSERT [dbo].[Proveedores] ([Id], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) 
VALUES (NEWID(), N'Exotic Liquids', N'49 Gilbert St., London, UK', N'028-312', N'david.santesteban.herrero@navarra.es', N'(171) 555-2222')
INSERT [dbo].[Proveedores] ([Id], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) 
VALUES (NEWID(), N'New Orleans Cajun Delights', N'P.O. Box 78934, New Orleans, LA, USA', N'70117', N'david.santesteban.herrero@navarra.es', N'(100) 555-4822')
INSERT [dbo].[Proveedores] ([Id], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) 
VALUES (NEWID(), N'Grandma Kelly''s Homestead', N'707 Oxford Rd., Ann Arbor, MI, USA', N'48104', N'david.santesteban.herrero@navarra.es', N'(313) 555-5735')
INSERT [dbo].[Proveedores] ([Id], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) 
VALUES (NEWID(), N'Tokyo Traders', N'9-8 Sekimai Musashino-shi, Tokio, Japan', N'100', N'david.santesteban.herrero@navarra.es', N'(03) 3555-5011')
INSERT [dbo].[Proveedores] ([Id], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) 
VALUES (NEWID(), N'Cooperativa de Quesos ''Las Cabras''', N'Calle del Rosal 4, Oviedo, Spain', N'33007', N'david.santesteban.herrero@navarra.es', N'(98) 598 76 54')

GO

-------------------------------------------------------- Inserción de Pedidos ----------------------------------------------------------------------------------------------

-- Declaración de variables para fechas
DECLARE @MinFechaAlta DATETIME = '2023-01-01';
DECLARE @MaxFechaAlta DATETIME = '2023-10-27';
DECLARE @MinFechaEnvio DATETIME = '2023-01-01';
DECLARE @MaxFechaEnvio DATETIME = '2023-10-27';
DECLARE @MinFechaEntrega DATETIME = '2023-01-01';
DECLARE @MaxFechaEntrega DATETIME = '2023-10-27';
DECLARE @MinFechaCancelacion DATETIME = '2023-01-01';
DECLARE @MaxFechaCancelacion DATETIME = '2023-10-27';

-- Variable para contar el número de pedidos
DECLARE @Contador INT = 0;

-- Inicio de la transacción
BEGIN TRANSACTION;

-- Bucle para insertar 100 pedidos de manera aleatoria
WHILE @Contador < 100
BEGIN
    -- Genera valores aleatorios para las fechas
    DECLARE @FechaAltaAleatoria DATETIME = DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, @MinFechaAlta, @MaxFechaAlta), @MinFechaAlta);
    DECLARE @FechaEnvioAleatoria DATETIME = CASE WHEN RAND() < 0.5 THEN NULL ELSE DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, @MinFechaEnvio, @MaxFechaEnvio), @MinFechaEnvio) END;
    DECLARE @FechaEntregaAleatoria DATETIME = CASE WHEN RAND() < 0.5 THEN NULL ELSE DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, @MinFechaEntrega, @MaxFechaEntrega), @MinFechaEntrega) END;
    DECLARE @FechaCancelacionAleatoria DATETIME = CASE WHEN RAND() < 0.5 THEN NULL ELSE DATEADD(SECOND, ABS(CHECKSUM(NEWID())) % DATEDIFF(SECOND, @MinFechaCancelacion, @MaxFechaCancelacion), @MinFechaCancelacion) END;

    -- Genera un IdProveedor aleatorio
    DECLARE @IdProveedorAleatorio UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Proveedores ORDER BY NEWID());

    -- Inserta el pedido con los valores aleatorios
    INSERT INTO Pedidos (Id, IdProveedor, FechaAlta, FechaEnvio, FechaEntrega, FechaCancelacion)
    VALUES (NEWID(), @IdProveedorAleatorio, @FechaAltaAleatoria, @FechaEnvioAleatoria, @FechaEntregaAleatoria, @FechaCancelacionAleatoria);

    -- Incrementa el contador
    SET @Contador = @Contador + 1;
END;

-- Confirmación de la transacción
COMMIT;

-------------------------------------------------------- Inserción de Detalles de Pedidos ----------------------------------------------------------------------------------------------
-- Variable para contar el número de detalles de pedidos
SET @Contador = 0;

-- Inicio de la transacción
BEGIN TRANSACTION;

-- Bucle para insertar 1000 detalles de pedidos de manera aleatoria
WHILE @Contador < 1000
BEGIN
    -- Genera un IdPedido aleatorio
    DECLARE @IdPedidoAleatorio UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Pedidos ORDER BY NEWID());

    -- Genera un IdProducto aleatorio
    DECLARE @IdProductoAleatorio UNIQUEIDENTIFIER = (SELECT TOP 1 Id FROM Productos ORDER BY NEWID());

    -- Genera un número aleatorio de unidades (por ejemplo, entre 1 y 10)
    DECLARE @UnidadesAleatorias INT = ABS(CHECKSUM(NEWID())) % 10 + 1;

    -- Inserta el detalle del pedido con los valores aleatorios
    INSERT INTO PedidosDetalle (IdPedido, IdProducto, Unidades)
    VALUES (@IdPedidoAleatorio, @IdProductoAleatorio, @UnidadesAleatorias);

    -- Incrementa el contador
    SET @Contador = @Contador + 1;
END;

-- Confirmación de la transacción
COMMIT;
