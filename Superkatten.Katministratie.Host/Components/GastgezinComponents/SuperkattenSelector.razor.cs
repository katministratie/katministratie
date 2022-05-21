using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Mappers;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class SuperkattenSelector
{
    [Inject]
    public ISuperkattenListService SuperkattenService { get; set; }

    [Inject]
    public ISuperkatMapper SuperkatMapper { get; set; }

    [Inject]
    public IGastgezinService GastgezinService { get; set; }

    [Parameter]
    public Gastgezin? Gastgezin { get; set; }

    [Parameter]
    public EventCallback OnFinishEdit { get; set; }


    private List<Superkat> AvailableSuperkatten { get; set; } = new List<Superkat>();

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
        Gastgezin?.Superkatten.Add(superkat);
    }

    private void RemoveSuperkatFromSelection(Superkat superkat)
    {
        AvailableSuperkatten.Add(superkat);
        Gastgezin?.Superkatten.Remove(superkat);
    }

    private async Task OnClose()
    {
        UpdateGastgezinSuperkatten();
        await OnFinishEdit.InvokeAsync();
    }

    private void UpdateGastgezinSuperkatten()
    {
        var updateParameters = new CreateOrUpdateGastgezinParameters
        {
            Name = Gastgezin?.Name ?? string.Empty,
            Address = Gastgezin.Address,
            City = Gastgezin.City,
            Phone = Gastgezin.Phone,
            Superkatten = Gastgezin
                .Superkatten
                .Select(SuperkatMapper.MapHostToContract)
                .ToList()
        };
        GastgezinService.UpdateGastgezinAsync(Gastgezin.Id, updateParameters);
    }
}