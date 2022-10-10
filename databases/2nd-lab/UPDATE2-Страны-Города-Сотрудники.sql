ALTER TABLE University.dbo.Сотрудники
	DROP CONSTRAINT CK_Сотрудники_Зарплата

ALTER TABLE University.dbo.Сотрудники
	ADD CONSTRAINT CK_Сотрудники_Зарплата CHECK(Зарплата >= 200 OR Зарплата < 500)

ALTER TABLE University.dbo.Сотрудники
	ADD CONSTRAINT UQ_ФиоСотрудника_ДатаРождения UNIQUE([ФИО сотрудника], [Дата рождения])
	
ALTER TABLE University.dbo.Сотрудники
	ADD CONSTRAINT CK_Сотрудники_ДатаРождения CHECK(YEAR(GETDATE()) - YEAR([Дата рождения]) > 15)
		
