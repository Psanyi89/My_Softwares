CREATE TABLE [dbo].[Stores] (
    [StoreId] INT            IDENTITY (1, 1) NOT NULL,
    [Name]    NVARCHAR (MAX) NOT NULL,
    PRIMARY KEY CLUSTERED ([StoreId] ASC)
);

