CREATE PROCEDURE [dbo].[uspDeleteItemFromStore]
	@ItemId int,
	@StoreId int
AS
begin
	delete from ItemsInStore where StoreId=@StoreId and @ItemId=ItemId
end
