CREATE PROCEDURE [dbo].[uspUpdateStore]
	@StoreId int ,
	@StoreName nvarchar(max)
AS
begin
update Stores set Name=ISNULL(@StoreName,Name)
where StoreId=@StoreId
end
