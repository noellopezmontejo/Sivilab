using Sivilab.Web.Components.Models;

namespace Sivilab.Web.Services;

public interface IEmpresaService
{
    Task<bool> CrearEmpresa(EmpresaDto empresa);
}
