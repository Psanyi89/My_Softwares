CREATE TABLE [dbo].[Vendors] (
    [VendorId]  INT            IDENTITY (1, 1) NOT NULL,
    [FirstName] NVARCHAR (MAX) NOT NULL,
    [LastName]  NVARCHAR (MAX) NOT NULL,
    [Comission] INT            NULL,
    PRIMARY KEY CLUSTERED ([VendorId] ASC)
);







