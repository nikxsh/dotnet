-- INNER JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
INNER JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- INNER MERGE JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
INNER MERGE JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- INNER HASH JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
INNER HASH JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- LEFT JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
LEFT JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- RIGHT JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
RIGHT JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- FULL OUTER JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
FULL OUTER JOIN [dbo].[LOAN] L
ON A.[Id] = L.[AccountNumber]

-- FULL OUTER JOIN
SELECT *
FROM [dbo].[ACCOUNT] A
CROSS JOIN [dbo].[LOAN] L
ORDER BY A.Name

-- SELF JOIN (Can find duplicate records)
SELECT *
FROM [dbo].[ACCOUNT] A, [dbo].[ACCOUNT] B
WHERE A.[Id] = B.[Id]

-- CROSS APPLY retrieves all the records from the table where there are corresponding matching rows in the output returned by the table valued function.(semantically similar to the INNER JOIN)
SELECT D.Name, EmployeeName, Salary
FROM [dbo].[DEPARTMENT] D
CROSS APPLY [dbo].[GetEmployeeReports](D.Id)

-- To retrieve all the rows from both the physical table and the output of the table valued function, OUTER APPLY is used. (semantically similar to the OUTER JOIN)
SELECT D.Name, EmployeeName, Salary
FROM [dbo].[DEPARTMENT] D
OUTER APPLY [dbo].[GetEmployeeReports](D.Id)