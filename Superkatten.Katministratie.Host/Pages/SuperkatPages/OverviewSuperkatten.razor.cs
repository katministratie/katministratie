using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class OverviewSuperkatten
{
    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

    public string LoadingInfoMessage { get; private set; } = string.Empty;
    private List<Superkat> Superkatten { get; set; } = new();

    private Guid _selectedSuperkatId;

    protected override async Task OnInitializedAsync()
    {
        LoadingInfoMessage = "Inlezen van alle superkatten";
        await UpdateListAsync();
    }

    private async Task UpdateListAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        var superkatten = await _superkattenService.GetAllSuperkattenAsync();
        if (superkatten is null)
        {
            LoadingInfoMessage = "Iets ging fout met inlezen.";
            return;
        }

        if (superkatten?.Count == 0)
        {
            LoadingInfoMessage = "Geen superkatten beschikbaar.";
            return;
        }

        Superkatten = superkatten!
            .AsQueryable()
            .OrderByDescending(sk => sk.Number)
            .ToList();
    }

    private void OnBackHome()
    {
        _navigationManager.NavigateTo("");
    }

    private async Task Print(string printername)
    {
        if (_selectedSuperkatId == Guid.Empty)
        {
            return;
        }

        var parameters = new SuperkatCageCardPrintParameters
        {
            Id = _selectedSuperkatId,
            PrinterName = printername
        };
        await _superkatActionService.CreateSuperkatCageCardAsync(parameters);
    }
}
