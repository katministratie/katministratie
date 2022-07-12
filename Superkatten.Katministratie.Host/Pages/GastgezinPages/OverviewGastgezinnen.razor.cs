
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class OverviewGastgezinnen
{
    [Inject]
    private Navigation _navigation { get; set; }

    [Inject]
    private IGastgezinService _gastgezinnenService { get; set; }


    public List<Gastgezin> _gastgezinnen = new();

    protected override async Task OnInitializedAsync()
    {
        await UpdateListAsync();
    }

    private async Task UpdateListAsync()
    {
        if (_gastgezinnenService is null)
        {
            return;
        }

        var gastgezinnen = await _gastgezinnenService.GetAllGastgezinAsync();
        _gastgezinnen  = gastgezinnen 
            .AsQueryable()
            .OrderByDescending(item => item.Name)
            .ToList();
    }

    private void OnBackHome()
    {
        _navigation.NavigateTo("/");
    }

    public void OnCreateGastgezin()
    {
        _navigation.NavigateTo("CreateGastgezin");
    }

    public void OnGastgezinDeleted(Gastgezin gastgezin)
    {
        _gastgezinnen.Remove(gastgezin);
    }
}
