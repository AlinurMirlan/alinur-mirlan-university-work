USE University
GO

SELECT	YEAR(�����.������������) AS ���,
		CASE DATEPART(month, �����.������������)
			WHEN 1 THEN N'������ 1��'
			WHEN 2 THEN N'������� 1��'
			WHEN 3 THEN N'���� 1��'
			WHEN 4 THEN N'������ 1��'
			WHEN 5  THEN N'��� 1��'
			WHEN 6 THEN N'���� 1��'
			WHEN 7 THEN N'���� 1��'
			WHEN 8 THEN N'������ 1��'
			WHEN 9 THEN N'�������� 1��'
			WHEN 10 THEN N'������� 1��'
			WHEN 11 THEN N'������ 1��'
			WHEN 12 THEN N'������� 1��'
		END AS �����,
		FORMAT(�����.������������, 'd', 'de') AS ����,
		ROUND(SUM(�����.���������� * �����.����), 2) AS ����� FROM �����
		INNER JOIN ����� ON �����.�������� = �����.��������
		WHERE �����.������������ = 10 AND �����.������������ IN
			(SELECT MIN(�����.������������) FROM �����
				WHERE DATEPART(weekday, �����.������������) = 6
				GROUP BY DATEPART(month, �����.������������), YEAR(�����.������������))
		GROUP BY �����.������������
		HAVING SUM(�����.���������� * �����.����) > 100;
GO

WITH ��������������
AS
(	
	SELECT YEAR(�����.������������) AS ���,
		DATEPART(month, �����.������������) AS �����_N,
		CASE
			WHEN DATEPART(month, �����.������������) = 1 THEN N'������/'
			WHEN DATEPART(month, �����.������������) = 2 THEN N'�������/'
			WHEN DATEPART(month, �����.������������) = 3 THEN N'����/'
			WHEN DATEPART(month, �����.������������) = 4 THEN N'������/'
			WHEN DATEPART(month, �����.������������) = 5 THEN N'���/'
			WHEN DATEPART(month, �����.������������) = 6 THEN N'����/'
			WHEN DATEPART(month, �����.������������) = 7 THEN N'����/'
			WHEN DATEPART(month, �����.������������) = 8 THEN N'������/'
			WHEN DATEPART(month, �����.������������) = 9 THEN N'��������/'
			WHEN DATEPART(month, �����.������������) = 10 THEN N'�������/'
			WHEN DATEPART(month, �����.������������) = 11 THEN N'������/'
			WHEN DATEPART(month, �����.������������) = 12 THEN N'�������/'
		END + CASE 
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
GO

SELECT YEAR(�����.������������) AS ���, MONTH(�����.������������) AS �����_N,
	CASE DATEPART(month, �����.������������)
		WHEN 1 THEN N'������'
		WHEN 2 THEN N'�������'
		WHEN 3 THEN N'����'
		WHEN 4 THEN N'������'
		WHEN 5  THEN N'���'
		WHEN 6 THEN N'����'
		WHEN 7 THEN N'����'
		WHEN 8 THEN N'������'
		WHEN 9 THEN N'��������'
		WHEN 10 THEN N'�������'
		WHEN 11 THEN N'������'
		WHEN 12 THEN N'�������'
	END + CASE
		WHEN DATEPART(day, �����.������������) <= 7 THEN N'   1'
		WHEN DATEPART(day, �����.������������) <= 14 THEN N'   2'
		WHEN DATEPART(day, �����.������������) <= 21 THEN N'   3'
		WHEN DATEPART(day, �����.������������) <= 28 THEN N'   4'
		ELSE N'   5'
	END AS �����_������,
	ROUND(SUM(�����.���������� * �����.����), 2) FROM �����
	WHERE DATEPART(weekday, �����.������������) = 1
		AND MONTH(�����.������������) = 7
	GROUP BY �����.������������
GO