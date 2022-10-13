USE [University]
GO

-- Selecting widowed entries from the ≈д_изм table using a JOIN and a Subquery.
SELECT —ырье.Ќаим—ырь€ AS Ќаименование,
	—ырье.÷ена—ырь€ AS ÷ена,
	≈д_изм.Ќаим≈д»зм AS ≈диница_»змерени€ FROM ≈д_изм 
		LEFT OUTER JOIN —ырье ON ≈д_изм. од≈д»зм = —ырье. од≈д»зм
		WHERE —ырье. од≈д»зм IS NULL;

SELECT ≈д_изм.Ќаим≈д»зм FROM ≈д_изм
	WHERE NOT EXISTS (SELECT * FROM —ырье WHERE —ырье. од≈д»зм = ≈д_изм. од≈д»зм)

SELECT ≈д_изм.Ќаим≈д»зм FROM ≈д_изм
	WHERE ≈д_изм. од≈д»зм NOT IN 
		(SELECT —ырье. од≈д»зм FROM —ырье WHERE —ырье. од≈д»зм IS NOT NULL)

-- Selecting orphaned entries from the —ырье table.
SELECT —ырье.Ќаим—ырь€ AS Ќаименование,
	—ырье.÷ена—ырь€ AS ÷ена,
	≈д_изм.Ќаим≈д»зм AS ≈диница_»змерени€ FROM ≈д_изм 
		RIGHT OUTER JOIN —ырье ON —ырье. од≈д»зм = ≈д_изм. од≈д»зм
		WHERE —ырье. од≈д»зм IS NULL;

-- Full outer join to include both widowed and orphan entries.
SELECT —ырье.Ќаим—ырь€ AS Ќаименование,
	—ырье.÷ена—ырь€ AS ÷ена,
	≈д_изм.Ќаим≈д»зм AS ≈диница_»змерени€ FROM ≈д_изм 
		FULL OUTER JOIN —ырье ON —ырье. од≈д»зм = ≈д_изм. од≈д»зм
		WHERE —ырье. од≈д»зм IS NULL OR ≈д_изм. од≈д»зм IS NULL;
GO

SELECT “ипы_сырь€.Ќаим“ипа—ырь€ AS “ип,
	—ырье.Ќаим—ырь€, —клад.÷ена AS —ырье,
	—клад. оличество AS  оличество,
	FORMAT(—клад.ƒатадвижени€, 'd', 'en-US') AS ƒата FROM —клад	
	INNER JOIN —ырье ON —клад. од—ырь€ = —ырье. од—ырь€
	INNER JOIN “ипы_сырь€ ON —ырье. од“ипа—ырь€ = “ипы_сырь€. од“ипа—ырь€
	WHERE YEAR(—клад.ƒатадвижени€) = 2003 AND “ипы_сырь€. од“ипа—ырь€ = 12
		AND —клад.ѕризнакƒвижени€ = N'ѕоступление' AND —клад. од—клада = 1
	ORDER BY —ырье.Ќаим—ырь€ ASC;
GO

WITH —клад2003
AS 
(
	SELECT —клад.ѕризнакƒвижени€, —клад. од—кладаƒвиж,
		—клад. од—ырь€,	—клад. оличество,
		DATEPART(DAYOFYEAR, —клад.ƒатадвижени€) / 7 + 1 AS Ќедел€
		FROM —клад
		WHERE YEAR(—клад.ƒатадвижени€) = 2003 AND
			—клад.ѕризнакƒвижени€ = N'ѕоступление'
)
SELECT —писок_складов.Ќаим—клада AS —клад, —клад2003.Ќедел€,
	COUNT(—клад2003. оличество) AS  оличество FROM —клад2003
	INNER JOIN —писок_складов ON —клад2003. од—кладаƒвиж = —писок_складов. од—клада
	INNER JOIN —ырье ON —клад2003. од—ырь€ = —ырье. од—ырь€
	WHERE —ырье. од“ипа—ырь€ = 9
	GROUP BY —писок_складов.Ќаим—клада, —клад2003.Ќедел€
	HAVING —клад2003.Ќедел€ % 2 = 0
	ORDER BY —писок_складов.Ќаим—клада ASC;
GO

/*
The initial version of the query. The only thing bad about this query is - 
- it has code duplicates; namely, computations performed to calculate the 
week of the year. So I went browsing wondering whether SQL offered some kind of
variables for me to save the intermediate result. Turns out you can do this
using subqueries or CTEs (Common Table Expression). I went with the latter

SELECT —писок_складов.Ќаим—клада AS —клад,
	DATEPART(DAYOFYEAR, —клад.ƒатадвижени€) / 7 + 1 AS Ќедел€,
	COUNT(—клад. оличество) AS  оличество FROM —клад
	INNER JOIN —писок_складов ON —клад. од—кладаƒвиж = —писок_складов. од—клада
	INNER JOIN —ырье ON —клад. од—ырь€ = —ырье. од—ырь€
	WHERE —ырье. од“ипа—ырь€ = 9 AND YEAR(—клад.ƒатадвижени€) = 2003
		AND —клад.ѕризнакƒвижени€ = N'ѕоступление'
	GROUP BY —писок_складов.Ќаим—клада, DATEPART(DAYOFYEAR, —клад.ƒатадвижени€) / 7 + 1
	HAVING ((DATEPART(DAYOFYEAR, —клад.ƒатадвижени€) / 7) + 1) % 2 = 0
	ORDER BY —писок_складов.Ќаим—клада ASC;
*/