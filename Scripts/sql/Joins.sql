-- INNER JOIN
SELECT *
FROM ACCOUNT A
INNER JOIN LOAN L
ON A.Id = L.AccountNumber

-- INNER MERGE JOIN
SELECT *
FROM ACCOUNT A
INNER MERGE JOIN LOAN L
ON A.Id = L.AccountNumber

-- INNER HASH JOIN
SELECT *
FROM ACCOUNT A
INNER HASH JOIN LOAN L
ON A.Id = L.AccountNumber

-- LEFT JOIN
SELECT *
FROM ACCOUNT A
LEFT JOIN LOAN L
ON A.Id = L.AccountNumber

-- RIGHT JOIN
SELECT *
FROM ACCOUNT A
RIGHT JOIN LOAN L
ON A.Id = L.AccountNumber

-- FULL OUTER JOIN
SELECT *
FROM ACCOUNT A
FULL OUTER JOIN LOAN L
ON A.Id = L.AccountNumber

-- FULL OUTER JOIN
SELECT *
FROM ACCOUNT A
CROSS JOIN LOAN L
ORDER BY A.Name

-- SELF JOIN (Can find duplicate records)
SELECT *
FROM ACCOUNT A, ACCOUNT B
WHERE A.Id = B.Id

-- CROSS APPLY retrieves all the records from the table where there are corresponding matching rows in the output returned by the table valued function.
-- (semantically similar to the INNER JOIN)
SELECT D.Name, EmployeeName, Salary
FROM DEPARTMENT D
CROSS APPLY GetEmployeeReports(D.Id)

-- To retrieve all the rows from both the physical table and the output of the table valued function, OUTER APPLY is used. 
-- (semantically similar to the OUTER JOIN)
SELECT D.Name, EmployeeName, Salary
FROM DEPARTMENT D
OUTER APPLY GetEmployeeReports(D.Id)