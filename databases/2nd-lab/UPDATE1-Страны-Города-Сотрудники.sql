ALTER TABLE University.dbo.Сотрудники
	ADD [Семейное положение] nvarchar(3)
		CHECK(LOWER([Семейное положение]) = N'да' OR LOWER([Семейное положение]) = N'нет')
		
