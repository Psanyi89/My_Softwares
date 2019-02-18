CREATE PROCEDURE [dbo].[uspDeleteVendor]
@VendorId int
AS
begin
delete from Vendors where @VendorId=VendorId
end