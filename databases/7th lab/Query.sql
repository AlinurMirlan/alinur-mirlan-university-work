USE University
GO

SELECT	YEAR(Склад.Датадвижения) AS Год,
		CASE DATEPART(month, Склад.Датадвижения)
			WHEN 1 THEN N'Январь 1сб'
			WHEN 2 THEN N'Февраль 1сб'
			WHEN 3 THEN N'Март 1сб'
			WHEN 4 THEN N'Апрель 1сб'
			WHEN 5  THEN N'Май 1сб'
			WHEN 6 THEN N'Июнь 1сб'
			WHEN 7 THEN N'Июль 1сб'
			WHEN 8 THEN N'Август 1сб'
			WHEN 9 THEN N'Сентябрь 1сб'
			WHEN 10 THEN N'Октябрь 1сб'
			WHEN 11 THEN N'Ноябль 1сб'
			WHEN 12 THEN N'Декабрь 1сб'
		END AS Месяц,
		FORMAT(Склад.Датадвижения, 'd', 'de') AS Дата,
		ROUND(SUM(Склад.Количество * Склад.Цена), 2) AS Объем FROM Склад
		INNER JOIN Сырье ON Склад.КодСырья = Сырье.КодСырья
		WHERE Сырье.КодТипаСырья = 10 AND Склад.Датадвижения IN
			(SELECT MIN(Склад.Датадвижения) FROM Склад
				WHERE DATEPART(weekday, Склад.Датадвижения) = 6
				GROUP BY DATEPART(month, Склад.Датадвижения), YEAR(Склад.Датадвижения))
		GROUP BY Склад.Датадвижения
		HAVING SUM(Склад.Количество * Склад.Цена) > 100;
GO

WITH СкладПоДекадам
AS
(	
	SELECT YEAR(Склад.Датадвижения) AS Год,
		DATEPART(month, Склад.Датадвижения) AS Месяц_N,
		CASE
			WHEN DATEPART(month, Склад.Датадвижения) = 1 THEN N'Январь/'
			WHEN DATEPART(month, Склад.Датадвижения) = 2 THEN N'Февраль/'
			WHEN DATEPART(month, Склад.Датадвижения) = 3 THEN N'Март/'
			WHEN DATEPART(month, Склад.Датадвижения) = 4 THEN N'Апрель/'
			WHEN DATEPART(month, Склад.Датадвижения) = 5 THEN N'Май/'
			WHEN DATEPART(month, Склад.Датадвижения) = 6 THEN N'Июнь/'
			WHEN DATEPART(month, Склад.Датадвижения) = 7 THEN N'Июль/'
			WHEN DATEPART(month, Склад.Датадвижения) = 8 THEN N'Август/'
			WHEN DATEPART(month, Склад.Датадвижения) = 9 THEN N'Сентябрь/'
			WHEN DATEPART(month, Склад.Датадвижения) = 10 THEN N'Октябрь/'
			WHEN DATEPART(month, Склад.Датадвижения) = 11 THEN N'Ноябрь/'
			WHEN DATEPART(month, Склад.Датадвижения) = 12 THEN N'Декабрь/'
		END + CASE 
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
GO

SELECT YEAR(Склад.Датадвижения) AS Год, MONTH(Склад.Датадвижения) AS Месяц_N,
	CASE DATEPART(month, Склад.Датадвижения)
		WHEN 1 THEN N'Январь'
		WHEN 2 THEN N'Февраль'
		WHEN 3 THEN N'Март'
		WHEN 4 THEN N'Апрель'
		WHEN 5  THEN N'Май'
		WHEN 6 THEN N'Июнь'
		WHEN 7 THEN N'Июль'
		WHEN 8 THEN N'Август'
		WHEN 9 THEN N'Сентябрь'
		WHEN 10 THEN N'Октябрь'
		WHEN 11 THEN N'Ноябль'
		WHEN 12 THEN N'Декабрь'
	END + CASE
		WHEN DATEPART(day, Склад.Датадвижения) <= 7 THEN N'   1'
		WHEN DATEPART(day, Склад.Датадвижения) <= 14 THEN N'   2'
		WHEN DATEPART(day, Склад.Датадвижения) <= 21 THEN N'   3'
		WHEN DATEPART(day, Склад.Датадвижения) <= 28 THEN N'   4'
		ELSE N'   5'
	END AS Месяц_Неделя,
	ROUND(SUM(Склад.Количество * Склад.Цена), 2) FROM Склад
	WHERE DATEPART(weekday, Склад.Датадвижения) = 1
		AND MONTH(Склад.Датадвижения) = 7
	GROUP BY Склад.Датадвижения
GO