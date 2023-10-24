USE [TiendaDb]
GO

delete [LineasPedidos]
delete [Pedidos]
delete [Productos]
delete [CategoriasProductos]
delete [Proveedores]

-- Encuentra el valor máximo actual en la columna de identidad
SELECT MAX(IdCategoriaProducto) FROM CategoriasProductos;
-- Reiniciar el valor de identidad a 1
DBCC CHECKIDENT ('CategoriasProductos', RESEED, 1);

SELECT MAX(IdProducto) FROM Productos;
DBCC CHECKIDENT ('Productos', RESEED, 1);

SELECT MAX(IdProveedor) FROM Proveedores;
DBCC CHECKIDENT ('Proveedores', RESEED, 1);

SELECT MAX(IdPedido) FROM Pedidos;
DBCC CHECKIDENT ('Pedidos', RESEED, 1);

-------------------------------------------------------- Inserción de Categorías ----------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[CategoriasProductos] ON 
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (1, N'Beverages', N'Soft drinks, coffees, teas, beers, and ales')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (2, N'Condiments', N'Sweet and savory sauces, relishes, spreads, and seasonings')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (3, N'Confections', N'Desserts, candies, and sweet breads')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (4, N'Dairy Products', N'Cheeses')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (5, N'Grains/Cereals', N'Breads, crackers, pasta, and cereal')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (6, N'Meat/Poultry', N'Prepared meats')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (7, N'Produce', N'Dried fruit and bean curd')
INSERT [dbo].[CategoriasProductos] ([IdCategoriaProducto], [Nombre], [Descripcion]) VALUES (8, N'Seafood', N'Seaweed and fish')
SET IDENTITY_INSERT [dbo].[CategoriasProductos] OFF
GO

