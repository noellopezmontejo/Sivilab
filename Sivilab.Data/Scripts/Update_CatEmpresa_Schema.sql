
-- Script para actualizar la estructura de la tabla CatEmpresa
-- Basado en el modelo Sivilab.Web.Components.Models.EmpresaDto

IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CatEmpresa' AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[CatEmpresa](
        [Id] [int] IDENTITY(1,1) NOT NULL,
        CONSTRAINT [PK_CatEmpresa] PRIMARY KEY CLUSTERED ([Id] ASC)
    )
END
GO

-- Paso 1: Datos de Acceso
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Nombre' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Nombre NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CorreoElectronico' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD CorreoElectronico NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Contrasena' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Contrasena NVARCHAR(255) NULL; -- Recomendado almacenar hash

-- Paso 3: Tipo de Empresa
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'TipoEmpresa' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD TipoEmpresa NVARCHAR(50) NULL;

-- Paso 4: Persona Física o Moral
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'EsPersonaMoral' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD EsPersonaMoral BIT DEFAULT 1;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RFC' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD RFC NVARCHAR(13) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Curp' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Curp NVARCHAR(18) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RazonSocial' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD RazonSocial NVARCHAR(255) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'TipoSociedad' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD TipoSociedad NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'PrimerApellido' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD PrimerApellido NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'SegundoApellido' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD SegundoApellido NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Sexo' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Sexo NVARCHAR(20) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'FechaNacimiento' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD FechaNacimiento DATETIME NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'LugarNacimiento' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD LugarNacimiento NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'SectorActividad' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD SectorActividad NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Subsector' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Subsector NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ActividadEconomica' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ActividadEconomica NVARCHAR(100) NULL;

-- Paso 5: Ubicación
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CodigoPostal' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD CodigoPostal NVARCHAR(10) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Estado' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Estado NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Municipio' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Municipio NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Colonia' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Colonia NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Calle' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Calle NVARCHAR(150) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NumeroExterior' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD NumeroExterior NVARCHAR(20) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NumeroInterior' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD NumeroInterior NVARCHAR(20) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'Referencia' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD Referencia NVARCHAR(255) NULL;

-- Paso 6: Acerca de la empresa
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'EsMismaRazonSocial' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD EsMismaRazonSocial BIT DEFAULT 0;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NombreComercial' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD NombreComercial NVARCHAR(255) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NumeroEmpleados' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD NumeroEmpleados NVARCHAR(50) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'DescripcionEmpresa' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD DescripcionEmpresa NVARCHAR(MAX) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'TieneSucursales' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD TieneSucursales BIT DEFAULT 0;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'LogotipoUrl' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD LogotipoUrl NVARCHAR(500) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'VideoEmpresaUrl' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD VideoEmpresaUrl NVARCHAR(500) NULL;
    
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'FacebookUrl' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD FacebookUrl NVARCHAR(500) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'LinkedInUrl' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD LinkedInUrl NVARCHAR(500) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'SitioWebUrl' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD SitioWebUrl NVARCHAR(500) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'OtraRedSocial' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD OtraRedSocial NVARCHAR(500) NULL;

-- Paso 7: Contacto SNE
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'EsContactoRegistrado' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD EsContactoRegistrado BIT DEFAULT 1;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoNombre' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoNombre NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoCorreo' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoCorreo NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoApellidos' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoApellidos NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoCargo' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoCargo NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoTelefono' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoTelefono NVARCHAR(20) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'ContactoExtension' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD ContactoExtension NVARCHAR(10) NULL;

-- Paso 8: Términos y Condiciones
IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AceptaTerminos' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD AceptaTerminos BIT DEFAULT 0;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AseguraDatosCorrectos' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD AseguraDatosCorrectos BIT DEFAULT 0;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'AceptaDerechosArco' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD AceptaDerechosArco BIT DEFAULT 0;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'RecibirCvCandidatos' AND Object_ID = Object_ID(N'CatEmpresa'))
    ALTER TABLE CatEmpresa ADD RecibirCvCandidatos BIT DEFAULT 0;

GO

-- Tabla Auxiliar para Sucursales (CatSucursal)
IF NOT EXISTS (SELECT * FROM sys.tables WHERE name = 'CatSucursal' AND type = 'U')
BEGIN
    CREATE TABLE [dbo].[CatSucursal](
        [Id] [uniqueidentifier] NOT NULL DEFAULT NEWID(),
        [EmpresaId] [int] NOT NULL,
        CONSTRAINT [PK_CatSucursal] PRIMARY KEY CLUSTERED ([Id] ASC),
        CONSTRAINT [FK_CatSucursal_CatEmpresa] FOREIGN KEY ([EmpresaId]) REFERENCES [dbo].[CatEmpresa] ([Id])
    )
END
GO

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NumeroSucursal' AND Object_ID = Object_ID(N'CatSucursal'))
    ALTER TABLE CatSucursal ADD NumeroSucursal NVARCHAR(50) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NombreSucursal' AND Object_ID = Object_ID(N'CatSucursal'))
    ALTER TABLE CatSucursal ADD NombreSucursal NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'NombreResponsable' AND Object_ID = Object_ID(N'CatSucursal'))
    ALTER TABLE CatSucursal ADD NombreResponsable NVARCHAR(150) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'CorreoResponsable' AND Object_ID = Object_ID(N'CatSucursal'))
    ALTER TABLE CatSucursal ADD CorreoResponsable NVARCHAR(100) NULL;

IF NOT EXISTS(SELECT * FROM sys.columns WHERE Name = N'TrasladarDatosEmpresa' AND Object_ID = Object_ID(N'CatSucursal'))
    ALTER TABLE CatSucursal ADD TrasladarDatosEmpresa BIT DEFAULT 0;
GO
