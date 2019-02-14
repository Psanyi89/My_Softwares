CREATE TABLE [dbo].[Items] (
    [ItemId]             INT            IDENTITY (1, 1) NOT NULL,
    [Title]              NVARCHAR (MAX) NOT NULL,
    [Description]        NVARCHAR (MAX) DEFAULT ('No Detailes') NULL,
    [Price]              MONEY          NOT NULL,
    [Sold]               BIT            NOT NULL,
    [PaymentDistributed] BIT            NOT NULL,
    [Owner]              INT            NOT NULL,
    PRIMARY KEY CLUSTERED ([ItemId] ASC),
    CONSTRAINT [FK_Items_Vendors] FOREIGN KEY ([Owner]) REFERENCES [dbo].[Vendors] ([VendorId])
);





