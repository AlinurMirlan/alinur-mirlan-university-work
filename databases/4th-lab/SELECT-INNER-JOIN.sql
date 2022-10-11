USE [University]
GO

-- ORDER BY { Property }
SELECT DISTINCT TOP(10) —ырье.Ќаим—ырь€ AS Ќаименование, —ырье.÷ена—ырь€ AS ÷ена FROM —клад
	INNER JOIN —ырье ON —клад. од—ырь€ = —ырье. од—ырь€
	WHERE —ырье. од“ипа—ырь€ = 9
	ORDER BY —ырье.÷ена—ырь€ DESC;

-- TOP (expression) or TOP {expression/percent}
SELECT TOP(10) —ырье.Ќаим—ырь€ AS Ќаименование, —ырье.÷ена—ырь€ AS ÷ена FROM —ырье
	WHERE  од“ипа—ырь€ = 9
	ORDER BY ÷ена DESC;
GO

/* Aggregate functions are used in conjunction with GROUP BY clause. Remember, you're not
limited to only use aggregate functions when working with GROUP BY. GROUP BY will only
affect aggregate function values.*/
SELECT MAX(—ырье.÷ена—ырь€) AS ћаксимальна€_÷ена_Ўтучных FROM —ырье
	WHERE —ырье. од≈д»зм = 3
	GROUP BY —ырье. од≈д»зм;

-- Scalar ANY/ALL { Condition } (Subquery)
SELECT
	—ырье.Ќаим—ырь€ AS Ќаименование,
	—ырье.÷ена—ырь€ AS ÷ена,
	—ырье. од≈д»зм AS ≈д»зм FROM —ырье
	WHERE —ырье.÷ена—ырь€ > ALL
	(
		SELECT —ырье.÷ена—ырь€ FROM —ырье
		WHERE —ырье. од≈д»зм = 3
	);

SELECT 
—ырье.Ќаим—ырь€ AS Ќаименование,
—ырье.÷ена—ырь€ AS ÷ена,
—ырье. од≈д»зм AS ≈д»зм FROM —ырье
WHERE —ырье.÷ена—ырь€ > ANY
(
	SELECT —ырье.÷ена—ырь€ FROM —ырье
	WHERE —ырье. од≈д»зм = 3
);
GO

SELECT COUNT(*) AS  оличество_Ѕутылочных_ѕродуктов FROM —ырье
	WHERE —ырье. од≈д»зм = 5
	GROUP BY —ырье. од≈д»зм;
GO

SELECT MAX(—ырье.÷ена—ырь€) AS ћаксимальна€_÷ена_ илограммных FROM —ырье
	WHERE —ырье. од≈д»зм = 1
	GROUP BY —ырье. од≈д»зм;

SELECT AVG(—ырье.÷ена—ырь€) AS —редн€€_÷ена_ илограммных FROM —ырье
	WHERE —ырье. од≈д»зм = 1
	GROUP BY —ырье. од≈д»зм;
GO
