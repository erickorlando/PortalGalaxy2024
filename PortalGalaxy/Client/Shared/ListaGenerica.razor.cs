using Microsoft.AspNetCore.Components;

namespace PortalGalaxy.Client.Shared
{
    public partial class ListaGenerica<TItem>
    {
        [Parameter]
        public string Titulo { get; set; } = "Listado";

        [Parameter]
        public ICollection<TItem>? Lista { get; set; }

        [Parameter]
        public EventCallback BotonNuevo { get; set; }

        [Parameter]
        [EditorRequired]
        public RenderFragment Cabeceras { get; set; } = default!;

        [Parameter]
        [EditorRequired]
        public RenderFragment<TItem> Detalles { get; set; } = default!;

        private void OnNuevo()
        {
            BotonNuevo.InvokeAsync();
        }
    }
}
