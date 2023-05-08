ALTER PROCEDURE PR_PERSONAS_S01
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
DATEDIFF(YEAR, FechaNacimiento, GETDATE()) AS 'Edad',
CASE WHEN Sexo = 'M' THEN 'Masculino' ELSE 'Femenino' END AS 'SexoDescripcion',
personas.Estado
FROM Personas
end
