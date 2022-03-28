/* ZADANIE 3 */

-- Usuwam procedur� i typ je�li istniej�

DROP PROCEDURE IF EXISTS czytelnicyDni;
DROP TYPE IF EXISTS ListaIdCzytelnikow;
GO

-- Musz� zdefiniowa� nowy typ, inaczej nie b�d� m�g� przekaza� tabeli do procedury
CREATE TYPE ListaIdCzytelnikow
AS TABLE (ID_Czytelnik INT NOT NULL PRIMARY KEY);
GO

-- Tworz� procedur�, kt�ra zliczy dni trzymania ksi��ki dla ka�dego czytelnika
CREATE PROCEDURE czytelnicyDni(@tab ListaIdCzytelnikow READONLY) AS
BEGIN
	SELECT Czytelnik_ID, SUM(Liczba_Dni) FROM Wypozyczenie w JOIN @tab t ON w.Czytelnik_ID = t.ID_Czytelnik
	GROUP BY Czytelnik_ID;
END
GO

-- Deklaruj� zmienn� i przekazuj� j� do procedury
DECLARE @tabela ListaIDCzytelnikow
INSERT INTO @tabela
SELECT DISTINCT Czytelnik_ID FROM Wypozyczenie;
EXEC czytelnicyDni @tabela
GO