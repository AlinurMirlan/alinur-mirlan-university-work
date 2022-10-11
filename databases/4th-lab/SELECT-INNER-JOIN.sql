USE [University]
GO

-- ORDER BY { Property }
SELECT DISTINCT TOP(10) �����.��������� AS ������������, �����.��������� AS ���� FROM �����
	INNER JOIN ����� ON �����.�������� = �����.��������
	WHERE �����.������������ = 9
	ORDER BY �����.��������� DESC;

-- TOP (expression) or TOP {expression/percent}
SELECT TOP(10) �����.��������� AS ������������, �����.��������� AS ���� FROM �����
	WHERE ������������ = 9
	ORDER BY ���� DESC;
GO

/* Aggregate functions are used in conjunction with GROUP BY clause. Remember, you're not
limited to only use aggregate functions when working with GROUP BY. GROUP BY will only
affect aggregate function values.*/
SELECT MAX(�����.���������) AS ������������_����_������� FROM �����
	WHERE �����.�������� = 3
	GROUP BY �����.��������;

-- Scalar ANY/ALL { Condition } (Subquery)
SELECT
	�����.��������� AS ������������,
	�����.��������� AS ����,
	�����.�������� AS ����� FROM �����
	WHERE �����.��������� > ALL
	(
		SELECT �����.��������� FROM �����
		WHERE �����.�������� = 3
	);

SELECT 
�����.��������� AS ������������,
�����.��������� AS ����,
�����.�������� AS ����� FROM �����
WHERE �����.��������� > ANY
(
	SELECT �����.��������� FROM �����
	WHERE �����.�������� = 3
);
GO

SELECT COUNT(*) AS ����������_����������_��������� FROM �����
	WHERE �����.�������� = 5
	GROUP BY �����.��������;
GO

SELECT MAX(�����.���������) AS ������������_����_������������ FROM �����
	WHERE �����.�������� = 1
	GROUP BY �����.��������;

SELECT AVG(�����.���������) AS �������_����_������������ FROM �����
	WHERE �����.�������� = 1
	GROUP BY �����.��������;
GO
