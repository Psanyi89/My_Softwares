CREATE PROCEDURE [dbo].[uspGetItemsInStore]
	@StoreId int
AS
begin
	SELECT * from ItemsInStore where StoreId=@StoreId
	end
