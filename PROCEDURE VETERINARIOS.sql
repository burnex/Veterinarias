USE [TrabajadoresPrueba]
GO
/****** Object:  StoredProcedure [dbo].[PR_VETERINARIOS_S01]    Script Date: 9/05/2023 15:52:02 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PR_VETERINARIOS_S01]
/*
exec [PR_VETERINARIOS_S01] 'DNI', 'F'
*/
(
@TipoDocumento	varchar(10) = '',
@Sexo			varchar(1) = ''
)
AS
BEGIN
	SELECT 
	Veterinarios.Id,
	Veterinarios.TipoDocumento,
	Veterinarios.NumeroDocumento,
	Veterinarios.Nombres,
	Veterinarios.Foto,
	CONVERT(date, Veterinarios.FechaNacimiento) AS FechaNacimiento,
	Veterinarios.Sexo,
	CASE WHEN Sexo = 'M' THEN 'Masculino' ELSE 'Femenino' END AS 'SexoDescripcion',
	DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS 'Edad',
	Veterinarios.colegio,
	Veterinarios.Estado
	FROM Veterinarios
	where TipoDocumento = @TipoDocumento
	and sexo = @Sexo
end
