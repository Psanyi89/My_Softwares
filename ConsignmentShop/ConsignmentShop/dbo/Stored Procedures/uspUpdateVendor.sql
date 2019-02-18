CREATE PROCEDURE [dbo].[uspUpdateVendor]
	@VendorId int,
	@FirstName nvarchar(max)= null,
	@LastName nvarchar(max) = null,
	@Comission int=null
AS
	begin
	update Vendors set 
FirstName=ISNULL(@FirstName,FirstName),
LastName=ISNULL(@LastName,LastName),
Comission=ISNULL(@Comission,Comission)
where @VendorId=VendorId

end
