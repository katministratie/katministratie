using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinDetailsComponent
{
    [Parameter]
    public Gastgezin? Gastgezin
    {
        set
        {
            if (value is null)
            {
                return;
            }

            Name = value.Name;
            Address = value.Address ?? string.Empty;
            City = value.City ?? string.Empty;
            Phone = value.Phone ?? string.Empty;
        }
    }

    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;
}
