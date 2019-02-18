CREATE proc [dbo].[uspSelectAllVendor]
@FirstName nvarchar(max) = Null,
@LastName nvarchar(max) = null,
@Comission int = null
as 
begin
select * from Vendors 
where (CHARINDEX(@FirstName,FirstName)>0 or @FirstName is null) and
	  (CHARINDEX(@LastName,LastName)>0 or @LastName is null) and
	  (@Comission=Comission or @Comission is null)
	  end
