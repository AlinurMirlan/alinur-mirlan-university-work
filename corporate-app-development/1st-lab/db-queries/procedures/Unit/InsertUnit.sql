CREATE OR ALTER PROC InsertUnit
	@unitName nvarchar(20),
	@id int OUT
AS
BEGIN
	BEGIN TRY
		INSERT INTO Unit(Name) VALUES(@unitName);
	END TRY
	BEGIN CATCH
		RAISERROR(N'Unit named %s already exists.', 12, 1, @unitName);
	END CATCH
	SET @id = SCOPE_IDENTITY();
END
