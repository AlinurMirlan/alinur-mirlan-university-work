CREATE OR ALTER PROC EditUnitName
	@unitId int,
	@newName nvarchar(20)
AS
BEGIN
	BEGIN TRY
		UPDATE Unit SET Name = @newName WHERE Id = @unitId;
	END TRY
	BEGIN CATCH
		RAISERROR(N'Unit named %s already exists.', 12, 1, @newName);
	END CATCH
END