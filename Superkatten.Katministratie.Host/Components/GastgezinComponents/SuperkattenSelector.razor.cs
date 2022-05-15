using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.GastgezinComponents;

public partial class SuperkattenSelector
{
    [Inject]
    private ISuperkattenListService? SuperkattenService { get; set; }

    [Parameter]
    public Gastgezin? Gastgezin { get; set; }

    [Parameter]
    public EventCallback OnFinishEdit { get; set; }


    private List<Superkat> AvailableSuperkatten { get; set; } = new List<Superkat>();

    protected async override Task OnInitializedAsync()
    {
        if (SuperkattenService is null)
        {
            return;
        }

        var superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        if (superkatten is null)
        {
            return;
        }

        if (superkatten.Count == 0)
        {
            return;
        }

        AvailableSuperkatten = superkatten!
            .AsQueryable()
            .OrderByDescending(sk => sk.Number)
            .ToList();
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
        await OnFinishEdit.InvokeAsync();
    }
}