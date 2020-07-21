-- Get Max Amount
SELECT MAX(Amount) FROM ACCOUNT

-- Get Second Max Amount
SELECT MAX(Amount) FROM ACCOUNT WHERE Amount < ( SELECT MAX(Amount) FROM ACCOUNT)

-- Get Third Max Amount
SELECT MAX(Amount) FROM ACCOUNT WHERE Amount < (SELECT MAX(Amount) FROM ACCOUNT WHERE Amount < ( SELECT MAX(Amount) FROM ACCOUNT))

-- Get Max Amount With Name
SELECT A.Name, A.Amount
FROM ACCOUNT A
WHERE A.Amount = (SELECT MAX(Amount) FROM ACCOUNT)

-- Get top 5 balace using ROW_NUMBER()
-- The ROW_NUMBER() is a MSSQL function that assigns a sequential integer to each row within the partition of a result set. 
SELECT Row#, A.Name, A.Amount
FROM (
	SELECT ROW_NUMBER() OVER (ORDER BY Amount DESC) AS Row#, Id, Name, Amount	
	FROM ACCOUNT
) A
WHERE Row# IN (1,2,3,4,5) 
ORDER BY A.Amount DESC

-- Adding a PARTITION BY clause on the Id column, will restart the numbering when the Id value changes.
-- PARTITION BY to divide the result set into partitions and perform computation on each subset of partitioned data.
SELECT ROW_NUMBER() OVER (PARTITION BY Bank ORDER BY Amount DESC) AS Row#, Name, Amount, Bank
FROM ACCOUNT

-- Get max Amount by Bank
SELECT A.Bank, MAX(A.Amount) As TotalAmount
FROM ACCOUNT A
GROUP BY A.Bank
ORDER BY TotalAmount DESC

-- Get top 5 Age using RANK()
SELECT Rank#, A.Name, A.Age, A.Amount
FROM (
    -- ROW_NUMBER and RANK are similar. ROW_NUMBER numbers all rows sequentially (for example 1, 2, 3, 4, 5). 
	-- RANK provides the same numeric value for ties (for example 1, 2, 2, 4, 5).
	SELECT RANK() OVER (ORDER BY Age DESC, Amount DESC) AS Rank#, Id, Name, Age,  Amount	
	FROM ACCOUNT
) A
WHERE Rank# IN (1,2,3,4,5) 

-- Get top 5 Age using RANK()
SELECT Rank#, A.Name, A.Age, A.Amount
FROM (
	-- If two or more rows have the same rank value in the same partition, each of those rows will receive the same rank.
	SELECT DENSE_RANK() OVER (ORDER BY Age DESC) AS Rank#, Id, Name, Age,  Amount	
	FROM ACCOUNT
) A
WHERE Rank# IN (1,2,3,4,5) 

-- NTILE() is a window function that distributes rows of an ordered partition into a specified number of approximately equal groups, or buckets. 
-- It assigns each group a bucket number starting from one. For each row in a group, the NTILE() function assigns a bucket number representing the 
-- group to which the row belongs.
SELECT NTILE(4) OVER (ORDER BY Age DESC) AS Quartile, Name, Age, Amount, Bank
FROM ACCOUNT

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
SELECT A.Id, A.Name, A.Amount
FROM ACCOUNT A
WHERE NOT EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- Account Number with loan
SELECT A.Id, A.Name, A.Amount
FROM ACCOUNT A
WHERE EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- Account Number with loan
SELECT A.Id, A.Name, A.Amount
FROM ACCOUNT A
WHERE EXISTS (SELECT E.AccountNumber FROM [dbo].[LOAN] E WHERE E.AccountNumber = A.Id)

-- The ALL operator returns TRUE if all of the subquery values meet the condition.
SELECT *
FROM [dbo].[EMPLOYEE] E1
WHERE E1.Salary > SOME (SELECT Salary FROM [dbo].[EMPLOYEE] WHERE E1.DeptId = 104)

