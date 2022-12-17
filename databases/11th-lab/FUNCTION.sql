USE University
GO

CREATE OR ALTER FUNCTION MaxSupply(@month smallint, @year smallint)
 RETURNS real
 AS
 BEGIN
 RETURN (SELECT TOP(1) SUM(Склад.Количество * Склад.Цена) FROM Склад
  INNER JOIN Поставщики ON Склад.КодПоставщика = Поставщики.КодПоставщика
  WHERE Поставщики.НаимПоставщика <> N'Склад' AND Склад.ПризнакДвижения = N'Поступление'
  AND MONTH(Склад.Датадвижения) = @month AND YEAR(Склад.Датадвижения) = @year
  GROUP BY Поставщики.КодПоставщика
  ORDER BY SUM(Склад.Количество * Склад.Цена) DESC);
 END
GO

CREATE OR ALTER FUNCTION SumOfSupplies(@month smallint, @year smallint, @supplier nvarchar(max))
 RETURNS real
 AS
 BEGIN
 RETURN (SELECT SUM(Склад.Количество * Склад.Цена) FROM Склад
  INNER JOIN Поставщики ON Склад.КодПоставщика = Поставщики.КодПоставщика AND Поставщики.НаимПоставщика = @supplier
  WHERE Склад.ПризнакДвижения = N'Поступление' AND MONTH(Склад.Датадвижения) = @month AND YEAR(Склад.Датадвижения) = @year);
 END
GO

CREATE OR ALTER FUNCTION EfficientSupplier(@month smallint, @year smallint)
 RETURNS nvarchar(max)
 AS
 BEGIN
 RETURN (SELECT TOP(1) Поставщики.НаимПоставщика FROM Поставщики
  ORDER BY dbo.SumOfSupplies(@month, @year, Поставщики.НаимПоставщика) DESC);
 END
GO

CREATE OR ALTER FUNCTION MaxSupplier(@month smallint, @year smallint)
 RETURNS nvarchar(max)
 AS
 BEGIN
 RETURN (SELECT TOP(1) Поставщики.НаимПоставщика FROM Склад
  INNER JOIN Поставщики ON Склад.КодПоставщика = Поставщики.КодПоставщика
  WHERE Поставщики.НаимПоставщика <> N'Склад' AND Склад.ПризнакДвижения = N'Поступление'
  AND MONTH(Склад.Датадвижения) = @month AND YEAR(Склад.Датадвижения) = @year
  GROUP BY Поставщики.НаимПоставщика
  ORDER BY SUM(Склад.Количество * Склад.Цена) DESC);
 END
GO

CREATE OR ALTER FUNCTION LimitSupplies(@month smallint, @year smallint, @goodsType nvarchar(max), @limit real)
 RETURNS TABLE
 AS
 RETURN SELECT Поставщики.КодПоставщика, Поставщики.НаимПоставщика,
  Объем = ROUND(SUM(Склад.Количество * Склад.Цена), 2) FROM Склад
  INNER JOIN Поставщики ON Склад.КодПоставщика = Поставщики.КодПоставщика
  INNER JOIN Сырье ON Склад.КодСырья = Сырье.КодСырья
  INNER JOIN Типы_сырья ON Сырье.КодТипаСырья = Типы_сырья.КодТипаСырья
  WHERE Типы_сырья.НаимТипаСырья = @goodsType AND Склад.ПризнакДвижения = N'Поступление'
  AND MONTH(Склад.Датадвижения) = @month AND YEAR(Склад.Датадвижения) = @year
  GROUP BY Поставщики.КодПоставщика, Поставщики.НаимПоставщика
  HAVING SUM(Склад.Количество * Склад.Цена) >= @limit;
GO

CREATE OR ALTER FUNCTION LimitSuppliesVerbose(@month smallint, @year smallint, @goodsType nvarchar(max), @limit real)
 RETURNS @table TABLE (КодПоставщика int, НаимПоставщика nvarchar(max), Объем real)
 AS
 BEGIN
 INSERT INTO @table SELECT * FROM (SELECT Поставщики.КодПоставщика, Поставщики.НаимПоставщика,
  Объем = ROUND(SUM(Склад.Количество * Склад.Цена), 2) FROM Склад
  INNER JOIN Поставщики ON Склад.КодПоставщика = Поставщики.КодПоставщика
  INNER JOIN Сырье ON Склад.КодСырья = Сырье.КодСырья
  INNER JOIN Типы_сырья ON Сырье.КодТипаСырья = Типы_сырья.КодТипаСырья
  WHERE Типы_сырья.НаимТипаСырья = @goodsType AND Склад.ПризнакДвижения = N'Поступление'
  AND MONTH(Склад.Датадвижения) = @month AND YEAR(Склад.Датадвижения) = @year
  GROUP BY Поставщики.КодПоставщика, Поставщики.НаимПоставщика
  HAVING SUM(Склад.Количество * Склад.Цена) >= @limit) AS Suppliers;
 RETURN
 END
GO

-- First task
SELECT dbo.MaxSupply(7, 2002);

-- Second task
SELECT dbo.EfficientSupplier(7, 2002) AS НаиэффективныйПоставщик;

-- Third task
SELECT * FROM dbo.LimitSupplies(7, 2002, N'Продукты', 2000);
SELECT * FROM dbo.LimitSuppliesVerbose(7, 2002, N'Прочие', 200);