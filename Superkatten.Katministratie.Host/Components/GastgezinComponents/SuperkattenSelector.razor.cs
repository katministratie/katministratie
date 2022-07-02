using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class SuperkattenSelector
{
    [Inject]
    public ISuperkattenListService? _superkattenService { get; set; }

    [Inject]
    public IGastgezinService? GastgezinService { get; set; }

    [Parameter]
    public Guid GastgezinId { get; set; }

    [Parameter]
    public EventCallback OnFinish { get; set; }



    private List<Superkat> AssignedSuperkatten { get; set; } = new();
    private List<Superkat> AvailableSuperkatten { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        var superkatten = await _superkattenService.GetAllNotAssignedSuperkattenAsync();
        
        var availableSuperkatten = superkatten
            .AsQueryable()
            .OrderByDescending(s => s.Number)
            .ToList();

        AvailableSuperkatten = availableSuperkatten;
    }

    private void AddSuperkatToSelection(Superkat superkat)
    {
        AvailableSuperkatten.Remove(superkat);
        AssignedSuperkatten.Add(superkat);
    }

    private void RemoveSuperkatFromSelection(Superkat superkat)
    {
        AvailableSuperkatten.Add(superkat);
        AssignedSuperkatten.Remove(superkat);
    }

    private async Task OnClose()
    {
        if (_superkattenService is null)
        { 
            throw new Exception("No superkatten service available");
        }

        if (GastgezinId == Guid.Empty)
        {
            throw new Exception("No gastgezin id");
        }

        foreach(var assignedSuperkat in AssignedSuperkatten)
        {
            var updateSuperkatParameters = new UpdateSuperkatParameters
            {
                GastgezinId = GastgezinId
            };

            await _superkattenService.UpdateSuperkatAsync(assignedSuperkat.Id, updateSuperkatParameters);
        }
    }
}