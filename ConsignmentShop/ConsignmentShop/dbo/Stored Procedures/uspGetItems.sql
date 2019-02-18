CREATE PROCEDURE [dbo].[uspGetItems]
	@Title nvarchar(max)=null,
	@Description nvarchar(max) = null,
	@Price money =null,
	@Sold bit =null,
	@PaymentDistributed bit = null,
	@OwnerId int=null
	as
	begin
	select * from Items where (CHARINDEX(@Title,Title)>0 or @Title is null) and
							  (CHARINDEX(@Description,Description)>0 or @Description is null) and
							  (@Price>0 or @Price is null) and
							  (@Sold=Sold or @Sold is null) and
							  (@PaymentDistributed=PaymentDistributed or @PaymentDistributed is null) and
							  (@OwnerId=Owner or @OwnerId is null)
							  end
