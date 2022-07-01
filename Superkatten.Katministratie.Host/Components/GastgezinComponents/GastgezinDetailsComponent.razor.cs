using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinDetailsComponent
{
    [Parameter]
    public Gastgezin Gastgezin
    {
        set
        {
            UpdateData(value);
        }
    }

    public string Name { get; private set; } = string.Empty;
    public string Address { get; private set; } = string.Empty;
    public string City { get; private set; } = string.Empty;
    public string Phone { get; private set; } = string.Empty;

    private void UpdateData(Gastgezin? gastgezin)
    {
        if (gastgezin is null)
        {
            return;
        }

        Name = gastgezin.Name;
        Address = gastgezin.Address ?? string.Empty;
        City = gastgezin.City ?? string.Empty;
        Phone = gastgezin.Phone ?? string.Empty;
    }
}
