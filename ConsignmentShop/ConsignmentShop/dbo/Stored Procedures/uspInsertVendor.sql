CREATE PROCEDURE [dbo].[uspInsertVendor]
	@FirstName nvarchar(max),
	@LastName nvarchar(max),
	@Comission int
AS
	begin
if exists(select * from Vendors where FirstName=@FirstName and LastName=@LastName)
begin
update Vendors set Comission=@Comission where FirstName=@FirstName and LastName=@LastName
end
else
begin
	Insert into Vendors (FirstName,LastName,Comission) values (@FirstName,@LastName,@Comission)
	end
end