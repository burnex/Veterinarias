USE [TrabajadoresPrueba]
GO
/****** Object:  StoredProcedure [dbo].[PR_PERSONAS_S01]    Script Date: 9/05/2023 15:51:52 ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO
ALTER PROCEDURE [dbo].[PR_PERSONAS_S01]

--exec [PR_PERSONAS_S01] 'CXE', 'M'

(
@TipoDocumento	varchar(10) = '',
@Sexo			varchar(1) = ''
)
AS
BEGIN
	SELECT 
	personas.Id,
	personas.TipoDocumento,
	personas.NumeroDocumento,
	personas.Nombres,
	personas.Foto,
	CONVERT(date, personas.FechaNacimiento) AS FechaNacimiento,
	personas.Sexo,
	CASE WHEN Sexo = 'M' THEN 'Masculino' ELSE 'Femenino' END AS 'SexoDescripcion',
	DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS 'Edad',
	personas.Estado
	FROM Personas
	where TipoDocumento = @TipoDocumento
	and sexo = @Sexo
end
