CREATE PROCEDURE [dbo].[UpdatePercentageTax] 
	@CountryId INT,
	@Income FLOAT,
	@ValidationMessage varchar(1000) OUTPUT
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	DECLARE @TaxRegimeCode varchar(20), @CountryTaxRegimeId INT

	SELECT @CountryTaxRegimeId=TaxRegimeId FROM Country where CountryId = @CountryId;
	SELECT @TaxRegimeCode= Code FROM TaxRegime where Id = @CountryTaxRegimeId

	IF(@TaxRegimeCode='PERC')
	BEGIN
		IF(@Income=0)
		BEGIN
			DECLARE @RateAmount DECIMAL(18, 2);
			DECLARE @MinIncome DECIMAL(18, 2);
			DECLARE @MaxIncome DECIMAL(18, 2);

			SELECT @RateAmount = Rate, @MinIncome = LowerLimit, @MaxIncome=UpperLimit
			FROM TaxBracketLine
			WHERE CountryId = @CountryId;

			UPDATE TaxCalculation
			SET CalculatedTax = CASE WHEN @MinIncome = -1 AND @MaxIncome = -1 THEN Income * 0.30 ELSE 0 END,
			NetPay=Income-CalculatedTax
			WHERE CountryId = @CountryId;
		END
		IF(@Income>0)
		BEGIN
			DECLARE @TDS FLOAT
			SELECT @TDS=CASE 
							WHEN LowerLimit =-1 AND UpperLimit=-1 THEN @Income * 0.30
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
