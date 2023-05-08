ALTER PROCEDURE PR_VETERINARIOS_S01
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
end
