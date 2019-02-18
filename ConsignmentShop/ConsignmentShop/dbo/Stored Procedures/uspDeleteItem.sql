CREATE PROCEDURE [dbo].[uspDeleteItem]
@ItemId int
AS
begin
delete from Items where @ItemId=ItemId
end