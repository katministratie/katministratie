using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Helpers;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class GastgezinComponent : ComponentBase
{
    [Inject] public ISuperkattenListService SuperkattenService { get; set; } = null!;
    [Inject] public IGastgezinService GastgezinService { get; set; } = null!;    
    [Inject] private Navigation Navigation { get; set; } = null!;

        [Parameter] public Gastgezin Gastgezin { get; set; } = null!;
    [Parameter] public EventCallback<Gastgezin> OnGastgezinDeleted { get; set; }


    private HostFamilyComponentEditMode _editMode = HostFamilyComponentEditMode.DisplayDetailsOnly;

    private bool _disableAdoption;

    protected override async Task OnInitializedAsync()
    {
        var superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        _disableAdoption = !superkatten.Any(o => o.GastgezinId == Gastgezin?.Id);
    }

    private async Task OnDelete()
    {
        await GastgezinService!.DeleteGastgezinAsync(Gastgezin.Id);
        await OnGastgezinDeleted.InvokeAsync(Gastgezin);
    }

    private void OnEdit()
    {
        _editMode = HostFamilyComponentEditMode.EditGastgezinNaw;
    }

    private void OnAssignSuperkat()
    {
        Navigation?.NavigateTo($"/AssignSuperkatten/{Gastgezin.Id}");
    }

    private void OnStartAdoption()
    {
        Navigation?.NavigateTo($"/SuperkatAdoption/{Gastgezin.Id}");
    }

    private async Task OnFinishEdit()
    {
        _editMode = HostFamilyComponentEditMode.DisplayDetailsOnly;
        var gastgezin = await GastgezinService.GetGastgezinAsync(Gastgezin.Id);
        Gastgezin = gastgezin ?? throw new Exception($"No gastgezin available with id {Gastgezin.Id}");
    }
}
