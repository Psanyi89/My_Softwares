CREATE TABLE [dbo].[ItemsInStore] (
    [StoreId] INT NOT NULL,
    [ItemId]  INT NOT NULL,
    CONSTRAINT [PK_ItemsInStore] PRIMARY KEY CLUSTERED ([StoreId] ASC, [ItemId] ASC),
    FOREIGN KEY ([ItemId]) REFERENCES [dbo].[Items] ([ItemId]),
    FOREIGN KEY ([StoreId]) REFERENCES [dbo].[Stores] ([StoreId])
);



