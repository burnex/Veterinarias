USE [TrabajadoresPrueba]
GO
/****** Object:  StoredProcedure [dbo].[PR_RAZAS_S01]    Script Date: 6/05/2023 23:37:50 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO

/*ALTER CREATE*/
 ALTER PROCEDURE PR_RAZAS_S01
AS
BEGIN
    SELECT
	Razas.Id AS Id,
    Animales.Nombre AS Animal,
    Razas.Nombre AS Raza,
    Razas.Estado
    FROM Razas
    INNER JOIN Animales ON Animales.Id = Razas.IdAnimal
END
