USE University
GO

IF OBJECT_ID(N'—ырье_1') IS NOT NULL
	DROP TABLE —ырье_1;

SELECT * INTO —ырье_1 FROM —ырье;

SELECT —ырье_1.Ќаим—ырь€ AS —ырье, “ипы_сырь€.Ќаим“ипа—ырь€ AS “ип FROM —ырье_1
	INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
	WHERE “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕосуда' OR “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕрочие'

UPDATE —ырье_1
	SET  од“ипа—ырь€ = 10
	WHERE  од“ипа—ырь€ = 11;

SELECT —ырье_1.Ќаим—ырь€ AS —ырье, “ипы_сырь€.Ќаим“ипа—ырь€ AS “ип FROM —ырье_1
	INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
	WHERE “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕосуда' OR “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕрочие'
GO


DECLARE @AvgProduct int = (SELECT AVG(—ырье_1.÷ена—ырь€) AS —редн€€_цена FROM —ырье_1
	INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
		AND “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕродукты');

DECLARE @AvgDrink int = (SELECT AVG(—ырье_1.÷ена—ырь€) AS —редн€€_цена FROM —ырье_1
	INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
	WHERE “ипы_сырь€.Ќаим“ипа—ырь€ = N'Ќапитки');

SELECT @AvgDrink AS —редн€€_цена_напитков, @AvgProduct AS —редн€€_цена_продуктов;

DECLARE @Iterations int = 0;

IF @AvgProduct > @AvgDrink
		WHILE(@AvgProduct > @AvgDrink)
			BEGIN
				UPDATE —ырье_1
				SET —ырье_1.÷ена—ырь€ -= (—ырье_1.÷ена—ырь€ * 0.2) FROM —ырье_1
					INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
					WHERE “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕродукты';

				SET @AvgProduct = (SELECT AVG(—ырье_1.÷ена—ырь€) AS —редн€€_цена FROM —ырье_1
								INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
									AND “ипы_сырь€.Ќаим“ипа—ырь€ = N'ѕродукты');
				SET @Iterations += 1;
			END
ELSE IF @AvgProduct < @AvgDrink
		WHILE(@AvgProduct < @AvgDrink)
			BEGIN
				UPDATE —ырье_1
					SET —ырье_1.÷ена—ырь€ -= (—ырье_1.÷ена—ырь€ * 0.2) FROM —ырье_1
						INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
						WHERE “ипы_сырь€.Ќаим“ипа—ырь€ = N'Ќапитки';

				SET @AvgDrink = (SELECT AVG(—ырье_1.÷ена—ырь€) AS —редн€€_цена FROM —ырье_1
									INNER JOIN “ипы_сырь€ ON —ырье_1. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
										AND “ипы_сырь€.Ќаим“ипа—ырь€ = N'Ќапитки');
				SET @Iterations += 1;
			END

SELECT @AvgDrink AS —редн€€_цена_напитков,
	@AvgProduct AS —редн€€_цена_продуктов,
	@Iterations AS  оличество_итераций;
