-- Deklaracje zmiennych. Mo¿na usun¹æ przypisane wartoœci, przyk³ad nadal bêdzie dzia³a³.
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

-- Dodawanie warunków w WHERE, zale¿nie od uzupe³nienia zmiennych
IF(ISNULL(@Tytul, '') != '') SET @SQLQ += ' AND Tytul LIKE @S1'
IF(ISNULL(@Autor, '') != '') SET @SQLQ += ' AND Autor LIKE @S2'
IF(ISNULL(@Rok, '') != '') SET @SQLQ += ' AND Rok_Wydania = @Rok'

-- Wykonanie zapytania DynamicSQL
EXEC sp_executesql @SQLQ, N'@Tytul VARCHAR(300), @Autor VARCHAR(200), @Rok INT, @S1 VARCHAR(200), @S2 VARCHAR(200)', @Tytul, @Autor, @Rok, @S1 = @S1, @S2 = @S2
GO

-- BEZ DYNAMIC SQL (s³aby pomys³ dla du¿ych baz danych)

-- Stworzenie 3 zmiennych:
-- -@SQ1 (dla wyników zapytania z klauzul¹ WHERE Autor LIKE '%@Autor') (jeœli @Autor jest puste, zapytanie bez WHERE)
-- -@SQ2 (dla wyników zapytania z klauzul¹ WHERE Tytul LIKE '%@Tytul') (jeœli @Tytul jest puste, zapytanie bez WHERE)
-- -@SQ3 (dla wyników zapytania z klauzul¹ WHERE Rok = @Rok) (jeœli @Rok jest puste, zapytanie bez WHERE)
-- Utworzenie tabeli bêd¹cej przeciêciem tych trzech tabel.

