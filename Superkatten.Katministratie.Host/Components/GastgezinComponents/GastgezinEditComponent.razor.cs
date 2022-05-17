using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Forms;
using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public class Model
{
    [Required]
    public string Name { get; set; } = string.Empty;
    public string Address { get; set; } = string.Empty;
    public string City { get; set; } = string.Empty;
    public string Phone { get; set; } = string.Empty;
}

public partial class GastgezinEditComponent
{
    private Model modelData = new Model();

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

        var updateGastgezinParameters = new CreateOrUpdateGastgezinParameters()
        {
            Name = modelData.Name,
            Address = modelData.Address,
            City = modelData.City,
            Phone = modelData.Phone
        };

        _gastgezinService!.UpdateGastgezinAsync(Gastgezin.Id, updateGastgezinParameters);
    }
}
