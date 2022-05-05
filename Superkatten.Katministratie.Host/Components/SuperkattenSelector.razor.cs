using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components;

public partial class SuperkattenSelector
{
    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

    [Parameter]
    public Gastgezin Gastgezin { get; set; }

    [Parameter]
    public EventCallback GastgezinChanged { get; set; }


    private List<Superkat> AvailableSuperkatten { get; set; } = new List<Superkat>();

    public List<Superkat> SelectedSuperkatten { get; set; } = new List<Superkat>();

    protected async override Task OnInitializedAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        var superkatten = await _superkattenService.GetAllSuperkattenAsync();
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
        SelectedSuperkatten.Add(superkat);
    }

    private void RemoveSuperkatFromSelection(Superkat superkat)
    {
        AvailableSuperkatten.Add(superkat);
        SelectedSuperkatten.Remove(superkat);
    }
}