USE Boreas3
GO

IF OBJECT_ID('DisplayGoodsOfType') IS NOT NULL
	DROP PROCEDURE DisplayGoodsOfType;
IF OBJECT_ID('IncreaseDeliveryFees') IS NOT NULL
	DROP PROCEDURE IncreaseDeliveryFees;
IF OBJECT_ID('AnalyzeCustomersOrders') IS NOT NULL
	DROP PROCEDURE AnalyzeCustomersOrders;
GO

CREATE PROCEDURE DisplayGoodsOfType
	@TypeOfGoods nvarchar(max) = N'Хлебобулочные изделия'
	AS
	BEGIN
	IF (@TypeOfGoods IS NULL OR @TypeOfGoods NOT IN (SELECT Типы.Категория FROM Типы))
		RETURN -1;

	SELECT Товары.Марка, Товары.Цена, Товары.НаСкладе FROM Товары
		INNER JOIN Типы ON Товары.КодТипа = Типы.КодТипа
			AND Типы.Категория = @TypeOfGoods
		ORDER BY Товары.Марка;
	RETURN 1;
	END;
GO

CREATE PROCEDURE IncreaseDeliveryFees
	@percentIncreased int, @deliveryName nvarchar(max),
	@dateFrom date, @dateTo smalldatetime, @modified int OUT
	AS
	BEGIN
	IF (@percentIncreased < 0 OR @percentIncreased > 100)
		RETURN -1;
	IF OBJECT_ID(N'Заказы_1') IS NOT NULL
		DROP TABLE Заказы_1;

	SET @modified = 0;
	SELECT * INTO Заказы_1 FROM Заказы;
	UPDATE Заказы_1
		SET Заказы_1.СтоимостьДоставки += (Заказы_1.СтоимостьДоставки * @percentIncreased),
			@modified += 1 FROM Заказы_1
			INNER JOIN Доставка ON Заказы_1.Доставка = Доставка.КодДоставки
			AND Доставка.Название = @deliveryName
			AND Заказы_1.ДатаИсполнения BETWEEN @dateFrom AND @dateTo;

	SELECT Заказы_1.КодЗаказа, ROUND(Заказы_1.СтоимостьДоставки, 2) AS СтоимостьДоставки,
		FORMAT(Заказы_1.ДатаИсполнения, 'd', 'de-DE') AS ДатаИсполнения  FROM Заказы_1
		INNER JOIN Доставка ON Заказы_1.Доставка = Доставка.КодДоставки 
			AND Доставка.Название = @deliveryName
			AND Заказы_1.ДатаИсполнения BETWEEN @dateFrom AND @dateTo
		ORDER BY Заказы_1.ДатаИсполнения;
	END
GO

CREATE PROCEDURE AnalyzeCustomersOrders @customerCode nvarchar(max),
	@dateFrom date, @dateTo smalldatetime, @totalSumOfOrders float OUT
	AS
	BEGIN
	SET @totalSumOfOrders = 0;
	SELECT Заказы.КодЗаказа, Заказы.ДатаИсполнения, Товары.Марка,
		Заказано.Цена - (Заказано.Цена * Заказано.Скидка) AS Стоимость FROM Заказы
		INNER JOIN Заказано ON Заказы.КодЗаказа = Заказано.КодЗаказа
		INNER JOIN Клиенты ON Заказы.Клиент_ID = Клиенты.Клиент_Id
			AND Клиенты.КодКлиента = @customerCode
		INNER JOIN Товары ON Заказано.КодТовара = Товары.КодТовара
		WHERE Заказы.ДатаИсполнения BETWEEN @dateFrom AND @dateTo;

	SET @totalSumOfOrders = 
		(SELECT SUM(Заказано.Цена - (Заказано.Цена * Заказано.Скидка)) FROM Заказы
			INNER JOIN Заказано ON Заказы.КодЗаказа = Заказано.КодЗаказа
			INNER JOIN Клиенты ON Заказы.Клиент_ID = Клиенты.Клиент_Id
				AND Клиенты.КодКлиента = @customerCode
			WHERE Заказы.ДатаИсполнения BETWEEN @dateFrom AND @dateTo);
	END
GO
DECLARE @status int;
EXECUTE @status = DisplayGoodsOfType;
IF (@status = -1)
	PRINT N'Данного вида товаров не существует.';
GO

DECLARE @status int, @modified int = 0;
EXECUTE @status = IncreaseDeliveryFees 20, N'Самовывоз', '1993-10-22', '1993-12-22', @modified OUT;
SELECT @status, @modified;

EXECUTE @status = IncreaseDeliveryFees 20, N'Почта', '1993-10-22', '1993-12-22', @modified OUT;
SELECT @status, @modified;
GO

DECLARE @totalSum float = 0;
EXECUTE AnalyzeCustomersOrders 'ANATR', '1994-1-22', '1995-12-22', @totalSum OUT;
SELECT @totalSum AS СуммаСтоимостей;
GO
