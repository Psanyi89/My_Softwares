CREATE PROCEDURE [dbo].[uspDeleteStore]
	@StoreId int

AS
begin
delete from ItemsInStore where StoreId=@StoreId;
delete from Stores where StoreId=@StoreId
 end
