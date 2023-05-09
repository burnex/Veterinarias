CREATE PROCEDURE PR_BANDEJA_S01
AS
BEGIN
SELECT 
Mascotas.Id,
Animales.Nombre 'Animal',
Razas.Nombre 'Raza',
Mascotas.Nombres,
CONVERT(date, Mascotas.FechaNacimiento) AS FechaNacimiento,
DATEDIFF(YEAR, CONVERT(date, Mascotas.FechaNacimiento), GETDATE()) AS 'Edad',
Mascotas.Color,
Mascotas.Foto,
Personas.Nombres 'Dueño',
CASE Mascotas.Estado
    WHEN 'Regis' THEN 'Registrado'
    WHEN 'Aprob' THEN 'Aprobado'
    WHEN 'Recha' THEN 'Rechazado'
    ELSE 'Desconocido'
END AS Estado
FROM Mascotas
INNER JOIN Razas ON Razas.Id = Mascotas.IdRaza
INNER JOIN Animales ON Animales.Id = Razas.IdAnimal
INNER JOIN Personas ON Personas.Id = Mascotas.IdPersona
WHERE Mascotas.Estado = 'Regis'
END