/* ZADANIE 3 */

-- Usuwam procedurê i typ jeœli istniej¹

DROP PROCEDURE IF EXISTS czytelnicyDni;
DROP TYPE IF EXISTS ListaIdCzytelnikow;
GO

-- Muszê zdefiniowaæ nowy typ, inaczej nie bêdê móg³ przekazaæ tabeli do procedury
CREATE TYPE ListaIdCzytelnikow
AS TABLE (ID_Czytelnik INT NOT NULL PRIMARY KEY);
GO

-- Tworzê procedurê, która zliczy dni trzymania ksi¹¿ki dla ka¿dego czytelnika
CREATE PROCEDURE czytelnicyDni(@tab ListaIdCzytelnikow READONLY) AS
BEGIN
	SELECT Czytelnik_ID, SUM(Liczba_Dni) FROM Wypozyczenie w JOIN @tab t ON w.Czytelnik_ID = t.ID_Czytelnik
	GROUP BY Czytelnik_ID;
END
GO

-- Deklarujê zmienn¹ i przekazujê j¹ do procedury
DECLARE @tabela ListaIDCzytelnikow
INSERT INTO @tabela
SELECT DISTINCT Czytelnik_ID FROM Wypozyczenie;
EXEC czytelnicyDni @tabela
GO