SELECT Razas.Id AS Id,
    Animales.Nombre AS Animal,
    Razas.Nombre AS Raza,
    Razas.Estado
FROM Razas
INNER JOIN Animales ON Animales.Id = Razas.IdAnimal
WHERE Animales.Nombre = 'Perro';
