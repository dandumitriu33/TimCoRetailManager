CREATE PROCEDURE [dbo].[spSale_Lookup]
	@CashierID nvarchar(128),
	@SaleDate datetime2
AS
BEGIN
	SET NOCOUNT ON;
	SELECT Id
	FROM dbo.Sale
	WHERE CashierId = @CashierID AND SaleDate = @SaleDate;
END