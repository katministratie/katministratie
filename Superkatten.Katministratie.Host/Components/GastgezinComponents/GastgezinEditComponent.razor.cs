using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinEditComponent
{
    [Parameter]
    public Gastgezin Gastgezin 
    {
        set
        {
            if (value is null)
            {
                return;
            }

            GastgezinId = value.Id;

            GastgezinData.Name = value.Name;
            GastgezinData.Address = value.Address ?? string.Empty;
            GastgezinData.City = value.City ?? string.Empty;
            GastgezinData.Phone = value.Phone ?? string.Empty;
        }
    }

    [Parameter]
    public EventCallback OnFinishEdit { get; set; }

    [Inject]
    private IGastgezinService? _gastgezinService { get; set; }

    private Guid GastgezinId = Guid.Empty;
    private readonly NawData GastgezinData = new();

    private async Task OnFinish(EditContext editContext)
    {
        Gastgezin = (Gastgezin)editContext.Model;
        UpdateGastgezin();
        await OnFinishEdit.InvokeAsync();
    }

    private async Task OnFinishFailed(EditContext editContext)
    {
        await OnFinishEdit.InvokeAsync();
    }

    private void UpdateGastgezin()
    {
        if (GastgezinId == Guid.Empty)
        {
            return;
        }

        var updateGastgezinParameters = new CreateUpdateGastgezinParameters()
        {
            Name = GastgezinData.Name,
            Address = GastgezinData.Address,
            City = GastgezinData.City,
            Phone = GastgezinData.Phone
        };

        _gastgezinService!.UpdateGastgezinAsync(GastgezinId, updateGastgezinParameters);
    }
}
