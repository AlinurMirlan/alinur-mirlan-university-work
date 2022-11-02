USE University
GO

SELECT	YEAR(�����.������������) AS ���,
		DATENAME(month, �����.������������) + N' 1��' AS �����,
		FORMAT(�����.������������, 'd', 'de') AS ����,
		ROUND(SUM(�����.���������� * �����.����), 2) AS ����� FROM �����
		INNER JOIN ����� ON �����.�������� = �����.��������
		WHERE �����.������������ = 10 AND DATEPART(day, �����.������������) <= 7
			AND DATENAME(weekday, �����.������������) = '�������'
		GROUP BY �����.������������
		HAVING SUM(�����.���������� * �����.����) > 100;
GO

WITH ��������������
AS
(	
	SELECT YEAR(�����.������������) AS ���,
		DATEPART(month, �����.������������) AS �����_N,
		DATENAME(month, �����.������������) + CASE 
			WHEN DATEPART(day, �����.������������) <= 10 THEN N'1-st'
			WHEN DATEPART(day, �����.������������) <= 20 THEN N'2-nd'
			ELSE N'3-rd'
		END AS �����_������,
		�����.����������,
		�����.���� FROM �����
		WHERE YEAR(�����.������������) = 2002 AND �����.��������������� = N'�����������'
)
SELECT ��������������.���, ��������������.�����_N, ��������������.�����_������,
	ROUND(SUM(��������������.���������� * ��������������.����), 2) AS �����
	FROM ��������������
	GROUP BY ��������������.���, ��������������.�����_N, ��������������.�����_������
	ORDER BY ��������������.���, ��������������.�����_N, ��������������.�����_������;

-- Version 2
SELECT	YEAR(�����.������������) AS ���,
		MONTH(�����.������������) AS �����_N,
		DATENAME(month, �����.������������) + CASE 
			WHEN DATEPART(day, �����.������������) <= 10 THEN N'1-st'
			WHEN DATEPART(day, �����.������������) <= 20 THEN N'2-nd'
			ELSE N'3-rd'
		END AS �����_������,
		ROUND(SUM(�����.���������� * �����.����), 2) AS �����
		FROM �����
		GROUP BY YEAR(�����.������������), MONTH(�����.������������),
			DATENAME(month, �����.������������) + CASE 
				WHEN DATEPART(day, �����.������������) <= 10 THEN N'1-st'
				WHEN DATEPART(day, �����.������������) <= 20 THEN N'2-nd'
				ELSE N'3-rd'
			END
		ORDER BY YEAR(�����.������������), MONTH(�����.������������), 
			DATENAME(month, �����.������������) + CASE 
				WHEN DATEPART(day, �����.������������) <= 10 THEN N'1-st'
				WHEN DATEPART(day, �����.������������) <= 20 THEN N'2-nd'
				ELSE N'3-rd'
			END
GO

SELECT YEAR(�����.������������) AS ���, MONTH(�����.������������) AS �����_N,
	DATENAME(month, �����.������������) + CASE
		WHEN DATEPART(day, �����.������������) <= 7 THEN N'   1'
		WHEN DATEPART(day, �����.������������) <= 14 THEN N'   2'
		WHEN DATEPART(day, �����.������������) <= 21 THEN N'   3'
		WHEN DATEPART(day, �����.������������) <= 28 THEN N'   4'
		ELSE N'   5'
	END AS �����_������,
	ROUND(SUM(�����.���������� * �����.����), 2) FROM �����
	WHERE DATEPART(weekday, �����.������������) = 1
		AND MONTH(�����.������������) = 7
	GROUP BY �����.������������;

SELECT YEAR(������.������������) AS ���, MONTH(������.������������) AS �����_N,
	DATENAME(month, ������.������������) + N'   ' +
		CAST((DATEPART(week, ������.������������) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1) AS nvarchar(1))
		AS �����_������,
	ROUND(SUM(������.���������� * ������.����), 2) FROM ����� AS ������
	WHERE MONTH(������.������������) = 7 AND YEAR(������.������������) = 2002 AND
		EXISTS 
		(
			SELECT * FROM ����� AS ������
			WHERE MONTH(������.������������) = 7
				AND YEAR(������.������������) = 2002
				AND ������.������������� = 19
				AND (DATEPART(week, ������.������������) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1)
				 = (DATEPART(week, ������.������������) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1)
		)
	GROUP BY YEAR(������.������������), MONTH(������.������������),
		DATENAME(month, ������.������������) + N'   ' +
			CAST((DATEPART(week, ������.������������) - DATEPART(week, DATEFROMPARTS(2002, 7, 1)) + 1) AS nvarchar(1))
GO