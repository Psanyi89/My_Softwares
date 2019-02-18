CREATE PROCEDURE [dbo].[uspUpdateItem]
		@ItemId int,
		@Title nvarchar(max)=null,
	@Description nvarchar(max) = null,
	@Price money =null,
	@Sold bit = null,
	@PaymentDistributed bit = null,
	@OwnerId int=null
AS
begin
update Items set
Title=ISNULL(@Title,Title),
Description=ISNULL(@Description,Description),
Price=ISNULL(@Price,Price),
Sold=ISNULL(@Sold,Sold),
PaymentDistributed=ISNULL(@PaymentDistributed,PaymentDistributed),
Owner=ISNULL(@OwnerId,Owner)
where @ItemId=ItemId
	end