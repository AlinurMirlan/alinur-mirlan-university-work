USE [University]
GO

-- Selecting widowed entries from the ��_��� table using a JOIN and a Subquery.
SELECT �����.��������� AS ������������,
	�����.��������� AS ����,
	��_���.��������� AS �������_��������� FROM ��_��� 
		LEFT OUTER JOIN ����� ON ��_���.�������� = �����.��������
		WHERE �����.�������� IS NULL;

SELECT ��_���.��������� FROM ��_���
	WHERE NOT EXISTS (SELECT * FROM ����� WHERE �����.�������� = ��_���.��������)

SELECT ��_���.��������� FROM ��_���
	WHERE ��_���.�������� NOT IN 
		(SELECT �����.�������� FROM ����� WHERE �����.�������� IS NOT NULL)

-- Selecting orphaned entries from the ����� table.
SELECT �����.��������� AS ������������,
	�����.��������� AS ����,
	��_���.��������� AS �������_��������� FROM ��_��� 
		RIGHT OUTER JOIN ����� ON �����.�������� = ��_���.��������
		WHERE �����.�������� IS NULL;

-- Full outer join to include both widowed and orphan entries.
SELECT �����.��������� AS ������������,
	�����.��������� AS ����,
	��_���.��������� AS �������_��������� FROM ��_��� 
		FULL OUTER JOIN ����� ON �����.�������� = ��_���.��������
		WHERE �����.�������� IS NULL OR ��_���.�������� IS NULL;
GO

SELECT ����_�����.������������� AS ���,
	�����.���������, �����.���� AS �����,
	�����.���������� AS ����������,
	FORMAT(�����.������������, 'd', 'en-US') AS ���� FROM �����	
	INNER JOIN ����� ON �����.�������� = �����.��������
	INNER JOIN ����_����� ON �����.������������ = ����_�����.������������
	WHERE YEAR(�����.������������) = 2003 AND ����_�����.������������ = 12
		AND �����.��������������� = N'�����������' AND �����.��������� = 1
	ORDER BY �����.��������� ASC;
GO

WITH �����2003
AS 
(
	SELECT �����.���������������, �����.�������������,
		�����.��������,	�����.����������,
		DATEPART(DAYOFYEAR, �����.������������) / 7 + 1 AS ������
		FROM �����
		WHERE YEAR(�����.������������) = 2003 AND
			�����.��������������� = N'�����������'
)
SELECT ������_�������.���������� AS �����, �����2003.������,
	COUNT(�����2003.����������) AS ���������� FROM �����2003
	INNER JOIN ������_������� ON �����2003.������������� = ������_�������.���������
	INNER JOIN ����� ON �����2003.�������� = �����.��������
	WHERE �����.������������ = 9
	GROUP BY ������_�������.����������, �����2003.������
	HAVING �����2003.������ % 2 = 0
	ORDER BY ������_�������.���������� ASC;
GO

/*
The initial version of the query. The only thing bad about this query is - 
- it has code duplicates; namely, computations performed to calculate the 
week of the year. So I went browsing wondering whether SQL offered some kind of
variables for me to save the intermediate result. Turns out you can do this
using subqueries or CTEs (Common Table Expression). I went with the latter

SELECT ������_�������.���������� AS �����,
	DATEPART(DAYOFYEAR, �����.������������) / 7 + 1 AS ������,
	COUNT(�����.����������) AS ���������� FROM �����
	INNER JOIN ������_������� ON �����.������������� = ������_�������.���������
	INNER JOIN ����� ON �����.�������� = �����.��������
	WHERE �����.������������ = 9 AND YEAR(�����.������������) = 2003
		AND �����.��������������� = N'�����������'
	GROUP BY ������_�������.����������, DATEPART(DAYOFYEAR, �����.������������) / 7 + 1
	HAVING ((DATEPART(DAYOFYEAR, �����.������������) / 7) + 1) % 2 = 0
	ORDER BY ������_�������.���������� ASC;
*/