
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SuperkatComponent
{
    [Parameter]
    public Superkat? Superkat { get; set; }

    public string SuperkatDisplayableNumber => Superkat is null 
        ? string.Empty 
        : Superkat.CatchDate.Year % 100 + "-" + Superkat.Number.ToString("000");
}
