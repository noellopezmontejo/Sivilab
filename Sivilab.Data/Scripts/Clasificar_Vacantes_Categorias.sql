-- Script para crear la tabla de categorías y enlazarlas a Vacantes
-- Esto centraliza el Fuzz Match que usábamos en código hacia la base de datos.
USE [SU_BASE_DE_DATOS_AQUI] -- Sustituir con la base de datos real (ej. dbempleos o Sivilab)
GO

-- 1. Crear el Catálogo de Categorías
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CatCategorias' AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[CatCategorias](
        [CveCategoria] [int] IDENTITY(1,1) NOT NULL,
        [NombreCategoria] [nvarchar](100) NOT NULL,
        [Activo] [bit] DEFAULT 1,
        CONSTRAINT [PK_CatCategorias] PRIMARY KEY CLUSTERED ([CveCategoria] ASC)
    )
END
GO

-- 2. Insertar las Categorías Base (Asegúrate que coincidan con las reales de tu sistema)
-- Usamos NOT EXISTS para no duplicar en múltiples corridas
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Contabilidad y Finanzas') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Contabilidad y Finanzas');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Administración y Auditorias') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Administración y Auditorias');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Atención a Clientes y Ventas') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Atención a Clientes y Ventas');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Tecnología y Sistemas') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Tecnología y Sistemas');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Recursos Humanos') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Recursos Humanos');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Distribución y Logistica') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Distribución y Logistica');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Oficios') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Oficios');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Educación y Capacitación') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Educación y Capacitación');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Salud') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Salud');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Seguridad') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Seguridad');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Técnicos Especializados') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Técnicos Especializados');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Industria') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Industria');
IF NOT EXISTS (SELECT 1 FROM [dbo].[CatCategorias] WHERE NombreCategoria = 'Otros') INSERT INTO [dbo].[CatCategorias] (NombreCategoria) VALUES ('Otros');
GO

-- 3. Añadir el Campo a la Tabla de Vacantes
-- Reemplazar "[dbempleos].[Vacantes]" por el nombre real de tu tabla de vacantes principal
DECLARE @TablaVacantes NVARCHAR(200) = 'Vacantes'; -- Ajustar al nombre real de la tabla (ej. 'dbo.CatVacante' o 'dbempleos.Vacante')

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CveCategoria' AND Object_ID = Object_ID(@TablaVacantes))
BEGIN
    EXEC('ALTER TABLE ' + @TablaVacantes + ' ADD CveCategoria INT NULL;');
    -- Crear Foránea si aplican las llaves
    -- EXEC('ALTER TABLE ' + @TablaVacantes + ' ADD CONSTRAINT FK_Vacante_Categoria FOREIGN KEY (CveCategoria) REFERENCES CatCategorias(CveCategoria);');
END
GO

