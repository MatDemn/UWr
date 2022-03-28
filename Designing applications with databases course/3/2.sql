-- Deklaracje struktur
DROP TABLE IF EXISTS Employees;
DROP TABLE IF EXISTS SalaryHistory;
DROP FUNCTION IF EXISTS policzID;
DROP PROCEDURE IF EXISTS ShowSalaries;
GO



CREATE TABLE Employees
(ID INT NOT NULL, SalaryGros DECIMAL(10,2),
PRIMARY KEY (ID));
CREATE TABLE SalaryHistory
(ID INT NOT NULL, EmployeeID INT NOT NULL, Year INT NOT NULL, Month INT NOT NULL, SalaryNet DECIMAL(10,2), SalaryGros DECIMAL(10,2),
PRIMARY KEY (ID));
GO

CREATE FUNCTION policzID() RETURNS INT
BEGIN
	DECLARE @ILE INT = 0;
	SELECT @ILE = COUNT(*) FROM SalaryHistory;
	IF (@ILE = 0)
		RETURN 0;
	RETURN @ILE;
END
GO

INSERT INTO Employees VALUES
(1, 15000), (2, 45000), (3, 5000), (4, 2500), (5, 52000), (6, 2670), (7, 3450), (8, 2350), (9, 12000),
(10, 12000), (11, 2430), (12, 2600), (13, 7200), (14, 4200)



DECLARE CEmplo CURSOR FOR SELECT ID, SalaryGros FROM Employees -- Deklaracja kursora
OPEN CEmplo
DECLARE @id INT, @sg DECIMAL(10,2), @sumsal DECIMAL(10,2) = 0, @taxlimit DECIMAL(10,2) = 85528, @licznik INT = dbo.policzID()+1;
FETCH NEXT FROM CEmplo INTO @id, @sg
WHILE(@@FETCH_STATUS = 0)
BEGIN
	SELECT @sumsal = COALESCE(SUM(SalaryGros), 0) FROM SalaryHistory WHERE EmployeeID = @id AND Year = YEAR(GETDATE());
	SET @sumsal = @sumsal + @sg

	PRINT(CAST(@id AS VARCHAR) + ': ' + CAST(@sumsal AS VARCHAR));

	IF(@sumsal <= @taxlimit)
	BEGIN
		--PRINT(CAST(@id AS VARCHAR) + 'MNIEJ')
		INSERT INTO SalaryHistory VALUES
		(@licznik, @id, 2020, 4, 0.82*@sg, @sg);
	END
	ELSE
	BEGIN
		--PRINT(CAST(@id AS VARCHAR) + 'WIÊCEJ')
		INSERT INTO SalaryHistory VALUES
		(@licznik, @id, 2020, 4, 0.68*@sg, @sg);
	END
	SET @licznik = @licznik + 1
	FETCH NEXT FROM CEmplo INTO @id, @sg
END

CLOSE CEmplo
DEALLOCATE CEmplo
GO

CREATE PROCEDURE ShowSalaries(@month INT) AS
BEGIN
	SET NOCOUNT ON
	DECLARE @TAB TABLE
	(ID INT,
	Pensja DECIMAL(10,2))

	DECLARE @LOG TABLE
	(ID INT,
	Message VARCHAR(200))

	DECLARE CEmplo CURSOR FOR SELECT ID, SalaryGros FROM Employees -- Deklaracja kursora
	OPEN CEmplo
	DECLARE @id INT, @sg DECIMAL(10,2), @sumsal DECIMAL(10,2) = 0, @taxlimit DECIMAL(10,2) = 85528, @monthCount INT = 0;
	FETCH NEXT FROM CEmplo INTO @id, @sg
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		SELECT @sumsal = COALESCE(SUM(SalaryGros), 0), @monthCount = COALESCE(COUNT(SalaryGros), 0) 
		FROM SalaryHistory WHERE EmployeeID = @id AND Month < @month AND Year = YEAR(GETDATE());


		IF(@monthCount+1 >= @month)
		BEGIN
			SET @sumsal = @sumsal + @sg
			IF(@sumsal <= @taxlimit)
			BEGIN
				INSERT INTO @TAB VALUES
				(@id, 0.82*@sg)
			END
			ELSE
			BEGIN
				INSERT INTO @TAB VALUES
				(@id, 0.68*@sg)
			END
		END
		ELSE
		BEGIN
			INSERT INTO @LOG VALUES
				(@id, 'ERROR DURING SALARY CALCULATION')
		END
		FETCH NEXT FROM CEmplo INTO @id, @sg
	END

	SELECT * FROM @TAB;

	DECLARE LogMes CURSOR FOR SELECT * FROM @LOG
	DECLARE @mes VARCHAR(200)
	OPEN LogMes

	FETCH NEXT FROM LogMes INTO @id, @mes
	WHILE(@@FETCH_STATUS = 0)
	BEGIN
		PRINT(CAST(@id AS VARCHAR) + ': ' + @mes)
		FETCH NEXT FROM LogMes INTO @id, @mes
	
	END
	CLOSE LogMes
	DEALLOCATE LogMes
	CLOSE CEmplo
	DEALLOCATE CEmplo

	SET NOCOUNT OFF
END
GO


EXEC ShowSalaries @month = 6
GO