CREATE DATABASE Company;
go
USE Company

CREATE TABLE EMPLOYEES (
	ID INT PRIMARY KEY IDENTITY (1,1),
	[NAME] NVARCHAR(100) NOT NULL,
	CHARGE NVARCHAR(100) NOT NULL,
	ID_BOSS INT NULL,
	[STATUS] CHAR (1) NOT NULL,
	CONSTRAINT FK_BOSS FOREIGN KEY (ID_BOSS) REFERENCES EMPLOYEES(ID)
);


INSERT INTO EMPLOYEES ([NAME], CHARGE, ID_BOSS, [STATUS]) VALUES
('Pedro', 'Gerente', NULL, 'A'),
('Pablo', 'Sub Gerente', 1, 'A'),
('Juan', 'Supervisor', 2, 'A'),
('José', 'Sub Gerente', 1, 'A'),
('Carlos', 'Supervisor', 4, 'A'),
('Diego', 'Supervisor', 4, 'A');


SELECT * FROM EMPLOYEES

CREATE PROCEDURE _sp_GetHierarchy
AS
BEGIN
    WITH Hierarchy AS (
        SELECT ID, [NAME], CHARGE, ID_BOSS, CAST('' AS VARCHAR(MAX)) AS SANGRIA, 0 AS [LEVEL]
        FROM EMPLOYEES
        WHERE ID_BOSS IS NULL

        UNION ALL

        SELECT e.ID, e.[NAME], e.CHARGE, e.ID_BOSS,
               CAST(j.SANGRIA + '    ' AS VARCHAR(MAX)),
               j.[LEVEL] + 1
        FROM EMPLOYEES e
        INNER JOIN Hierarchy j ON e.ID_BOSS = j.ID
    )
    SELECT SANGRIA + CAST(ID AS VARCHAR) + ' - ' + CHARGE + ' - ' + [NAME] AS Jerarquia, [LEVEL]
    FROM Hierarchy
    ORDER BY [LEVEL]
END;

CREATE PROCEDURE _sp_Employees

@i_operation	NVARCHAR(100),
@i_id			INT = NULL,
@i_name			NVARCHAR(100),
@i_charge		NVARCHAR(100),
@i_id_boss		INT = NULL
AS 
BEGIN
	 BEGIN TRY
		IF(@i_operation = 'INSERT_EMPLOYEES')
			INSERT INTO EMPLOYEES ([NAME], CHARGE, [ID_BOSS]) VALUES (@i_name, @i_charge, @i_id_boss);
	
		ELSE IF(@i_operation = 'UPDATE_EMPLOYEES')
			UPDATE EMPLOYEES SET [NAME] = @i_name, CHARGE = @i_charge, [ID_BOSS] = @i_id_boss WHERE ID = @i_id

		ELSE IF (@i_operation = 'GET_ALL_EMPLOYEES')
			SELECT  ID, [NAME], CHARGE, ID_BOSS FROM EMPLOYEES ORDER BY ID
	
		ELSE IF(@i_operation = 'GET_SPECIFIC_EMPLOYEE')
			SELECT  ID, [NAME], CHARGE, ID_BOSS FROM EMPLOYEES WHERE ID = @i_id

		ELSE IF (@i_operation = 'DELETE_EMPLOYEE')
			UPDATE EMPLOYEES SET [STATUS] = 'I' WHERE ID = @i_id 
	 END TRY

    BEGIN CATCH
        DECLARE @ErrorMessage NVARCHAR(4000);
        SET @ErrorMessage = 'Error en operación [' + @i_operation + ']: ' + ERROR_MESSAGE();

        RAISERROR(@ErrorMessage, 16, 1);
    END CATCH
END;