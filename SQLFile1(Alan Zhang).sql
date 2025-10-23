USE AdventureWorks2022

--How many products can you find in the Production.Product table?
SELECT COUNT(p.ProductID) "Total Products"
FROM Production.Product p

--Write a query that retrieves the number of products in the Production.Product table that are included in a subcategory. The rows that have NULL in column ProductSubcategoryID are considered to not be a part of any subcategory.
SELECT COUNT(p.ProductID) "Total Product w/ Subcategory"
FROM Production.Product p
WHERE p.ProductSubcategoryID IS NOT NULL

--How many Products reside in each SubCategory? Write a query to display the results with the following titles.
SELECT p.ProductSubcategoryID ,COUNT(p.ProductID) "CountedProducts"
FROM Production.Product p
WHERE p.ProductSubcategoryID IS NOT NULL
GROUP BY p.ProductSubcategoryID

--How many products that do not have a product subcategory.
SELECT COUNT(p.ProductID) "Total Product w/o Subcategory"
FROM Production.Product p
WHERE p.ProductSubcategoryID IS NULL

--Write a query to list the sum of products quantity in the Production.ProductInventory table.
SELECT SUM(p.Quantity) "Total Quantity"
FROM Production.ProductInventory p

-- Write a query to list the sum of products in the Production.ProductInventory table and LocationID set to 40 and limit the result to include just summarized quantities less than 100.
SELECT TOP 100 p.ProductID ,SUM(p.Quantity) "TheSum"
FROM Production.ProductInventory p 
WHERE p.LocationID = 40
GROUP BY p.ProductID

--Write a query to list the sum of products with the shelf information in the Production.ProductInventory table and LocationID set to 40 and limit the result to include just summarized quantities less than 100
SELECT TOP 100 p.Shelf, p.ProductID ,SUM(p.Quantity) "TheSum"
FROM Production.ProductInventory p 
WHERE p.LocationID = 40
GROUP BY p.ProductID, p.Shelf

--Write the query to list the average quantity for products where column LocationID has the value of 10 from the table Production.ProductInventory table.
SELECT AVG(p.Quantity) "AVG Quantity"
FROM Production.ProductInventory p
WHERE p.LocationID = 10

--Write query  to see the average quantity  of  products by shelf  from the table Production.ProductInventory
SELECT p.ProductID, p.Shelf, AVG(p.Quantity) "TheAVG"
FROM Production.ProductInventory p
WHERE p.LocationID = 10
GROUP BY p.ProductID, p.Shelf

--Write query  to see the average quantity  of  products by shelf excluding rows that has the value of N/A in the column Shelf from the table Production.ProductInventory
SELECT p.ProductID, p.Shelf, AVG(p.Quantity) "TheAVG"
FROM Production.ProductInventory p
WHERE p.LocationID = 10 AND p.Shelf != 'N/A'
GROUP BY p.ProductID, p.Shelf

-- List the members (rows) and average list price in the Production.Product table. This should be grouped independently over the Color and the Class column. Exclude the rows where Color or Class are null.
SELECT p.Color, p.Class, COUNT(p.ProductID) "TheCount", SUM(p.ListPrice) "AvgPrice"
FROM Production.Product p
WHERE p.Color IS NOT NULL AND p.Class IS NOT NULL
GROUP BY p.Color, p.Class

--Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables. Join them and produce a result set similar to the following.
SELECT cr.Name Country, sp.StateProvinceCode Province
FROM Person.CountryRegion cr JOIN Person.StateProvince sp ON cr.CountryRegionCode = sp.CountryRegionCode
ORDER BY cr.Name

--Write a query that lists the country and province names from person. CountryRegion and person. StateProvince tables and list the countries filter them by Germany and Canada. Join them and produce a result set similar to the following.
SELECT cr.Name Country, sp.StateProvinceCode Province
FROM Person.CountryRegion cr JOIN Person.StateProvince sp ON cr.CountryRegionCode = sp.CountryRegionCode
WHERE cr.Name IN ('Germany', 'Canada')
ORDER BY cr.Name

USE Northwind

--List all Products that has been sold at least once in last 27 years.
SELECT p.ProductName, o.OrderDate
FROM Products p JOIN [Order Details] od ON od.ProductID = p.ProductID
JOIN Orders o ON o.OrderID = od.OrderID
WHERE o.OrderDate  >= DATEADD(YEAR, -27, GETDATE())

