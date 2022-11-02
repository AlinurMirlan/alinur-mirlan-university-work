USE University
GO

SELECT	YEAR(Склад.Датадвижения) AS Год,
		DATENAME(month, Склад.Датадвижения) + N' 1сб' AS Месяц,
		FORMAT(Склад.Датадвижения, 'd', 'de') AS Дата,
		ROUND(SUM(Склад.Количество * Склад.Цена), 2) AS Объем FROM Склад
		INNER JOIN Сырье ON Склад.КодСырья = Сырье.КодСырья
		WHERE Сырье.КодТипаСырья = 10 AND DATEPART(day, Склад.Датадвижения) <= 7
			AND DATENAME(weekday, Склад.Датадвижения) = 'суббота'
		GROUP BY Склад.Датадвижения
		HAVING SUM(Склад.Количество * Склад.Цена) > 100;
GO

WITH СкладПоДекадам
AS
(	
	SELECT YEAR(Склад.Датадвижения) AS Год,
		DATEPART(month, Склад.Датадвижения) AS Месяц_N,
		DATENAME(month, Склад.Датадвижения) + CASE 
			WHEN DATEPART(day, Склад.Датадвижения) <= 10 THEN N'1-st'
			WHEN DATEPART(day, Склад.Датадвижения) <= 20 THEN N'2-nd'
			ELSE N'3-rd'
		END AS Месяц_Декада,
		Склад.Количество,
		Склад.Цена FROM Склад
		WHERE YEAR(Склад.Датадвижения) = 2002 AND Склад.ПризнакДвижения = N'Поступление'
)
SELECT СкладПоДекадам.Год, СкладПоДекадам.Месяц_N, СкладПоДекадам.Месяц_Декада,
	ROUND(SUM(СкладПоДекадам.Количество * СкладПоДекадам.Цена), 2) AS Объем
	FROM СкладПоДекадам
	GROUP BY СкладПоДекадам.Год, СкладПоДекадам.Месяц_N, СкладПоДекадам.Месяц_Декада
	ORDER BY СкладПоДекадам.Год, СкладПоДекадам.Месяц_N, СкладПоДекадам.Месяц_Декада;

-- Version 2
SELECT	YEAR(Склад.Датадвижения) AS Год,
		MONTH(Склад.Датадвижения) AS Месяц_N,
		DATENAME(month, Склад.Датадвижения) + CASE 
			WHEN DATEPART(day, Склад.Датадвижения) <= 10 THEN N'1-st'
			WHEN DATEPART(day, Склад.Датадвижения) <= 20 THEN N'2-nd'
			ELSE N'3-rd'
		END AS Месяц_Декада,
		ROUND(SUM(Склад.Количество * Склад.Цена), 2) AS Объем
		FROM Склад
		GROUP BY YEAR(Склад.Датадвижения), MONTH(Склад.Датадвижения),
			DATENAME(month, Склад.Датадвижения) + CASE 
				WHEN DATEPART(day, Склад.Датадвижения) <= 10 THEN N'1-st'
				WHEN DATEPART(day, Склад.Датадвижения) <= 20 THEN N'2-nd'
				ELSE N'3-rd'
			END
		ORDER BY YEAR(Склад.Датадвижения), MONTH(Склад.Датадвижения), 
			DATENAME(month, Склад.Датадвижения) + CASE 
				WHEN DATEPART(day, Склад.Датадвижения) <= 10 THEN N'1-st'
				WHEN DATEPART(day, Склад.Датадвижения) <= 20 THEN N'2-nd'
				ELSE N'3-rd'
			END
GO

SELECT YEAR(Склад.Датадвижения) AS Год, MONTH(Склад.Датадвижения) AS Месяц_N,
	DATENAME(month, Склад.Датадвижения) + CASE
		WHEN DATEPART(day, Склад.Датадвижения) <= 7 THEN N'   1'
		WHEN DATEPART(day, Склад.Датадвижения) <= 14 THEN N'   2'
		WHEN DATEPART(day, Склад.Датадвижения) <= 21 THEN N'   3'
		WHEN DATEPART(day, Склад.Датадвижения) <= 28 THEN N'   4'
		ELSE N'   5'
	END AS Месяц_Неделя,
	ROUND(SUM(Склад.Количество * Склад.Цена), 2) FROM Склад
	WHERE DATEPART(weekday, Склад.Датадвижения) = 1
		AND MONTH(Склад.Датадвижения) = 7
	GROUP BY Склад.Датадвижения;

SELECT YEAR(СкладА.Датадвижения) AS Год, MONTH(СкладА.Датадвижения) AS Месяц_N,
	DATENAME(month, СкладА.Датадвижения) + N'   ' +
		CAST((DATEPART(week, СкладА.Датадвижения) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1) AS nvarchar(1))
		AS Месяц_Неделя,
	ROUND(SUM(СкладА.Количество * СкладА.Цена), 2) FROM Склад AS СкладА
	WHERE MONTH(СкладА.Датадвижения) = 7 AND YEAR(СкладА.Датадвижения) = 2002 AND
		EXISTS 
		(
			SELECT * FROM Склад AS СкладБ
			WHERE MONTH(СкладБ.Датадвижения) = 7
				AND YEAR(СкладБ.Датадвижения) = 2002
				AND СкладБ.КодПоставщика = 19
				AND (DATEPART(week, СкладА.Датадвижения) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1)
				 = (DATEPART(week, СкладБ.Датадвижения) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1)
		)
	GROUP BY YEAR(СкладА.Датадвижения), MONTH(СкладА.Датадвижения),
		DATENAME(month, СкладА.Датадвижения) + N'   ' +
			CAST((DATEPART(week, СкладА.Датадвижения) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1) AS nvarchar(1))
GO