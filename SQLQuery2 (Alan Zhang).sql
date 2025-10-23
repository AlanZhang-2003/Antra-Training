USE Northwind

--List all cities that have both Employees and Customers.
SELECT DISTINCT c.City
FROM Customers c LEFT JOIN Employees e ON e.City = c.City	

--a)List all cities that have Customers but no Employee.
SELECT DISTINCT c.city
FROM Customers c
WHERE c.City  NOT IN (
	SELECT e.city
	FROM Employees e
)

--b)List all cities that have Customers but no Employee.
SELECT DISTINCT c.city
FROM Customers c LEFT JOIN Employees e ON e.City = c.City
WHERE e.City IS NULL

--List all products and their total order quantities throughout all orders.
SELECT p.ProductName, SUM(od.Quantity) TotalQuan
FROM Products p
LEFT JOIN [Order Details] od ON od.ProductID = p.ProductID
GROUP BY ProductName

--List all Customer Cities and total products ordered by that city.
SELECT c.City, SUM(od.Quantity) TotalQuan
FROM Customers c
LEFT JOIN Orders o ON o.CustomerID = c.CustomerID
LEFT JOIN [Order Details] od ON od.OrderID = o.OrderID
GROUP BY c.City

--List all Customer Cities that have at least two customers.
SELECT c.City, COUNT(c.CustomerID) Customers
FROM Customers c
GROUP BY c.City
HAVING COUNT(c.CustomerID) >= 2

--List all Customer Cities that have ordered at least two different kinds of products.
SELECT c.City, COUNT(DISTINCT p.ProductID) DiffProduct
FROM Customers c
LEFT JOIN Orders o ON o.CustomerID = c.CustomerID
LEFT JOIN [Order Details] od ON od.OrderID = o.OrderID
LEFT JOIN Products p ON p.ProductID = od.ProductID
GROUP BY c.City
HAVING COUNT( DISTINCT p.ProductID) >=2

--List all Customers who have ordered products, but have the Åeship cityÅf on the order different from their own customer cities.
SELECT DISTINCT c.City
FROM Customers c
LEFT JOIN Orders o ON o.CustomerID = c.CustomerID
WHERE c.City != o.ShipCity

--List 5 most popular products, their average price, and the customer city that ordered most quantity of it.
WITH CityData 
AS(
	SELECT p.ProductName, c.City, SUM(od.Quantity) TotalQuan, AVG(p.UnitPrice) AvgPrice
	FROM Customers c
    LEFT JOIN Orders o ON o.CustomerID = c.CustomerID
	LEFT JOIN [Order Details] od ON od.OrderID = o.OrderID
	LEFT JOIN Products p ON p.ProductID = od.ProductID
	GROUP BY p.ProductName, c.City
)
SELECT TOP 5 cd.ProductName, cd.City, cd.TotalQuan, cd.AvgPrice
FROM CityData cd
ORDER BY cd.TotalQuan DESC

--a)List all cities that have never ordered something but we have employees there.
SELECT DISTINCT e.City
FROM Employees e
WHERE e.City NOT IN (
	SELECT DISTINCT o.ShipCity
	FROM Orders o
)

--b)List all cities that have never ordered something but we have employees there.
SELECT DISTINCT e.City
FROM Employees e
LEFT JOIN Orders o ON o.ShipCity = e.City
WHERE o.OrderID IS NULL

--List one city, if exists, that is the city from where the employee sold most orders (not the product quantity) is, and also the city of most total quantity of products ordered from. (tip: join  sub-query)
WITH EmpOrder
AS
(
	SELECT o.ShipCity, COUNT(o.OrderID) OrderCount
	FROM Orders o 
	LEFT JOIN Employees e ON e.City = o.ShipCity
	GROUP BY o.ShipCity
),
CityOrder
AS
(
	SELECT o.ShipCity, SUM(od.Quantity) TotalQuan
	FROM Orders o
	LEFT JOIN [Order Details] od ON od.OrderID = o.OrderID
	GROUP BY o.ShipCity
)

SELECT eo.ShipCity
FROM EmpOrder eo
LEFT JOIN CityOrder co ON eo.ShipCity = co.ShipCity
WHERE eo.OrderCount = (
	SELECT MAX(eo2.OrderCount) FROM EmpOrder eo2
)
AND
co.TotalQuan = (
	SELECT MAX(co2.TotalQuan) FROM CityOrder co2
)

--How do you remove the duplicates record of a table?
--I would use Partition By to find duplciate data, beacuse
--every data should have a rank of 1 if they are unique.
--Afterwards, I'll delete the data with ranks higher than 1
