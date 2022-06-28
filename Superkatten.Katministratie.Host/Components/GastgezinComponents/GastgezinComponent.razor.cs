using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinComponent
{
    [Inject]
    private NavigationManager? _navigationManager { get; set; }

    [Inject]
    private IGastgezinService? _gastgezinService { get; set; }

    private GastgezinEditMode _editMode = GastgezinEditMode.DisplayDetailsOnly;

    [Parameter]
    public Gastgezin? Gastgezin { get; set; }


    private void OnAdd()
    {
        _navigationManager?.NavigateTo("CreateGastgezin");
    }

    private async Task OnDelete()
    {
        if (_gastgezinService is null)
        {
            throw new Exception("No gastgezin service available");
        }

        if (Gastgezin is null)
        {
            throw new Exception("gastgezin is null");
        }

        await _gastgezinService!.DeleteGastgezinAsync(Gastgezin.Id);
    }

    private void OnEdit()
    {
        _editMode = GastgezinEditMode.EditGastgezinNaw;
    }

    private void OnAssignSuperkat()
    {
        _editMode = GastgezinEditMode.AssignSuperkatten;
    }

    private void OnFinishEdit()
    {
        _editMode = GastgezinEditMode.DisplayDetailsOnly;
    }
}
