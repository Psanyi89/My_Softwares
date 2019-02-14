CREATE TABLE [dbo].[Vendors] (
    [VendorId]   INT            IDENTITY (1, 1) NOT NULL,
    [FirstName]  NVARCHAR (MAX) NOT NULL,
    [LastName]   NVARCHAR (MAX) NOT NULL,
    [Commission] FLOAT (53)     NOT NULL,
    PRIMARY KEY CLUSTERED ([VendorId] ASC)
);





