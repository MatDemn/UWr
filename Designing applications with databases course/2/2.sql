-- ZAD 2 --

/* Najpierw usuwam funkcjê i procedurê, w razie gdyby ju¿ istnia³y. */
DROP FUNCTION IF EXISTS policzKombinacje;
DROP PROCEDURE IF EXISTS losujKombinacje;
GO

/* Funkcja zliczaj¹ca mo¿liwe kombinacje tabeli imiona z tabel¹ nazwiska. */
CREATE FUNCTION policzKombinacje() RETURNS INT
BEGIN
	DECLARE @C_imiona INT, @C_nazwiska INT
	SELECT @C_imiona = COUNT(imie) FROM dbo.imiona
	SELECT @C_nazwiska = COUNT(nazwisko) FROM dbo.nazwiska
	RETURN @C_imiona * @C_nazwiska
END
GO

/* Procedura losuj¹ca kombinacje imion i nazwisk oraz wstawiaj¹ca je do tabeli dane. */
CREATE PROCEDURE losujKombinacje (@ile INT) AS
BEGIN
	IF(@ile > dbo.policzKombinacje()/2) THROW 51000, 'ZA DUZY ARGUMENT.', 1;  
	DROP TABLE IF EXISTS dane;

	CREATE TABLE dane (
	imie VARCHAR(50),
	nazwisko VARCHAR(50)
	);

	WITH TAB1 AS (
		SELECT imiona.ID_imie, nazwiska.ID_nazwisko FROM imiona CROSS JOIN nazwiska)
	INSERT INTO dane
	SELECT TOP(@ile) imie, nazwisko FROM TAB1 t JOIN imiona i ON t.ID_imie = i.ID_imie
	JOIN nazwiska n ON t.ID_nazwisko = n.ID_nazwisko ORDER BY NEWID();
END
GO

/* Wykonanie procedury. */

EXEC losujKombinacje @ile=42
GO

/* Wyœwietlenie zawartoœci tabeli dane po wykonaniu procedury. */
SELECT * FROM dane;
GO