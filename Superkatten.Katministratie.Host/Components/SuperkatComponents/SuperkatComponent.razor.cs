
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SuperkatComponent
{
    [Parameter]
    public Superkat? Superkat { get; set; }

    public string SuperkatDisplayableNumber => Superkat!.DisplayableNumber;
}