--List top 5 locations (Zip Code) where the products sold most.
SELECT TOP 5 o.ShipPostalCode, COUNT(od.ProductID) Sold
FROM Orders o
JOIN [Order Details] od ON o.OrderID = od.OrderID
WHERE o.ShipPostalCode IS NOT NULL
GROUP BY o.ShipPostalCode
ORDER BY Sold DESC

--List top 5 locations (Zip Code) where the products sold most in last 27 years.
WITH Sold 
AS(
	SELECT od.ProductID, o.OrderID, o.ShipPostalCode
	FROM [Order Details] od JOIN Orders o ON o.OrderID = od.OrderID
	WHERE o.OrderDate  >= DATEADD(YEAR, -30, GETDATE()) AND o.ShipPostalCode IS NOT NULL
)
SELECT TOP 5 s.ShipPostalCode, COUNT(s.ProductID) TotalSold
FROM Sold s
GROUP BY s.ShipPostalCode
ORDER BY TotalSold DESC

--List all city names and number of customers in that city.     
SELECT c.city, COUNT(c.CustomerID) "Number of Customers"
FROM Customers c
GROUP BY c.City

--List city names which have more than 2 customers, and number of customers in that city
SELECT c.city, COUNT(c.CustomerID) "Number of Customers"
FROM Customers c
GROUP BY c.City
HAVING  COUNT(c.CustomerID) > 2

--List the names of customers who placed orders after 1/1/98 with order date.
SELECT c.ContactName, o.OrderDate
FROM Customers c JOIN Orders o ON o.CustomerID = c.CustomerID
WHERE o.OrderDate >= '1998-01-01'

-- List the names of all customers with most recent order dates
SELECT c.ContactName, MAX(o.OrderDate)
FROM Customers c JOIN Orders o ON o.CustomerID = c.CustomerID
GROUP BY c.ContactName

--Display the names of all customers  along with the  count of products they bought
SELECT c.ContactName, COUNT(od.ProductID) TotalProduct
FROM Customers c JOIN Orders o ON o.CustomerID = c.CustomerID
JOIN [Order Details] od ON od.OrderID = o.OrderID
GROUP BY c.ContactName

--Display the customer ids who bought more than 100 Products with count of products.
SELECT c.CustomerID, COUNT(od.ProductID) TotalProduct
FROM Customers c JOIN Orders o ON o.CustomerID = c.CustomerID
JOIN [Order Details] od ON od.OrderID = o.OrderID
GROUP BY c.CustomerID
HAVING COUNT(od.ProductID) > 100

--List all of the possible ways that suppliers can ship their products. Display the results as below
SELECT DISTINCT s.CompanyName "Supplier Company Name", sh.CompanyName "Shipping Company Name"
FROM Suppliers s 
JOIN Products p ON s.SupplierID = p.SupplierID
JOIN [Order Details] od ON od.ProductID = p.ProductID
JOIN Orders o ON od.OrderID = o.OrderID
JOIN Shippers sh ON o.ShipVia = sh.ShipperID
ORDER BY s.CompanyName, sh.CompanyName

--Display the products order each day. Show Order date and Product Name.
SELECT o.OrderDate OrderDate, p.ProductName ProductName 
FROM Orders o 
JOIN [Order Details] od ON od.OrderID = o.OrderID
JOIN Products p ON od.ProductID = p.ProductID

--Displays pairs of employees who have the same job title.
SELECT e1.FirstName + ' ' + e1.LastName Employee1, e2.FirstName + ' ' + e2.LastName Employee2, e1.Title
FROM Employees e1 JOIN Employees e2 ON e1.Title = e2.Title AND e1.EmployeeID < e2.EmployeeID

-- Display all the Managers who have more than 2 employees reporting to them.
SELECT e.ReportsTo Manager, COUNT(e.ReportsTo) ManageNumber
FROM Employees e
GROUP By e.ReportsTo
HAVING COUNT(e.ReportsTo) > 2

--Display the customers and suppliers by city. The results should have the following columns
SELECT c.City, c.CompanyName Name, c.ContactName, 'Customer' Type
FROM Customers c
UNION ALL
SELECT s.City, s.CompanyName, s.ContactName, 'Supplier' Type
FROM Suppliers s