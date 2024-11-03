CREATE PROCEDURE [dbo].[UpdateFlatRateTax] 
	@CountryId INT,
	@Income FLOAT,
	@ValidationMessage varchar(1000) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

    -- Insert statements for procedure here
	DECLARE @TaxRegimeCode varchar(20), @CountryTaxRegimeId INT

	SELECT @CountryTaxRegimeId=TaxRegimeId FROM Country where CountryId = @CountryId;
	SELECT @TaxRegimeCode= Code FROM TaxRegime where Id = @CountryTaxRegimeId

	IF(@TaxRegimeCode='FLAT')
	BEGIN
		IF(@Income=0)
		BEGIN
			DECLARE @FlatRateAmount DECIMAL(18, 2);
			DECLARE @FlatMinIncome DECIMAL(18, 2);

			-- Get flat rate values for the country
			SELECT @FlatRateAmount = Rate, @FlatMinIncome = LowerLimit
			FROM TaxBracketLine
			WHERE CountryId = @CountryId;

			-- Update tax using flat rate for all incomes in the specified country
			UPDATE TaxCalculation
			SET CalculatedTax = CASE WHEN Income > @FlatMinIncome THEN @FlatRateAmount ELSE 0 END,
			    NetPay = Income-CalculatedTax
			WHERE CountryId = @CountryId;
		END
		IF(@Income>0)
		BEGIN
			DECLARE @TDS FLOAT
			SELECT @TDS=CASE 
							WHEN LowerLimit < @Income THEN Rate
							ELSE 0
						END
			FROM TaxBracketLine WHERE CountryId = @CountryId;

			INSERT INTO TaxCalculation(CountryId, Income, CalculatedTax, NetPay)
			values
			(@CountryId, @Income, @TDS, @Income-@TDS)
		END
	END
	ELSE
	BEGIN
		SET @ValidationMessage='Please correct your countrys tax regime!'
	END
END
GO
