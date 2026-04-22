using Microsoft.AspNetCore.Components;
using Sivilab.PortalEmpleo.Models;

namespace Sivilab.PortalEmpleo.Components.Pages
{
    public partial class DetailOferts : ComponentBase
    {
        [Parameter]
        public OfertaModel? Oferta { get; set; }
    }
}