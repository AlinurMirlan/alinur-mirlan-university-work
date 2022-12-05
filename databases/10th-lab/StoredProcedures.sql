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
	@TypeOfGoods nvarchar(max) = N'������������� �������'
	AS
	BEGIN
	IF (@TypeOfGoods IS NULL OR @TypeOfGoods NOT IN (SELECT ����.��������� FROM ����))
		RETURN -1;

	SELECT ������.�����, ������.����, ������.�������� FROM ������
		INNER JOIN ���� ON ������.������� = ����.�������
			AND ����.��������� = @TypeOfGoods
		ORDER BY ������.�����;
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
	IF OBJECT_ID(N'������_1') IS NOT NULL
		DROP TABLE ������_1;

	SET @modified = 0;
	SELECT * INTO ������_1 FROM ������;
	UPDATE ������_1
		SET ������_1.����������������� += (������_1.����������������� * @percentIncreased),
			@modified += 1 FROM ������_1
			INNER JOIN �������� ON ������_1.�������� = ��������.�����������
			AND ��������.�������� = @deliveryName
			AND ������_1.�������������� BETWEEN @dateFrom AND @dateTo;

	SELECT ������_1.���������, ROUND(������_1.�����������������, 2) AS �����������������,
		FORMAT(������_1.��������������, 'd', 'de-DE') AS ��������������  FROM ������_1
		INNER JOIN �������� ON ������_1.�������� = ��������.����������� 
			AND ��������.�������� = @deliveryName
			AND ������_1.�������������� BETWEEN @dateFrom AND @dateTo
		ORDER BY ������_1.��������������;
	END
GO

CREATE PROCEDURE AnalyzeCustomersOrders @customerCode nvarchar(max),
	@dateFrom date, @dateTo smalldatetime, @totalSumOfOrders float OUT
	AS
	BEGIN
	SET @totalSumOfOrders = 0;
	SELECT ������.���������, ������.��������������, ������.�����,
		��������.���� - (��������.���� * ��������.������) AS ��������� FROM ������
		INNER JOIN �������� ON ������.��������� = ��������.���������
		INNER JOIN ������� ON ������.������_ID = �������.������_Id
			AND �������.���������� = @customerCode
		INNER JOIN ������ ON ��������.��������� = ������.���������
		WHERE ������.�������������� BETWEEN @dateFrom AND @dateTo;

	SET @totalSumOfOrders = 
		(SELECT SUM(��������.���� - (��������.���� * ��������.������)) FROM ������
			INNER JOIN �������� ON ������.��������� = ��������.���������
			INNER JOIN ������� ON ������.������_ID = �������.������_Id
				AND �������.���������� = @customerCode
			WHERE ������.�������������� BETWEEN @dateFrom AND @dateTo);
	END
GO
DECLARE @status int;
EXECUTE @status = DisplayGoodsOfType;
IF (@status = -1)
	PRINT N'������� ���� ������� �� ����������.';
GO

DECLARE @status int, @modified int = 0;
EXECUTE @status = IncreaseDeliveryFees 20, N'���������', '1993-10-22', '1993-12-22', @modified OUT;
SELECT @status, @modified;

EXECUTE @status = IncreaseDeliveryFees 20, N'�����', '1993-10-22', '1993-12-22', @modified OUT;
SELECT @status, @modified;
GO

DECLARE @totalSum float = 0;
EXECUTE AnalyzeCustomersOrders 'ANATR', '1994-1-22', '1995-12-22', @totalSum OUT;
SELECT @totalSum AS ���������������;
GO