-------------------------------------------------------- Inserción de Productos ----------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Productos] ON 
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (1, N'Chai', N'Esta es una descripción tonta', 18.0000, N'10 boxes x 20 bags', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (2, N'Chang', N'Esta es una descripción tonta', 19.0000, N'24 - 12 oz bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (3, N'Aniseed Syrup', N'Esta es una descripción tonta', 10.0000, N'12 - 550 ml bottles', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (4, N'Chef Anton''s Cajun Seasoning', N'Esta es una descripción tonta', 22.0000, N'48 - 6 oz jars', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (5, N'Chef Anton''s Gumbo Mix', N'Esta es una descripción tonta', 21.3500, N'36 boxes', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (6, N'Grandma''s Boysenberry Spread', N'Esta es una descripción tonta', 25.0000, N'12 - 8 oz jars', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (7, N'Uncle Bob''s Organic Dried Pears', N'Esta es una descripción tonta', 30.0000, N'12 - 1 lb pkgs.', 7)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (8, N'Northwoods Cranberry Sauce', N'Esta es una descripción tonta', 40.0000, N'12 - 12 oz jars', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (9, N'Mishi Kobe Niku', N'Esta es una descripción tonta', 97.0000, N'18 - 500 g pkgs.', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (10, N'Ikura', N'Esta es una descripción tonta', 31.0000, N'12 - 200 ml jars', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (11, N'Queso Cabrales', N'Esta es una descripción tonta', 21.0000, N'1 kg pkg.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (12, N'Queso Manchego La Pastora', N'Esta es una descripción tonta', 38.0000, N'10 - 500 g pkgs.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (13, N'Konbu', N'Esta es una descripción tonta', 6.0000, N'2 kg box', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (14, N'Tofu', N'Esta es una descripción tonta', 23.2500, N'40 - 100 g pkgs.', 7)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (15, N'Genen Shouyu', N'Esta es una descripción tonta', 15.5000, N'24 - 250 ml bottles', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (16, N'Pavlova', N'Esta es una descripción tonta', 17.4500, N'32 - 500 g boxes', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (17, N'Alice Mutton', N'Esta es una descripción tonta', 39.0000, N'20 - 1 kg tins', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (18, N'Carnarvon Tigers', N'Esta es una descripción tonta', 62.5000, N'16 kg pkg.', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (19, N'Teatime Chocolate Biscuits', N'Esta es una descripción tonta', 9.2000, N'10 boxes x 12 pieces', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (20, N'Sir Rodney''s Marmalade', N'Esta es una descripción tonta', 81.0000, N'30 gift boxes', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (21, N'Sir Rodney''s Scones', N'Esta es una descripción tonta', 10.0000, N'24 pkgs. x 4 pieces', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (22, N'Gustaf''s Knäckebröd', N'Esta es una descripción tonta', 21.0000, N'24 - 500 g pkgs.', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (23, N'Tunnbröd', N'Esta es una descripción tonta', 9.0000, N'12 - 250 g pkgs.', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (24, N'Guaraná Fantástica', N'Esta es una descripción tonta', 4.5000, N'12 - 355 ml cans', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (25, N'NuNuCa Nuß-Nougat-Creme', N'Esta es una descripción tonta', 14.0000, N'20 - 450 g glasses', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (26, N'Gumbär Gummibärchen', N'Esta es una descripción tonta', 31.2300, N'100 - 250 g bags', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (27, N'Schoggi Schokolade', N'Esta es una descripción tonta', 43.9000, N'100 - 100 g pieces', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (28, N'Rössle Sauerkraut', N'Esta es una descripción tonta', 45.6000, N'25 - 825 g cans', 7)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (29, N'Thüringer Rostbratwurst', N'Esta es una descripción tonta', 123.7900, N'50 bags x 30 sausgs.', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (30, N'Nord-Ost Matjeshering', N'Esta es una descripción tonta', 25.8900, N'10 - 200 g glasses', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (31, N'Gorgonzola Telino', N'Esta es una descripción tonta', 12.5000, N'12 - 100 g pkgs', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (32, N'Mascarpone Fabioli', N'Esta es una descripción tonta', 32.0000, N'24 - 200 g pkgs.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (33, N'Geitost', N'Esta es una descripción tonta', 2.5000, N'500 g', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (34, N'Sasquatch Ale', N'Esta es una descripción tonta', 14.0000, N'24 - 12 oz bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (35, N'Steeleye Stout', N'Esta es una descripción tonta', 18.0000, N'24 - 12 oz bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (36, N'Inlagd Sill', N'Esta es una descripción tonta', 19.0000, N'24 - 250 g  jars', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (37, N'Gravad lax', N'Esta es una descripción tonta', 26.0000, N'12 - 500 g pkgs.', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (38, N'Côte de Blaye', N'Esta es una descripción tonta', 263.5000, N'12 - 75 cl bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (39, N'Chartreuse verte', N'Esta es una descripción tonta', 18.0000, N'750 cc per bottle', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (40, N'Boston Crab Meat', N'Esta es una descripción tonta', 18.4000, N'24 - 4 oz tins', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (41, N'Jack''s New England Clam Chowder', N'Esta es una descripción tonta', 9.6500, N'12 - 12 oz cans', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (42, N'Singaporean Hokkien Fried Mee', N'Esta es una descripción tonta', 14.0000, N'32 - 1 kg pkgs.', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (43, N'Ipoh Coffee', N'Esta es una descripción tonta', 46.0000, N'16 - 500 g tins', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (44, N'Gula Malacca', N'Esta es una descripción tonta', 19.4500, N'20 - 2 kg bags', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (45, N'Rogede sild', N'Esta es una descripción tonta', 9.5000, N'1k pkg.', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (46, N'Spegesild', N'Esta es una descripción tonta', 12.0000, N'4 - 450 g glasses', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (47, N'Zaanse koeken', N'Esta es una descripción tonta', 9.5000, N'10 - 4 oz boxes', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (48, N'Chocolade', N'Esta es una descripción tonta', 12.7500, N'10 pkgs.', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (49, N'Maxilaku', N'Esta es una descripción tonta', 20.0000, N'24 - 50 g pkgs.', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (50, N'Valkoinen suklaa', N'Esta es una descripción tonta', 16.2500, N'12 - 100 g bars', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (51, N'Manjimup Dried Apples', N'Esta es una descripción tonta', 53.0000, N'50 - 300 g pkgs.', 7)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (52, N'Filo Mix', N'Esta es una descripción tonta', 7.0000, N'16 - 2 kg boxes', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (53, N'Perth Pasties', N'Esta es una descripción tonta', 32.8000, N'48 pieces', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (54, N'Tourtière', N'Esta es una descripción tonta', 7.4500, N'16 pies', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (55, N'Pâté chinois', N'Esta es una descripción tonta', 24.0000, N'24 boxes x 2 pies', 6)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (56, N'Gnocchi di nonna Alice', N'Esta es una descripción tonta', 38.0000, N'24 - 250 g pkgs.', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (57, N'Ravioli Angelo', N'Esta es una descripción tonta', 19.5000, N'24 - 250 g pkgs.', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (58, N'Escargots de Bourgogne', N'Esta es una descripción tonta', 13.2500, N'24 pieces', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (59, N'Raclette Courdavault', N'Esta es una descripción tonta', 55.0000, N'5 kg pkg.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (60, N'Camembert Pierrot', N'Esta es una descripción tonta', 34.0000, N'15 - 300 g rounds', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (61, N'Sirop d''érable', N'Esta es una descripción tonta', 28.5000, N'24 - 500 ml bottles', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (62, N'Tarte au sucre', N'Esta es una descripción tonta', 49.3000, N'48 pies', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (63, N'Vegie-spread', N'Esta es una descripción tonta', 43.9000, N'15 - 625 g jars', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (64, N'Wimmers gute Semmelknödel', N'Esta es una descripción tonta', 33.2500, N'20 bags x 4 pieces', 5)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (65, N'Louisiana Fiery Hot Pepper Sauce', N'Esta es una descripción tonta', 21.0500, N'32 - 8 oz bottles', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (66, N'Louisiana Hot Spiced Okra', N'Esta es una descripción tonta', 17.0000, N'24 - 8 oz jars', 2)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (67, N'Laughing Lumberjack Lager', N'Esta es una descripción tonta', 14.0000, N'24 - 12 oz bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (68, N'Scottish Longbreads', N'Esta es una descripción tonta', 12.5000, N'10 boxes x 8 pieces', 3)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (69, N'Gudbrandsdalsost', N'Esta es una descripción tonta', 36.0000, N'10 kg pkg.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (70, N'Outback Lager', N'Esta es una descripción tonta', 15.0000, N'24 - 355 ml bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (71, N'Flotemysost', N'Esta es una descripción tonta', 21.5000, N'10 - 500 g pkgs.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (72, N'Mozzarella di Giovanni', N'Esta es una descripción tonta', 34.8000, N'24 - 200 g pkgs.', 4)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (73, N'Röd Kaviar', N'Esta es una descripción tonta', 15.0000, N'24 - 150 g jars', 8)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (74, N'Longlife Tofu', N'Esta es una descripción tonta', 10.0000, N'5 kg pkg.', 7)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (75, N'Rhönbräu Klosterbier', N'Esta es una descripción tonta', 7.7500, N'24 - 0.5 l bottles', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (76, N'Lakkalikööri', N'Esta es una descripción tonta', 18.0000, N'500 ml', 1)
INSERT [dbo].[Productos] ([IdProducto], [Nombre], [Descripcion], [PrecioUnitario], [CantidadPorUnidad], [IdCategoriaProducto]) VALUES (77, N'Original Frankfurter grüne Soße', N'Esta es una descripción tonta', 13.0000, N'12 boxes', 2)
SET IDENTITY_INSERT [dbo].[Productos] OFF
GO

-------------------------------------------------------- Inserción de Proveedores ----------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Proveedores] ON 
INSERT [dbo].[Proveedores] ([IdProveedor], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) VALUES (1, N'Exotic Liquids', N'49 Gilbert St., London, UK', N'028-312', N'david.santesteban.herrero@navarra.es', N'(171) 555-2222')
INSERT [dbo].[Proveedores] ([IdProveedor], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) VALUES (2, N'New Orleans Cajun Delights', N'P.O. Box 78934, New Orleans, LA, USA', N'70117', N'david.santesteban.herrero@navarra.es', N'(100) 555-4822')
INSERT [dbo].[Proveedores] ([IdProveedor], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) VALUES (3, N'Grandma Kelly''s Homestead', N'707 Oxford Rd., Ann Arbor, MI, USA', N'48104', N'david.santesteban.herrero@navarra.es', N'(313) 555-5735')
INSERT [dbo].[Proveedores] ([IdProveedor], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) VALUES (4, N'Tokyo Traders', N'9-8 Sekimai Musashino-shi, Tokio, Japan', N'100', N'david.santesteban.herrero@navarra.es', N'(03) 3555-5011')
INSERT [dbo].[Proveedores] ([IdProveedor], [Nombre], [Direccion], [CodigoPostal], [Email], [Telefono]) VALUES (5, N'Cooperativa de Quesos ''Las Cabras''', N'Calle del Rosal 4, Oviedo, Spain', N'33007', N'david.santesteban.herrero@navarra.es', N'(98) 598 76 54')
SET IDENTITY_INSERT [dbo].[Proveedores] OFF
GO

-------------------------------------------------------- Inserción de Pedidos ----------------------------------------------------------------------------------------------
SET IDENTITY_INSERT [dbo].[Pedidos] ON 
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(1, 3, '2023-04-06T00:00:00.000', '2023-04-22T00:00:00.000', '2023-05-07T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(2, 2, '2023-04-07T00:00:00.000', '2023-04-23T00:00:00.000', '2023-05-08T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(3, 4, '2023-04-08T00:00:00.000', '2023-04-24T00:00:00.000', '2023-05-09T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(4, 1, '2023-04-09T00:00:00.000', '2023-04-25T00:00:00.000', '2023-05-10T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(5, 5, '2023-04-10T00:00:00.000', '2023-04-26T00:00:00.000', '2023-05-11T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(6, 3, '2023-04-11T00:00:00.000', '2023-04-27T00:00:00.000', '2023-05-12T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(7, 2, '2023-04-12T00:00:00.000', '2023-04-28T00:00:00.000', '2023-05-13T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(8, 4, '2023-04-13T00:00:00.000', '2023-04-29T00:00:00.000', '2023-05-14T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(9, 1, '2023-04-14T00:00:00.000', '2023-04-30T00:00:00.000', '2023-05-15T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(10, 5, '2023-04-15T00:00:00.000', '2023-05-01T00:00:00.000', '2023-05-16T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(11, 3, '2023-04-16T00:00:00.000', '2023-05-02T00:00:00.000', '2023-05-17T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(12, 2, '2023-04-17T00:00:00.000', '2023-05-03T00:00:00.000', '2023-05-18T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(13, 4, '2023-04-18T00:00:00.000', '2023-05-04T00:00:00.000', '2023-05-19T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(14, 1, '2023-04-19T00:00:00.000', '2023-05-05T00:00:00.000', '2023-05-20T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(15, 5, '2023-04-20T00:00:00.000', '2023-05-06T00:00:00.000', '2023-05-21T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(16, 3, '2023-04-21T00:00:00.000', '2023-05-07T00:00:00.000', '2023-05-22T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(17, 2, '2023-04-22T00:00:00.000', '2023-05-08T00:00:00.000', '2023-05-23T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(18, 4, '2023-04-23T00:00:00.000', '2023-05-09T00:00:00.000', '2023-05-24T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(19, 1, '2023-04-24T00:00:00.000', '2023-05-10T00:00:00.000', '2023-05-25T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(20, 5, '2023-04-25T00:00:00.000', '2023-05-11T00:00:00.000', '2023-05-26T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(21, 3, '2023-04-26T00:00:00.000', '2023-05-12T00:00:00.000', '2023-05-27T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(22, 2, '2023-04-27T00:00:00.000', '2023-05-13T00:00:00.000', '2023-05-28T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(23, 4, '2023-04-28T00:00:00.000', '2023-05-14T00:00:00.000', '2023-05-29T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(24, 1, '2023-04-29T00:00:00.000', '2023-05-15T00:00:00.000', '2023-05-30T00:00:00.000', NULL)
INSERT INTO [dbo].[Pedidos] ([IdPedido], [IdProveedor], [FechaAlta], [FechaEnvio], [FechaEntrega], [FechaCancelacion]) VALUES(25, 5, '2023-04-30T00:00:00.000', '2023-05-16T00:00:00.000', '2023-05-31T00:00:00.000', NULL)
SET IDENTITY_INSERT [dbo].[Pedidos] OFF
GO

-------------------------------------------------------- Inserción de Líneas de los Pedidos ----------------------------------------------------------------------------------------------
--NOTA: este script al generar datos aleatoriamente puede que falle en algún registro, pero generará cientos de ellos

-- Generar 1000 líneas de pedido de ejemplo y agregarlas a la variable de tabla
DECLARE @i INT = 1
WHILE @i <= 1000
BEGIN
    INSERT INTO LineasPedidos (IdPedido, IdProducto, Unidades)
    VALUES (
        FLOOR(RAND() * 25) + 1, -- IdPedido entre 1 y 25
        FLOOR(RAND() * 77) + 1, -- IdProducto entre 1 y 77
        FLOOR(RAND() * 20) + 1  -- Unidades entre 1 y 20 (ajusta según tus necesidades)
    )
    SET @i = @i + 1
END
GO