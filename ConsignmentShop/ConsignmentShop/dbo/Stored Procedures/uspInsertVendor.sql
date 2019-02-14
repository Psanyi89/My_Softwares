CREATE PROCEDURE [dbo].[uspInsertVendor]
	@FirstName nvarchar(max),
	@LastName nvarchar(max),
	@Comission float(53)
AS
	begin
if exists(select * from Vendors where FirstName=@FirstName and LastName=@LastName)
begin
update Vendors set Commission=@Comission where FirstName=@FirstName and LastName=@LastName
end
else
begin
	Insert into Vendors (FirstName,LastName,Commission) values (@FirstName,@LastName,@Comission)
	end
end