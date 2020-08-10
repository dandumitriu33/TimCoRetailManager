CREATE PROCEDURE [dbo].[spSale_Insert]
	@id int output,
	@CashierId nvarchar(128),
	@SaleDate datetime2,
	@SubTotal money,
	@Tax money,
	@Total money
AS
BEGIN
	set NOCOUNT ON;
	INSERT INTO dbo.Sale(CashierId, SaleDate, SubTotal, Tax, Total)
	values (@CashierId, @SaleDate, @SubTotal, @Tax, @Total);

	SELECT @Id = @@Identity;
END