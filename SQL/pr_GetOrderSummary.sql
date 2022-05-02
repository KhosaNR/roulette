/*    ==Scripting Parameters==

    Source Server Version : SQL Server 2017 (14.0.2037)
    Source Database Engine Edition : Microsoft SQL Server Express Edition
    Source Database Engine Type : Standalone SQL Server

    Target Server Version : SQL Server 2017
    Target Database Engine Edition : Microsoft SQL Server Express Edition
    Target Database Engine Type : Standalone SQL Server
*/

USE [Northwind]
GO

/****** Object:  StoredProcedure [dbo].[pr_GetOrderSummary]    Script Date: 2022/05/02 17:24:07 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO


CREATE PROCEDURE [dbo].[pr_GetOrderSummary] @StartDate nvarchar(20), @EndDate nvarchar(20), @EmployeeID nvarchar(5), @CustomerID nvarchar(5)
AS

SELECT
	Header.EmployeeFullName
	,Header.ShipperCompanyName
	,Header.CustomerCompanyName
	,Sum(Header.OrderCount) AS NumberOfOrders
	,Header.[Date]
	,Sum(Header.SumFreightCost) AS TotalFreightCost
	,Sum(Header.DifferentProducts) AS NumberOfDifferentProducts
FROM
(SELECT
			(TitleOfCourtesy + ' ' + FirstName + ' ' + LAStName) AS EmployeeFullName
			,Shippers.CompanyName AS ShipperCompanyName 
			,Customers.CompanyName AS CustomerCompanyName
			,Count(Distinct Orders.OrderID) AS OrderCount
			,Orders.[OrderDate] AS [Date]
			,Sum(Orders.Freight) AS SumFreightCost
			,(SELECT Count (Distinct ProductID)	FROM [Order Details] where OrderID = Orders.OrderID)  AS DifferentProducts 
			--,Sum(Orders.EmployeeID) AS TotalOrderValue -- //What is This?
		FROM 
			Orders
			LEFT JOIN Employees ON ORDERS.EmployeeID = Employees.EmployeeID
			LEFT JOIN Shippers ON Orders.ShipVia = Shippers.ShipperID
			LEFT JOIN Customers ON Orders.CustomerID = Customers.CustomerID
		WHERE
			Orders.OrderDate >= @StartDate
			AND Orders.OrderDate <= @EndDate
			AND Employees.EmployeeID LIKE ISNULL(@EmployeeID,'%')
			AND Customers.CustomerID LIKE ISNULL(@CustomerID, '%')
		GROUP BY
			Orders.[OrderDate]
			,(TitleOfCourtesy + ' ' + FirstName + ' ' + LastName)
			,Customers.CompanyName
			,Shippers.CompanyName
			,OrderID)
		AS Header
GROUP BY
	Header.[Date]
	,Header.EmployeeFullName
	,Header.ShipperCompanyName
	,Header.CustomerCompanyName

GO


