using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Helpers;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinComponent
{

    [Inject]
    private IGastgezinService? _gastgezinService { get; set; }
    
    [Inject]
    private Navigation? Navigation { get; set; }


    [Parameter]
    public Gastgezin? Gastgezin 
    { 
        get => _gastgezin; 
        set => _gastgezin = value; 
    }

    [Parameter]
    public EventCallback<Gastgezin> OnGastgezinDeleted { get; set; }


    private Gastgezin? _gastgezin;

    private HostFamilyComponentEditMode _editMode = HostFamilyComponentEditMode.DisplayDetailsOnly;

    private async Task OnDelete()
    {
        if (_gastgezinService is null)
        {
            throw new Exception("No gastgezin service available");
        }

        if (_gastgezin is null)
        {
            throw new Exception("gastgezin is null");
        }

        await _gastgezinService!.DeleteGastgezinAsync(_gastgezin.Id);
        await OnGastgezinDeleted.InvokeAsync(_gastgezin);
    }

    private void OnEdit()
    {
        _editMode = HostFamilyComponentEditMode.EditGastgezinNaw;
    }

    private void OnAssignSuperkat()
    {
        if (_gastgezin is null)
        {
            return;
        }

        Navigation.NavigateTo($"/AssignSuperkatten/{_gastgezin.Id}");
    }

    private async Task OnFinishEdit()
    {
        _editMode = HostFamilyComponentEditMode.DisplayDetailsOnly;

        if (_gastgezin is null)
        {
            return;
        }

        if (_gastgezinService is null)
        {
            return;
        }

        Gastgezin = await _gastgezinService.GetGastgezinAsync(_gastgezin.Id);
    }
}
