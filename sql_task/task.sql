
--CREATE TABLE dbo.Salesperson
--(
--	Id INT PRIMARY KEY IDENTITY(1, 1),
--	[Name] VARCHAR(50),
--	Age INT,
--	Salary DECIMAL(8, 2)
--)

--CREATE TABLE dbo.Customer
--(
--	Id INT PRIMARY KEY IDENTITY(1, 1),
--	[Name] VARCHAR(50),
--	City VARCHAR(50),
--	IndustryType VARCHAR(1)
--)

--CREATE TABLE dbo.Orders
--(
--	Id INT PRIMARY KEY IDENTITY(1, 1),
--	OrderDate DATE,
--	CustomerId INT,
--	SalespersonId INT,
--	Amount INT,

--	CONSTRAINT [FK_Orders_Salesperson] FOREIGN KEY (SalespersonId) REFERENCES dbo.Salesperson(Id),
--	CONSTRAINT [FK_Orders_Customer] FOREIGN KEY (CustomerId) REFERENCES dbo.Customer(Id)
--)

--SET IDENTITY_INSERT dbo.Salesperson ON

--	INSERT INTO dbo.Salesperson(Id, [Name], Age, Salary)
--		VALUES (1, 'Maxim', 61, 140000)
--			 , (2, 'Michael', 34, 44000)
--			 , (5, 'Chris', 34, 40000)
--			 , (7, 'Dan', 41, 52000)
--			 , (8, 'Ken', 57, 115000)
--			 , (11, 'Joe', 38, 38000)

--SET IDENTITY_INSERT dbo.Salesperson OFF

--SET IDENTITY_INSERT dbo.Customer ON

--	INSERT INTO dbo.Customer(Id, [Name], City, IndustryType)
--		VALUES (4, 'IVM', 'New York', 'J')
--			 , (6, 'Panosong', 'Florida', 'J') 
--			 , (7, 'Seamens', 'Chicago', 'B') 
--			 , (9, 'Nowkia', 'Houston', 'B') 

--SET IDENTITY_INSERT dbo.Customer OFF

--SET IDENTITY_INSERT dbo.Orders ON

--	INSERT INTO dbo.Orders(Id, OrderDate, CustomerId, SalespersonId, Amount)
--		VALUES (10, '8/2/96', 4, 2, 540)
--			 , (20, '1/30/99', 4, 8, 460)
--			 , (30, '7/14/95', 9, 1, 460)
--			 , (40, '1/29/98', 7, 2, 2400)
--			 , (50, '2/3/98', 6, 7, 600)
--			 , (60, '3/2/98', 6, 7, 2400)
--			 , (70, '5/6/98', 9, 7, 150)

--SET IDENTITY_INSERT dbo.Orders OFF



--a)	Имена всех продавцов, у которых были заказы от Seamens

SELECT sp.[Name]
FROM dbo.Salesperson sp
JOIN dbo.Orders o ON o.SalespersonId = sp.Id
JOIN dbo.Customer c ON o.CustomerId = c.Id
WHERE c.[Name] = 'Seamens'


--b)	Имена всех продавцов, у которых было 2 и более заказов

SELECT sp.[Name]
FROM dbo.Salesperson sp
JOIN (
	SELECT o.SalespersonId
	FROM dbo.Orders o
	GROUP BY o.SalespersonId
	HAVING COUNT(o.SalespersonId) >= 2
) q ON sp.Id = q.SalespersonId


-- c)	Имена всех продавцов, у которых был заказ с максимальной суммой.

SELECT sp.[Name]
FROM dbo.Salesperson sp
WHERE sp.Id IN (
	SELECT o.SalespersonId
	FROM dbo.Orders o
	WHERE o.Amount IN ( 
		SELECT Max(Amount) FROM dbo.Orders 
	)
)

/*
	d)	Имена всех продавцов, отсортированные в порядке возрастания общей суммы, заключенных ими заказов (сумму выводить не нужно). 
	Если у двух продавцов сумма совпадает, то их нужно сортировать в алфавитном порядке.
*/

SELECT sp.[Name]
	--, ISNULL(orders.Amount, 0)	
	, ROW_NUMBER() OVER(PARTITION BY orders.Amount ORDER BY sp.[Name]) AS [Row]
FROM dbo.Salesperson sp
LEFT JOIN (
	SELECT o.SalespersonId
		, SUM(o.Amount) AS [Amount]
	FROM dbo.Orders o
	GROUP BY o.SalespersonId
) orders ON sp.Id = orders.SalespersonId
ORDER BY orders.Amount OFFSET 0 ROWS

