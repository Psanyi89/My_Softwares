CREATE PROCEDURE [dbo].[uspInsertItem]
	@Title nvarchar(max),
	@Description nvarchar(max) = null,
	@Price money ,
	@Sold bit = false,
	@PaymentDistributed bit = false,
	@Owner nvarchar(max)
	as
	begin
	declare @VendorId int
	Select @VendorId=VendorId from Vendors where @Owner= CONCAT(FirstName,' ',LastName);
	if(@VendorId>0)
	begin
	insert into Items 
	(Title,Description,Price,Sold,PaymentDistributed,Owner) 
	values 
	(@Title,@Description,@Price,@Sold,@PaymentDistributed,@VendorId)
	end
	else begin
	return 0;
	end
	end