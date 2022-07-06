using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class SuperkattenSelector
{
    [Inject]
    public ISuperkattenListService SuperkattenService { get; set; }

    [Inject]
    public IGastgezinService? GastgezinService { get; set; }

    [Parameter]
    public Guid GastgezinId
    {
        get;
        set;
    }

    [Parameter]
    public EventCallback OnFinish { get; set; }


    private List<Superkat> AssignedSuperkatten { get; set; } = new();
    private List<Superkat> AvailableSuperkatten { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        if (SuperkattenService is null)
        {
            return;
        }

        var superkatten = await SuperkattenService.GetAllNotAssignedSuperkattenAsync();
        AvailableSuperkatten = superkatten
            .AsQueryable()
            .OrderByDescending(s => s.Number)
            .ToList();

        superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        AssignedSuperkatten = superkatten
            .Where(o => o.GastgezinId == GastgezinId)
            .OrderByDescending(s => s.Number)
            .ToList();
    }

    private async Task AddSuperkatToSelection(Superkat superkat)
    {
        AvailableSuperkatten.Remove(superkat);
        AssignedSuperkatten.Add(superkat);

        await SuperkattenService.UpdateSuperkatAsync(
            superkat.Id,
            new UpdateSuperkatParameters
            {
                GastgezinId = GastgezinId
            }
        );
    }

    private async Task RemoveSuperkatFromSelection(Superkat superkat)
    {
        AvailableSuperkatten.Add(superkat);
        AssignedSuperkatten.Remove(superkat);

        // Remove by having null as guid
        await SuperkattenService.UpdateSuperkatAsync(superkat.Id, new UpdateSuperkatParameters());
    }

    private async Task OnClose()
    {
        await OnFinish.InvokeAsync();
    }
}