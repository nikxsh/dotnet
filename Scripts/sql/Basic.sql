-- Single Table Selection

-- Get Max Balance
SELECT MAX(Balance) FROM [dbo].[ACCOUNT]

-- Get Second Max Balance
SELECT MAX(Balance) FROM [dbo].[ACCOUNT] WHERE Balance < ( SELECT MAX(Balance) FROM [dbo].[ACCOUNT])

-- Get Third Max Balance
SELECT MAX(Balance) FROM [dbo].[ACCOUNT] WHERE Balance < (SELECT MAX(Balance) FROM [dbo].[ACCOUNT] WHERE Balance < ( SELECT MAX(Balance) FROM [dbo].[ACCOUNT]))

-- Get Max Balance With Name
SELECT A.Name, A.Balance
FROM [dbo].[ACCOUNT] A
WHERE A.Balance = (SELECT MAX(Balance) FROM [dbo].[ACCOUNT])

-- Get top 5 balace using ROW_NUMBER()
SELECT Row#, A.Name, A.Balance
FROM (
	SELECT ROW_NUMBER() OVER (ORDER BY Balance DESC) AS Row#, Id, Name, Balance	
	FROM [dbo].[ACCOUNT]
) A
WHERE Row# IN (1,2,3,4,5) 
ORDER BY A.Balance DESC

-- Adding a PARTITION BY clause on the Id column, will restart the numbering when the Id value changes.
SELECT ROW_NUMBER() OVER (PARTITION BY Country ORDER BY Balance DESC) AS Row#, Name, Balance, Country
FROM [dbo].[ACCOUNT]

-- Get max balance by Country
SELECT A.Country, MAX(A.Balance) As TotalBalance
FROM [dbo].[ACCOUNT] A
GROUP BY A.Country
ORDER BY TotalBalance DESC

-- Get top 5 Age using RANK()
SELECT Rank#, A.Name, A.Age, A.Balance
FROM (
    -- ROW_NUMBER and RANK are similar. ROW_NUMBER numbers all rows sequentially (for example 1, 2, 3, 4, 5). 
	-- RANK provides the same numeric value for ties (for example 1, 2, 2, 4, 5).
	SELECT RANK() OVER (ORDER BY Age DESC, Balance DESC) AS Rank#, Id, Name, Age,  Balance	
	FROM [dbo].[ACCOUNT]
) A
WHERE Rank# IN (1,2,3,4,5) 

-- Get top 5 Age using RANK()
SELECT Rank#, A.Name, A.Age, A.Balance
FROM (
	-- If two or more rows have the same rank value in the same partition, each of those rows will receive the same rank.
	SELECT DENSE_RANK() OVER (ORDER BY Age DESC) AS Rank#, Id, Name, Age,  Balance	
	FROM [dbo].[ACCOUNT]
) A
WHERE Rank# IN (1,2,3,4,5) 

-- Distributes the rows in an ordered partition into a specified number of groups. The groups are numbered, starting at one. 
-- For each row, NTILE returns the number of the group to which the row belongs.
SELECT NTILE(4) OVER (ORDER BY Age DESC) AS Quartile, Name, Age, Balance, Country
FROM [dbo].[ACCOUNT]

-- Employee salary between 10000 to 40000
SELECT *
FROM [dbo].[EMPLOYEE]
WHERE Salary BETWEEN 10000 AND 40000

-- The ANY operator returns TRUE if any of the subquery values meet the condition
SELECT *
FROM [dbo].[DEPARTMENT]
WHERE ManagerId = ANY (SELECT Id FROM [dbo].[EMPLOYEE])

-- The ALL operator returns TRUE if all of the subquery values meet the condition.
SELECT *
FROM [dbo].[DEPARTMENT]
WHERE ManagerId = ALL (SELECT Id FROM [dbo].[EMPLOYEE])

-- Account Number without loan (EXISTS used to test for the existence of any record in a subquery.)
SELECT A.Id, A.Name, A.Balance
FROM [dbo].[ACCOUNT] A
WHERE NOT EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- Account Number with loan
SELECT A.Id, A.Name, A.Balance
FROM [dbo].[ACCOUNT] A
WHERE EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- Account Number with loan
SELECT A.Id, A.Name, A.Balance
FROM [dbo].[ACCOUNT] A
WHERE EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- The ALL operator returns TRUE if all of the subquery values meet the condition.
SELECT *
FROM [dbo].[EMPLOYEE] E1
WHERE E1.Salary > SOME (SELECT Salary FROM [dbo].[EMPLOYEE] WHERE E1.DeptId = 104)

