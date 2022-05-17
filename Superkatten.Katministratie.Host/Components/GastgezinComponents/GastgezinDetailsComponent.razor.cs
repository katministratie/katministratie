using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinDetailsComponent
{
    [Parameter]
    public Gastgezin? Gastgezin { get; set; }
}
