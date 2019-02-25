CREATE PROCEDURE [dbo].[uspInsertToStore]
	@StoreName nvarchar(max),
	@ItemId int=null
AS
begin
declare @StoreId int
select @StoreId = StoreId from Stores where Name=@StoreName

if(@StoreId is null)
begin
insert into Stores (Name) values (@StoreName)
select @StoreId= @@IDENTITY
end
if(@ItemId is not null)
begin
insert into ItemsInStore (StoreId,ItemId) values (@StoreId,@ItemId)
end
end