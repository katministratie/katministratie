using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components;

public partial class SuperkattenSelector
{
    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

    private List<Superkat> AvailableSuperkatten { get; set; } = new List<Superkat>();

    [Parameter]
    public List<Superkat> SelectedSuperkatten { get; set; } = new List<Superkat>();


    public Guid _selectedSuperkatId { get; set; }


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

        if (superkatten?.Count == 0)
        {
            return;
        }

        AvailableSuperkatten = superkatten!
            .AsQueryable()
            .OrderByDescending(sk => sk.Number)
            .ToList();
    }

    private void SelectSuperkat(Guid selectedSuperkatId)
    {
        _selectedSuperkatId = selectedSuperkatId;
    }

    private void AddSuperkatToSelection()
    {
        if (_selectedSuperkatId == Guid.Empty)
        {
        }

        var superkat = AvailableSuperkatten
            .Where(s => s.Id == _selectedSuperkatId)
            .FirstOrDefault();

        AvailableSuperkatten.Remove(superkat);
        SelectedSuperkatten.Add(superkat);
    }
}