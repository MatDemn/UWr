/* Zad 2 */
SELECT Product.Name, COUNT(Product.Name) AS HowMuch
FROM SalesLT.SalesOrderDetail JOIN SalesLT.Product ON SalesLT.SalesOrderDetail.ProductID = SalesLT.Product.ProductID
GROUP BY Product.Name
HAVING COUNT(Product.Name)>1;

/* Utworzyć zapytanie, które w wyniku zwróci dwie kolumny: nazwę modelu produktu (ProductModel.Name) oraz
liczbę produktów tego modelu, przy czym w wyniku chcemy widzieć tylko te, dla których ta liczba jest większa
niż 1. Zastanowić się, jakie konsekwencje rodzi fakt wyboru nazwy jako wartości grupującej.

Łączę tabele SalesOrderDetail oraz Product. Wybieram nazwy produktów i liczę ich wystąpienia w zamówieniach.

 */