using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class SuperkattenSelector
{
    [Inject]
    public ISuperkattenListService? SuperkattenService { get; set; }

    [Inject]
    public IGastgezinService? GastgezinService { get; set; }

    [Parameter]
    public Gastgezin? Gastgezin 
    { 
        set
        {
            if (value is null)
            {
                return;
            }

            GastgezinId = value.Id;

        }
    }

    [Parameter]
    public EventCallback OnFinishEdit { get; set; }

    private Guid GastgezinId { get; set; } = Guid.Empty;
    private List<Superkat> AssignedSuperkatten { get; set; } = new();
    private List<Superkat> AvailableSuperkatten { get; set; } = new();

    protected async override Task OnInitializedAsync()
    {
        if (SuperkattenService is null)
        {
            await OnFinishEdit.InvokeAsync();
            return;
        }

        var superkatten = await SuperkattenService.GetAllNotAssignedSuperkattenAsync();
        
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
        UpdateGastgezinSuperkatten();
        await OnFinishEdit.InvokeAsync();
    }

    private void UpdateGastgezinSuperkatten()
    {
        if (GastgezinService is null)
        {
            throw new Exception("No gastgezin service available");
        }

        if (GastgezinId == Guid.Empty)
        {
            throw new Exception("No gastgezin available");
        }

        var updateParameters = new AssignSuperkattenParameters
        {
            Id = GastgezinId,
            AssignedSuperkatten = AssignedSuperkatten
        };

        GastgezinService.AssignSuperkattenAsync(updateParameters);
    }
}