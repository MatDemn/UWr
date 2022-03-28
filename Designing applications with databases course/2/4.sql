-- Deklaracje zmiennych. Mo�na usun�� przypisane warto�ci, przyk�ad nadal b�dzie dzia�a�.
DECLARE @Autor VARCHAR(200) = 'Robert'
DECLARE @Tytul VARCHAR(300) = 'Czysty kod'
DECLARE @Rok INT = 2010

-- Wzorce potrzebne do zapytania DynamicSQL
DECLARE @S1 VARCHAR(200) = '%' + @Tytul + '%'
DECLARE @S2 VARCHAR(200) = '%' + @Autor + '%'

-- Zapytanie dla DynamicSQL
DECLARE @SQLQ NVARCHAR(4000) = '
	SELECT COUNT(*) FROM Egzemplarz e JOIN Ksiazka k ON e.Ksiazka_ID = k.Ksiazka_ID
		WHERE Egzemplarz_ID > 0'

-- Dodawanie warunk�w w WHERE, zale�nie od uzupe�nienia zmiennych
IF(ISNULL(@Tytul, '') != '') SET @SQLQ += ' AND Tytul LIKE @S1'
IF(ISNULL(@Autor, '') != '') SET @SQLQ += ' AND Autor LIKE @S2'
IF(ISNULL(@Rok, '') != '') SET @SQLQ += ' AND Rok_Wydania = @Rok'

-- Wykonanie zapytania DynamicSQL
EXEC sp_executesql @SQLQ, N'@Tytul VARCHAR(300), @Autor VARCHAR(200), @Rok INT, @S1 VARCHAR(200), @S2 VARCHAR(200)', @Tytul, @Autor, @Rok, @S1 = @S1, @S2 = @S2
GO

-- BEZ DYNAMIC SQL (s�aby pomys� dla du�ych baz danych)

-- Stworzenie 3 zmiennych:
-- -@SQ1 (dla wynik�w zapytania z klauzul� WHERE Autor LIKE '%@Autor') (je�li @Autor jest puste, zapytanie bez WHERE)
-- -@SQ2 (dla wynik�w zapytania z klauzul� WHERE Tytul LIKE '%@Tytul') (je�li @Tytul jest puste, zapytanie bez WHERE)
-- -@SQ3 (dla wynik�w zapytania z klauzul� WHERE Rok = @Rok) (je�li @Rok jest puste, zapytanie bez WHERE)
-- Utworzenie tabeli b�d�cej przeci�ciem tych trzech tabel.

