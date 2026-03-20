using Sivilab.Web.Components.Models;

namespace Sivilab.Web.Services
{
    public interface IOfertaService
    {
        Task<bool> CrearOferta(OfertaEmpleoDto oferta);
    }
}
