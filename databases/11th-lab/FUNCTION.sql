USE University
GO

CREATE OR ALTER FUNCTION MaxSupply(@month smallint, @year smallint)
 RETURNS real
 AS
 BEGIN
 RETURN (SELECT TOP(1) SUM(�����.���������� * �����.����) FROM �����
  INNER JOIN ���������� ON �����.������������� = ����������.�������������
  WHERE ����������.�������������� <> N'�����' AND �����.��������������� = N'�����������'
  AND MONTH(�����.������������) = @month AND YEAR(�����.������������) = @year
  GROUP BY ����������.�������������
  ORDER BY SUM(�����.���������� * �����.����) DESC);
 END
GO

CREATE OR ALTER FUNCTION SumOfSupplies(@month smallint, @year smallint, @supplier nvarchar(max))
 RETURNS real
 AS
 BEGIN
 RETURN (SELECT SUM(�����.���������� * �����.����) FROM �����
  INNER JOIN ���������� ON �����.������������� = ����������.������������� AND ����������.�������������� = @supplier
  WHERE �����.��������������� = N'�����������' AND MONTH(�����.������������) = @month AND YEAR(�����.������������) = @year);
 END
GO

CREATE OR ALTER FUNCTION EfficientSupplier(@month smallint, @year smallint)
 RETURNS nvarchar(max)
 AS
 BEGIN
 RETURN (SELECT TOP(1) ����������.�������������� FROM ����������
  ORDER BY dbo.SumOfSupplies(@month, @year, ����������.��������������) DESC);
 END
GO

CREATE OR ALTER FUNCTION MaxSupplier(@month smallint, @year smallint)
 RETURNS nvarchar(max)
 AS
 BEGIN
 RETURN (SELECT TOP(1) ����������.�������������� FROM �����
  INNER JOIN ���������� ON �����.������������� = ����������.�������������
  WHERE ����������.�������������� <> N'�����' AND �����.��������������� = N'�����������'
  AND MONTH(�����.������������) = @month AND YEAR(�����.������������) = @year
  GROUP BY ����������.��������������
  ORDER BY SUM(�����.���������� * �����.����) DESC);
 END
GO

CREATE OR ALTER FUNCTION LimitSupplies(@month smallint, @year smallint, @goodsType nvarchar(max), @limit real)
 RETURNS TABLE
 AS
 RETURN SELECT ����������.�������������, ����������.��������������,
  ����� = ROUND(SUM(�����.���������� * �����.����), 2) FROM �����
  INNER JOIN ���������� ON �����.������������� = ����������.�������������
  INNER JOIN ����� ON �����.�������� = �����.��������
  INNER JOIN ����_����� ON �����.������������ = ����_�����.������������
  WHERE ����_�����.������������� = @goodsType AND �����.��������������� = N'�����������'
  AND MONTH(�����.������������) = @month AND YEAR(�����.������������) = @year
  GROUP BY ����������.�������������, ����������.��������������
  HAVING SUM(�����.���������� * �����.����) >= @limit;
GO

CREATE OR ALTER FUNCTION LimitSuppliesVerbose(@month smallint, @year smallint, @goodsType nvarchar(max), @limit real)
 RETURNS @table TABLE (������������� int, �������������� nvarchar(max), ����� real)
 AS
 BEGIN
 INSERT INTO @table SELECT * FROM (SELECT ����������.�������������, ����������.��������������,
  ����� = ROUND(SUM(�����.���������� * �����.����), 2) FROM �����
  INNER JOIN ���������� ON �����.������������� = ����������.�������������
  INNER JOIN ����� ON �����.�������� = �����.��������
  INNER JOIN ����_����� ON �����.������������ = ����_�����.������������
  WHERE ����_�����.������������� = @goodsType AND �����.��������������� = N'�����������'
  AND MONTH(�����.������������) = @month AND YEAR(�����.������������) = @year
  GROUP BY ����������.�������������, ����������.��������������
  HAVING SUM(�����.���������� * �����.����) >= @limit) AS Suppliers;
 RETURN
 END
GO

-- First task
SELECT dbo.MaxSupply(7, 2002);

-- Second task
SELECT dbo.EfficientSupplier(7, 2002) AS �����������������������;

-- Third task
SELECT * FROM dbo.LimitSupplies(7, 2002, N'��������', 2000);
SELECT * FROM dbo.LimitSuppliesVerbose(7, 2002, N'������', 200);