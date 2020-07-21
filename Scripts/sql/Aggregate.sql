-- Aggregate Examples

-- Get All Accounts having loan
SELECT A.Id, A.Name, L.Amount, CASE WHEN L.Defaulter = 1 THEN 'Defaulter' ELSE 'Normal' END As IsDefaulter
FROM ACCOUNT A
INNER JOIN LOAN L
ON A.Id = L.AccountNumber
ORDER BY L.Amount DESC

-- Get Loan Amount by Country
SELECT A.Country, SUM(L.Amount) TotalLoanAmount
FROM ACCOUNT A
INNER JOIN LOAN L
ON A.Id = L.AccountNumber
GROUP BY A.Country
ORDER BY TotalLoanAmount DESC

-- Get Loan Amount by Age less than 40
SELECT A.Age, SUM(L.Amount) TotalLoanAmount
FROM ACCOUNT A
INNER JOIN LOAN L
ON A.Id = L.AccountNumber
GROUP BY A.Age
HAVING A.Age < 40
ORDER BY TotalLoanAmount DESC

-- You can use the PIVOT and UNPIVOT relational operators to change a table-valued expression into another table. 
-- PIVOT rotates a table-valued expression by turning the unique values from one column in the expression into multiple columns in the output. 
-- And PIVOT runs aggregations where they're required on any remaining column values that are wanted in the final output. 
-- UNPIVOT carries out the opposite operation to PIVOT by rotating columns of a table-valued expression into column values.
SELECT 'Average Balance' AS Average_Balance, [CO], [IN], [IT], [SE], [UK], [USA], [PK]
FROM(
	SELECT Country, Balance
	FROM ACCOUNT
) AS SourceTable
PIVOT
(
	AVG(Balance)
	FOR Country IN ([CO], [IN], [IT], [SE], [UK], [USA], [PK])
) AS PivotTable


