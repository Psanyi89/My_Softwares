CREATE PROCEDURE [dbo].[uspInsertToStore]
	@StoreName nvarchar(max),
	@ItemName nvarchar(max)
AS
begin
declare @StoreId int
declare @ItemId int
select @StoreId = StoreId from Stores where Name=@StoreName

if(@StoreId is null)
begin
insert into Stores (Name) values (@StoreName)
select @StoreId= @@IDENTITY
end

select @ItemId= ItemId from Items where @ItemName = Title
insert into ItemsInStore (StoreId,ItemId) values (@StoreId,@ItemId)
end