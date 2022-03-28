-- Brudne dane - Pomiêdzy zapytaniami w tej samej transakcji odczytujemy ró¿ne dane. 
-- Tutaj mamy ró¿ne wyniki tego samego zapytania
S1:
BEGIN TRAN
UPDATE liczby
   SET liczba = 42

S2: 
SET TRANSACTION ISOLATION LEVEL READ UNCOMMITTED
SELECT * FROM liczby

S1: 
ROLLBACK TRAN

S2:
SELECT * FROM liczby

--		Pojawi³y siê ró¿ne dane

-- Niepowtarzalnoœæ odczytów - Robimy podobnie co wczeœniej, tylko teraz pomiêdzy selectami
-- licz¹cymi œredni¹ mamy zmianê jednego pola, co powoduje zmianê œredniej w drugim select
S1:
SET TRANSACTION ISOLATION LEVEL READ COMMITTED
BEGIN TRAN
SELECT AVG(liczba) from liczby

S2:
UPDATE liczby
   SET liczba = 500 WHERE liczba = 151

S1:
SELECT AVG(liczba) from liczby

		-- Jest ca³kowicie inna œrednia pomiêdzy Selectami

-- Odczyty fantomów - Nowa pozycja pojawia siê w drugim select
S1:
SET TRANSACTION ISOLATION LEVEL REPEATABLE READ
BEGIN TRAN
SELECT * FROM liczby
   WHERE liczba BETWEEN 20 AND 40

S2:
INSERT INTO liczby
   VALUES (999)

S1:
SELECT * FROM liczby
   WHERE liczba BETWEEN 20 AND 40

-- W drugim SELECT pojawi³a siê nowa pozycja, której nie by³o wczeœniej

-- Problemy, ktore napotykamy i ich rozwi¹zania:
-- Brudne dane (rozwi¹zanie: READ COMMITTED)
-- Niepowtarzalnoœæ odczytów (rozwi¹zanie: REPEATABLE READ)
-- Fantomy (rozwi¹zanie: SERIALIZABLE)

-- Im wy¿szy poziom izolacji, tym wiêcej jest blokowane w bazie danych podczas transakcji