-- 4. Actualizar la información histórica existente (Traducción del C# Fuzzy Match -> SQL)
-- Realiza el Mapeo basándose en PuestoOfrecido, OtrosReq, Actividades y Habilidades.
-- Reemplazar "[Vacantes]" por el nombre de tu tabla de vacantes real.

UPDATE v
SET v.CveCategoria = ISNULL(
    (
        SELECT TOP 1 CveCategoria 
        FROM CatCategorias c
        WHERE 
            -- Busca coincidencia exacta con el nombre de la categoría
            txt.Texto LIKE '%' + LOWER(c.NombreCategoria) + '%'
            -- O busca por las palabras clave predefinidas (Ajustando a las categorías reales que podrías tener)
            OR (c.NombreCategoria = 'Contabilidad y Finanzas' AND (txt.Texto LIKE '%contab%' OR txt.Texto LIKE '%contador%' OR txt.Texto LIKE '%finanzas%' OR txt.Texto LIKE '%auditor%' OR txt.Texto LIKE '%fiscal%' OR txt.Texto LIKE '%auxiliar contable%' OR txt.Texto LIKE '%tesorero%'))
            OR (c.NombreCategoria = 'Administración y Auditorias' AND (txt.Texto LIKE '%administra%' OR txt.Texto LIKE '%auditor%' OR txt.Texto LIKE '%gerente%' OR txt.Texto LIKE '%asistente%' OR txt.Texto LIKE '%recepcionista%'))
            OR (c.NombreCategoria = 'Atención a Clientes y Ventas' AND (txt.Texto LIKE '%cliente%' OR txt.Texto LIKE '%atención%' OR txt.Texto LIKE '%call center%' OR txt.Texto LIKE '%mostrador%' OR txt.Texto LIKE '%cajero%' OR txt.Texto LIKE '%venta%' OR txt.Texto LIKE '%comercial%' OR txt.Texto LIKE '%ejecutivo%' OR txt.Texto LIKE '%vendedor%' OR txt.Texto LIKE '%asesor%'))
            OR (c.NombreCategoria = 'Tecnología y Sistemas' AND (txt.Texto LIKE '%sistemas%' OR txt.Texto LIKE '%desarrollador%' OR txt.Texto LIKE '%programador%' OR txt.Texto LIKE '%tecnología%' OR txt.Texto LIKE '%software%' OR txt.Texto LIKE '%it %' OR txt.Texto LIKE '%soporte%' OR txt.Texto LIKE '%redes%'))
            OR (c.NombreCategoria = 'Recursos Humanos' AND (txt.Texto LIKE '%recursos humanos%' OR txt.Texto LIKE '%rh %' OR txt.Texto LIKE '%recluta%' OR txt.Texto LIKE '%talento%' OR txt.Texto LIKE '%psicolo%'))
            OR (c.NombreCategoria = 'Distribución y Logistica' AND (txt.Texto LIKE '%logística%' OR txt.Texto LIKE '%distribución%' OR txt.Texto LIKE '%chofer%' OR txt.Texto LIKE '%almacen%' OR txt.Texto LIKE '%inventario%' OR txt.Texto LIKE '%repartidor%' OR txt.Texto LIKE '%bodega%'))
            OR (c.NombreCategoria = 'Oficios' AND (txt.Texto LIKE '%carpintero%' OR txt.Texto LIKE '%electricista%' OR txt.Texto LIKE '%plomero%' OR txt.Texto LIKE '%albañil%' OR txt.Texto LIKE '%pintor%' OR txt.Texto LIKE '%jardinero%'))
            OR (c.NombreCategoria = 'Educación y Capacitación' AND (txt.Texto LIKE '%educación%' OR txt.Texto LIKE '%maestro%' OR txt.Texto LIKE '%profesor%' OR txt.Texto LIKE '%docente%' OR txt.Texto LIKE '%instructor%' OR txt.Texto LIKE '%capacitador%'))
            OR (c.NombreCategoria = 'Salud' AND (txt.Texto LIKE '%salud%' OR txt.Texto LIKE '%médico%' OR txt.Texto LIKE '%enfermer%' OR txt.Texto LIKE '%clínica%' OR txt.Texto LIKE '%terapia%' OR txt.Texto LIKE '%odontólogo%'))
            OR (c.NombreCategoria = 'Seguridad' AND (txt.Texto LIKE '%seguridad%' OR txt.Texto LIKE '%guardia%' OR txt.Texto LIKE '%vigilante%' OR txt.Texto LIKE '%prevención%'))
            OR (c.NombreCategoria = 'Técnicos Especializados' AND (txt.Texto LIKE '%técnico%' OR txt.Texto LIKE '%soporte técnico%' OR txt.Texto LIKE '%mantenimiento%'))
            OR (c.NombreCategoria = 'Industria' AND (txt.Texto LIKE '%industria%' OR txt.Texto LIKE '%producción%' OR txt.Texto LIKE '%manufactura%' OR txt.Texto LIKE '%fábrica%' OR txt.Texto LIKE '%operador%'))
    ), 
    13 -- 13 se refiere a "Otros" (Cuando no hace Match con nada)
)
FROM [Vacantes] v -- !Ajustar nombre de la tabla
CROSS APPLY (
    -- Concatenamos todos los campos solicitados para hacer el cruce más certero
    SELECT LOWER(
        ISNULL(CAST(v.PuestoOfrecido AS NVARCHAR(MAX)), '') + ' ' + 
        ISNULL(CAST(v.OtrosReq AS NVARCHAR(MAX)), '') + ' ' + 
        ISNULL(CAST(v.Actividades AS NVARCHAR(MAX)), '') + ' ' + 
        ISNULL(CAST(v.Habilidades AS NVARCHAR(MAX)), '')
    ) AS Texto
) txt
WHERE v.CveCategoria IS NULL; -- Solo afecta a los registros nuevos o que no tengan categoría asignada
GO
