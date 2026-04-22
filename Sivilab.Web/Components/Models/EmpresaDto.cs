using System.ComponentModel.DataAnnotations;

namespace Sivilab.Web.Components.Models;

public class EmpresaDto
{
    public int Id { get; set; }

    // Paso 1: Datos de acceso
    [Required(ErrorMessage = "El nombre es requerido")]
    public string Nombre { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo electrónico es requerido")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    public string CorreoElectronico { get; set; } = string.Empty;

    [Required(ErrorMessage = "La confirmación del correo es requerida")]
    [Compare(nameof(CorreoElectronico), ErrorMessage = "Los correos no coinciden")]
    public string ConfirmarCorreo { get; set; } = string.Empty;

    [Required(ErrorMessage = "La contraseña es requerida")]
    [MinLength(8, ErrorMessage = "La contraseña debe tener al menos 8 caracteres")]
    [RegularExpression(@"^(?=.*[A-Z])(?=.*\d).+$", ErrorMessage = "La contraseña debe contener al menos una mayúscula y un número")]
    public string Contrasena { get; set; } = string.Empty;

    [Required(ErrorMessage = "La confirmación de la contraseña es requerida")]
    [Compare(nameof(Contrasena), ErrorMessage = "Las contraseñas no coinciden")]
    public string ConfirmarContrasena { get; set; } = string.Empty;
    
    // Paso 3: Tipo de empresa
    //[Required(ErrorMessage = "El tipo de empresa es requerido")]
    public string TipoEmpresa { get; set; } = string.Empty;

    // Paso 4: Persona Física o Moral
    public bool EsPersonaMoral { get; set; } = true; // Por defecto Moral para UI, pero se elige con pestaña
    
    // Datos comunes o específicos
    public string RFC { get; set; } = string.Empty;
    public string Curp { get; set; } = string.Empty;
    
    // Persona Moral
    public string RazonSocial { get; set; } = string.Empty;
    public string TipoSociedad { get; set; } = string.Empty;
    
    // Persona Física (si no conoce CURP)
    public string PrimerApellido { get; set; } = string.Empty;
    public string SegundoApellido { get; set; } = string.Empty;
    public string Sexo { get; set; } = string.Empty;
    public DateTime? FechaNacimiento { get; set; }
    public string LugarNacimiento { get; set; } = string.Empty;

    // Datos Económicos (ambos)
    public string SectorActividad { get; set; } = string.Empty;
    public string Subsector { get; set; } = string.Empty;
    public string ActividadEconomica { get; set; } = string.Empty;

    // Paso 5: Ubicación
    //[Required(ErrorMessage = "El código postal es requerido")]
    public string CodigoPostal { get; set; } = string.Empty;

    //[Required(ErrorMessage = "La entidad federativa es requerida")]
    public string Estado { get; set; } = string.Empty;

    //[Required(ErrorMessage = "El municipio es requerido")]
    public string Municipio { get; set; } = string.Empty;

    //[Required(ErrorMessage = "La colonia es requerida")]
    public string Colonia { get; set; } = string.Empty;

    //[Required(ErrorMessage = "La calle es requerida")]
    public string Calle { get; set; } = string.Empty;

    //[Required(ErrorMessage = "El número exterior es requerido")]
    public string NumeroExterior { get; set; } = string.Empty;

    public string NumeroInterior { get; set; } = string.Empty;

    //[Required(ErrorMessage = "La referencia es requerida")]
    public string Referencia { get; set; } = string.Empty;

    // Paso 6: Acerca de la empresa
    public bool EsMismaRazonSocial { get; set; } = false;

    //[Required(ErrorMessage = "El número de empleados es requerido")]
    public string NumeroEmpleados { get; set; } = string.Empty;

    //[Required(ErrorMessage = "La descripción de la empresa es requerida")]
    public string DescripcionEmpresa { get; set; } = string.Empty;

    public bool TieneSucursales { get; set; } = false;

    public string LogotipoUrl { get; set; } = string.Empty;

    public string VideoEmpresaUrl { get; set; } = string.Empty;

    public string FacebookUrl { get; set; } = string.Empty;
    public string LinkedInUrl { get; set; } = string.Empty;
    public string SitioWebUrl { get; set; } = string.Empty;
    public string OtraRedSocial { get; set; } = string.Empty;
    
    // Sucursales
    public List<SucursalDto> Sucursales { get; set; } = new();

    // Paso 7: Contacto SNE
    public bool EsContactoRegistrado { get; set; } = true;

    // Si elige "Otro contacto"
    public string ContactoNombre { get; set; } = string.Empty;
    public string ContactoCorreo { get; set; } = string.Empty;

    //[Required(ErrorMessage = "Los apellidos son requeridos")]
    public string ContactoApellidos { get; set; } = string.Empty;

    //[Required(ErrorMessage = "El cargo del contacto es requerido")]
    public string ContactoCargo { get; set; } = string.Empty;

    //[Required(ErrorMessage = "El teléfono es requerido")]
    public string ContactoTelefono { get; set; } = string.Empty;

    public string ContactoExtension { get; set; } = string.Empty;

    // Paso 8: Términos y Condiciones
    //[AllowedValues(true, ErrorMessage = "Debe aceptar los términos y condiciones.")]
    public bool AceptaTerminos { get; set; }

    //[AllowedValues(true, ErrorMessage = "Debe asegurar que los datos son correctos.")]
    public bool AseguraDatosCorrectos { get; set; }

    //[AllowedValues(true, ErrorMessage = "Debe aceptar el aviso de privacidad y derechos ARCO.")]
    public bool AceptaDerechosArco { get; set; }

    public bool RecibirCvCandidatos { get; set; }

    // Propiedades para pasos futuros (se irán agregando/descomentando según avancemos)
    public string NombreComercial { get; set; } = string.Empty;
}

public class SucursalDto
{
    public Guid Id { get; set; } = Guid.NewGuid();

    [Required(ErrorMessage = "El número de sucursal es requerido")]
    public string NumeroSucursal { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre de la sucursal es requerido")]
    public string NombreSucursal { get; set; } = string.Empty;

    [Required(ErrorMessage = "El nombre del responsable es requerido")]
    public string NombreResponsable { get; set; } = string.Empty;

    [Required(ErrorMessage = "El correo del responsable es requerido")]
    [EmailAddress(ErrorMessage = "El formato del correo no es válido")]
    public string CorreoResponsable { get; set; } = string.Empty;

    public bool TrasladarDatosEmpresa { get; set; }
}
