/* Zad 3 */

SELECT a.City, COUNT(a.City) FROM
(SELECT DISTINCT SalesLT.Address.City, SalesLT.SalesOrderHeader.ShipToAddressID FROM SalesLT.SalesOrderHeader 
JOIN SalesLT.Address ON  SalesLT.SalesOrderHeader.ShipToAddressID = SalesLT.Address.AddressID) a
GROUP BY a.City;

/* Utworzyć zapytanie, które w wyniku zwróci trzy kolumny: nazwę miasta (z tabeli Address), liczbą klientów z
tego miasta, liczbą SalesPerson obsługujących klientów z tego miasta.

Moja baza nie posiada "SalesPerson", więc jestem w stanie zwrócić tylko Miasto oraz liczbę klientów z miasta.
Łączę tabele SalesOrderHeader oraz Address. Wybieram miasta oraz liczę liczbę wystąpień tych miast. Żeby uniknąć liczenia dwa razy tego samego klienta, używam DISTINCT
w podzapytaniu. Dzięki temu takie same pozycje z miastami i ID nie będą wyświetlone. Potem w zapytaniu nadrzędnym liczę te wystąpienia i wyświetlam wraz z miastami. 
 */