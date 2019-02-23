CREATE PROCEDURE [dbo].[uspGetStores]
	@Name nvarchar(max)=null

AS
begin
	SELECT * from Stores
	where (CHARINDEX(@Name,Name)>0 or @Name is null)
	end
