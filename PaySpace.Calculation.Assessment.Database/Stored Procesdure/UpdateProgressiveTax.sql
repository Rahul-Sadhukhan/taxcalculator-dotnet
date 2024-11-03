CREATE PROCEDURE [dbo].[UpdateProgressiveTax]
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

	IF(@TaxRegimeCode='PROG')
	BEGIN
		IF(@Income=0)
		BEGIN
			WITH TC AS (
				SELECT 
					t.CountryId,
					SUM(
						CASE 
							WHEN i.Income > t.LowerLimit THEN 
								(CASE 
									WHEN t.UpperLimit IS NULL OR i.Income < t.UpperLimit 
									THEN i.Income 
									ELSE t.UpperLimit 
								END - t.LowerLimit) * (t.Rate / 100)
							ELSE 0 
						END
					) AS TotalTax
				FROM 
					TaxBracketLine t
				JOIN 
					TaxCalculation i ON i.CountryId = t.CountryId
				WHERE 
					i.Income > t.LowerLimit
				GROUP BY 
					t.CountryId
			)

			UPDATE i
			SET i.CalculatedTax = tc.TotalTax,
			i.NetPay = i.Income-i.CalculatedTax
			FROM TaxCalculation i
			JOIN TC tc ON i.CountryId = tc.CountryId
			WHERE i.CountryId = @CountryId;

		END
		IF(@Income>0)
		BEGIN
			DECLARE @TDS FLOAT
			SELECT @TDS=SUM(
						CASE 
							WHEN @Income > t.LowerLimit THEN 
								(CASE 
									WHEN t.UpperLimit IS NULL OR @Income < t.UpperLimit THEN @Income
									ELSE t.UpperLimit 
								END - t.LowerLimit) * (t.Rate / 100)
							ELSE 0 
						END
						)
			FROM TaxBracketLine t
			WHERE t.CountryId = @CountryId AND @Income > t.LowerLimit

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