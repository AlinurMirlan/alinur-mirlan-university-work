USE University
GO

IF OBJECT_ID(N'�����_1') IS NOT NULL
	DROP TABLE �����_1;

SELECT * INTO �����_1 FROM �����;

SELECT �����_1.��������� AS �����, ����_�����.������������� AS ��� FROM �����_1
	INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
	WHERE ����_�����.������������� = N'������' OR ����_�����.������������� = N'������'

UPDATE �����_1
	SET ������������ = 10
	WHERE ������������ = 11;

SELECT �����_1.��������� AS �����, ����_�����.������������� AS ��� FROM �����_1
	INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
	WHERE ����_�����.������������� = N'������' OR ����_�����.������������� = N'������'
GO


DECLARE @AvgProduct int = (SELECT AVG(�����_1.���������) AS �������_���� FROM �����_1
	INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
		AND ����_�����.������������� = N'��������');

DECLARE @AvgDrink int = (SELECT AVG(�����_1.���������) AS �������_���� FROM �����_1
	INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
	WHERE ����_�����.������������� = N'�������');

SELECT @AvgDrink AS �������_����_��������, @AvgProduct AS �������_����_���������;

DECLARE @Iterations int = 0;

IF @AvgProduct > @AvgDrink
		WHILE(@AvgProduct > @AvgDrink)
			BEGIN
				UPDATE �����_1
				SET �����_1.��������� -= (�����_1.��������� * 0.2) FROM �����_1
					INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
					WHERE ����_�����.������������� = N'��������';

				SET @AvgProduct = (SELECT AVG(�����_1.���������) AS �������_���� FROM �����_1
								INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
									AND ����_�����.������������� = N'��������');
				SET @Iterations += 1;
			END
ELSE IF @AvgProduct < @AvgDrink
		WHILE(@AvgProduct < @AvgDrink)
			BEGIN
				UPDATE �����_1
					SET �����_1.��������� -= (�����_1.��������� * 0.2) FROM �����_1
						INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
						WHERE ����_�����.������������� = N'�������';

				SET @AvgDrink = (SELECT AVG(�����_1.���������) AS �������_���� FROM �����_1
									INNER JOIN ����_����� ON �����_1.������������ = ����_�����.������������
										AND ����_�����.������������� = N'�������');
				SET @Iterations += 1;
			END

SELECT @AvgDrink AS �������_����_��������,
	@AvgProduct AS �������_����_���������,
	@Iterations AS ����������_��������;
