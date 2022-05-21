using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinEditComponent
{
    [Parameter]
    public Gastgezin? Gastgezin { get; set; }

    [Parameter]
    public EventCallback OnFinishEdit { get; set; }

    [Inject]
    private IGastgezinService? _gastgezinService { get; set; }

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
        if (Gastgezin is null)
        {
            throw new Exception("Internal error; geen gastgezin meegegeven tijdens opslaan.");
        }

        var updateGastgezinParameters = new CreateOrUpdateNawGastgezinParameters()
        {
            Name = Gastgezin.Name,
            Address = Gastgezin.Address,
            City = Gastgezin.City,
            Phone = Gastgezin.Phone
        };

        _gastgezinService!.UpdateGastgezinAsync(Gastgezin.Id, updateGastgezinParameters);
    }
}
