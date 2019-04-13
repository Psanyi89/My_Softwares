CREATE TABLE [dbo].[Product]
(
	[Id] INT NOT NULL PRIMARY KEY identity, 
	[ProductName] NVARCHAR(100) NOT NULL, 
   [RetailPrice] Money not null,
   [Description] NVARCHAR(MAX) NOT NULL, 
	[CreateDate] DATETIME2 NOT NULL DEFAULT getutcdate(), 
	[LastModified] DATETIME2 NOT NULL DEFAULT getutcdate()
)
