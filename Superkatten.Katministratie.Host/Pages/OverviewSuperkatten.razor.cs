using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages;

public partial class OverviewSuperkatten
{
    [Inject]
    private ISuperkattenListService _superkattenService { get; set; }

    public List<Superkat> _superkatten = new();
    public List<Superkat> Superkatten => _superkatten;

    private bool ShowPrinterDialog { get; set; }

    protected override async Task OnInitializedAsync()
    {
        ShowPrinterDialog = true;
        await UpdateListAsync();
    }

    private async Task UpdateListAsync()
    {
        if (_superkattenService == null)
        {
            return;
        }

        var superkatten = await _superkattenService.GetAllSuperkattenAsync();
        _superkatten = superkatten
            .AsQueryable()
            .OrderByDescending(sk => sk.Number)
            .ToList();
    }

    private void OnBackHome()
    {
        _navigationManager.NavigateTo("");
    }
}
