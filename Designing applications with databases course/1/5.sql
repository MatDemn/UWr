/* Zad 5 */

SELECT Customer.LastName, Customer.FirstName, SUM(SalesLT.SalesOrderDetail.UnitPriceDiscount) AS 'Discounts'
FROM SalesLT.SalesOrderDetail 
JOIN SalesLT.SalesOrderHeader ON SalesLT.SalesOrderDetail.SalesOrderID = SalesLT.SalesOrderHeader.SalesOrderID 
JOIN SalesLT.Customer ON SalesLT.SalesOrderHeader.CustomerID = SalesLT.Customer.CustomerID
GROUP BY Customer.FirstName, Customer.LastName;

/* Utworzyć zapytanie, który w wyniku zwróci trzy kolumny: nazwisko i imię klienta (Customer) oraz kwotę, którą
ten klient zaoszczędził dzięki udzielonym rabatom (SalesOrderDetail.UnitPriceDiscount).

Łączę trzy tabele: SalesOrderDetail (zniżki z zamówień), SalesOrderHeader (połączenie pomiędzy Customer a Detail) oraz Customer (imiona i nazwiska).
Na końcu sumuję wszystkie zniżki danego klienta.
 */