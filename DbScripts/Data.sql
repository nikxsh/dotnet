CREATE TABLE ACCOUNT
(
	Id INT,
	Name VARCHAR(30),
	Age INT,
	Balance MONEY,
	Score INT,
	Country VARCHAR(30)
)

CREATE TABLE LOAN
(
	Id INT,
	AccountNumber INT,
	Amount MONEY,
	Defaulter BIT
)
 
CREATE TABLE DEPARTMENT
(
	Id INT,
	Name VARCHAR(30),
	ManagerId INT
)

CREATE TABLE EMPLOYEE
(
	Id INT,
	Name VARCHAR(30),
	Salary MONEY,
	DeptId INT
)

INSERT INTO ACCOUNT VALUES
(1001, 'Jon Snow', 25, 15000.56, 99, 'UK'),
(1002, 'Arya Stark', 15, 25000.21, 90, 'UK'),
(1003, 'Nedd Stark', 45, 35000.01, 78, 'UK'),
(1004, 'Mukesh Ambani', 25, 562993.6, 88, 'IN'),
(1005, 'Shaktimaan', 21, 21367, 68, 'IN'),
(1006, 'Shaakal', 55, 98635, 12, 'IN'),
(1007, 'Captain Vyom', 35, 154893.32, 99, 'IN'),
(1008, 'Gita Vishwas', 28, 52306.56, 55, 'IN'),
(1009, 'Tyrion Lannister', 32, 156329.36, 45, 'USA'),
(1010, 'Ragnar Lothbrok', 35, 189365.23, 89, 'SE'),
(1011, 'Lagertha', 32, 253000.50, 90, 'SE'),
(1012, 'Floki', 25, 15236.98, 77, 'SE'),
(1013, 'Dr. Zen', 21, 53265.74, 35, 'IN'),
(1014, 'Pablo Escobar', 48, 455236.99, 25, 'CO'),
(1015, 'Gravito', 45, 9863.25, 5, 'CO'),
(1016, 'Jadoo', 21, 199999.99, 1, 'CO'),
(1017, 'Joey Tribbiani', 28, 32963.21,  16, 'IT'),
(1018, 'George Lucas', 88, 236036.36,  99, 'IT'),
(1019, 'Luke Skywalker', 28, 144236.36,  88, 'USA'),
(1020, 'Darth Vader', 28, 152000.23,  16, 'USA')

INSERT INTO LOAN VALUES
(1, 1006, 25000, 0),
(2, 1008, 50000, 1),
(3, 1010, 200000, 0),
(4, 1009, 40000, 0),
(5, 1003, 20000, 0),
(6, 1021, 690000, 1),
(7, 1022, 890000, 0),
(8, 1023, 990000, 1),
(9, 1013, 15000, 0),
(10, 1017, 35200, 1)

INSERT INTO DEPARTMENT VALUES
(101, 'Retail', 1000),
(102, 'Capital', 1005),
(103, 'TaxAccounting', 1010),
(104, 'Wealth', 1015),
(105, 'Insurance', 1022),
(106, 'Investment', 1025)

INSERT INTO EMPLOYEE VALUES
(1000, 'Employee 1', 10000, 101),
(1001, 'Employee 2', 20000, 102),
(1002, 'Employee 3', 30000, 103),
(1003, 'Employee 4', 40000, 104),
(1004, 'Employee 5', 50000, 101),
(1005, 'Employee 6', 60000, 102),
(1006, 'Employee 7', 170000, 103),
(1007, 'Employee 8', 160000, 101),
(1008, 'Employee 9', 20000, 104),
(1009, 'Employee 10', 100000, 102),
(1010, 'Employee 11', 20000, 103),
(1011, 'Employee 12', 60000, 101),
(1012, 'Employee 13', 130000, 102),
(1013, 'Employee 14', 50000, 104),
(1014, 'Employee 15', 150000, 103),
(1015, 'Employee 16', 160000, 101),
(1016, 'Employee 17', 170000, 104),
(1017, 'Employee 18', 180000, 102),
(1018, 'Employee 19', 40000, 104),
(1019, 'Employee 20', 20000, 103)

ALTER FUNCTION GetEmployeeReports
(	
	@DeptId INT
)
RETURNS TABLE 
AS
RETURN 
(
	SELECT E.Name as EmployeeName, Salary
	FROM [dbo].[EMPLOYEE] E
	WHERE E.DeptId = @DeptId
)


SELECT * FROM ACCOUNT
SELECT * FROM LOAN

SELECT * FROM DEPARTMENT
SELECT * FROM EMPLOYEE

/*

DELETE FROM ACCOUNT
DELETE FROM LOAN

DROP TABLE ACCOUNT
DROP TABLE LOAN

DELETE FROM DEPARTMENT
DELETE FROM EMPLOYEE

DROP TABLE DEPARTMENT
DROP TABLE EMPLOYEE
*/