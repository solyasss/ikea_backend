CREATE DATABASE [ikea-back];
GO

USE [ikea-back];
GO

-----------------------------
--Categories
-----------------------------
CREATE TABLE [dbo].[Categories]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ParentId] INT NULL,
    [Title] NVARCHAR(255) NOT NULL,
    [Slug] NVARCHAR(255) NOT NULL,   
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE()),
    [UpdatedDate] DATETIME2 NULL
);
GO

ALTER TABLE [dbo].[Categories]
ADD CONSTRAINT [FK_Categories_Parent]
    FOREIGN KEY ([ParentId])
    REFERENCES [dbo].[Categories]([Id])
    ON DELETE NO ACTION;
GO



-----------------------------
--Products
-----------------------------
CREATE TABLE [dbo].[Products]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [CategoryId] INT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,       
    [Slug] NVARCHAR(255) NOT NULL,       
    [Price] DECIMAL(10,2) NOT NULL DEFAULT(0),
    [MainImage] NVARCHAR(500) NULL,      
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE()),
    [UpdatedDate] DATETIME2 NULL
);
GO

-- Внешний ключ на Categories:
ALTER TABLE [dbo].[Products]
ADD CONSTRAINT [FK_Products_Categories]
    FOREIGN KEY ([CategoryId])
    REFERENCES [dbo].[Categories]([Id])
    ON DELETE CASCADE;
GO

-----------------------------
--ProductCharacteristics
-----------------------------
CREATE TABLE [dbo].[ProductCharacteristics]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProductId] INT NOT NULL,
    [Name] NVARCHAR(255) NOT NULL,     
    [Value] NVARCHAR(255) NOT NULL,     
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE())
);
GO

ALTER TABLE [dbo].[ProductCharacteristics]
ADD CONSTRAINT [FK_ProductCharacteristics_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([Id])
    ON DELETE CASCADE;
GO

-----------------------------
--ProductImages
-----------------------------
CREATE TABLE [dbo].[ProductImages]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProductId] INT NOT NULL,
    [ImageUrl] NVARCHAR(500) NOT NULL,  
    [SortOrder] INT NOT NULL DEFAULT(0), 
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE())
);
GO

ALTER TABLE [dbo].[ProductImages]
ADD CONSTRAINT [FK_ProductImages_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([Id])
    ON DELETE CASCADE;
GO

-----------------------------
--ProductComments
-----------------------------
CREATE TABLE [dbo].[ProductComments]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [ProductId] INT NOT NULL,
    [UserName] NVARCHAR(255) NULL,       
    [CommentText] NVARCHAR(MAX) NOT NULL,
    [Rating] INT NULL,                   
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE())
);
GO

ALTER TABLE [dbo].[ProductComments]
ADD CONSTRAINT [FK_ProductComments_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([Id])
    ON DELETE CASCADE;
GO

-----------------------------
--Sets + SetItems
-----------------------------
CREATE TABLE [dbo].[Sets]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [Name] NVARCHAR(255) NOT NULL,        
    [Slug] NVARCHAR(255) NULL,           
    [ImageUrl] NVARCHAR(500) NULL,      
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE()),
    [UpdatedDate] DATETIME2 NULL
);
GO

CREATE TABLE [dbo].[SetItems]
(
    [Id] INT IDENTITY(1,1) NOT NULL PRIMARY KEY,
    [SetId] INT NOT NULL,
    [ProductId] INT NOT NULL,
    [Quantity] INT NOT NULL DEFAULT(1),  
    [CreatedDate] DATETIME2 NOT NULL DEFAULT(GETUTCDATE())
);
GO

ALTER TABLE [dbo].[SetItems]
ADD CONSTRAINT [FK_SetItems_Sets]
    FOREIGN KEY ([SetId])
    REFERENCES [dbo].[Sets]([Id])
    ON DELETE CASCADE;

ALTER TABLE [dbo].[SetItems]
ADD CONSTRAINT [FK_SetItems_Products]
    FOREIGN KEY ([ProductId])
    REFERENCES [dbo].[Products]([Id])
    ON DELETE CASCADE;
GO

-----------------------------
--TEST INSERT
-----------------------------
INSERT INTO [dbo].[Categories] ([ParentId], [Title], [Slug])
VALUES (NULL, N'Гостиная', N'living-room');

INSERT INTO [dbo].[Categories] ([ParentId], [Title], [Slug])
VALUES 
(1, N'Диваны', N'sofas'),
(1, N'Кресла', N'armchairs'),
(1, N'Журнальные столики', N'coffee-tables'),
(1, N'Шкафы и полки', N'wardrobes-and-shelves'),
(1, N'Стеллажи для ТВ', N'tv-stands'),
(1, N'Ковры', N'carpets'),
(1, N'Освещение', N'lighting'),
(1, N'Декор', N'decor'),
(1, N'Системы хранения', N'storage');

-----------------------------
--TEST SELECT
-----------------------------
SELECT * FROM Categories

SELECT * FROM Categories
-----------------------------
--
-----------------------------

