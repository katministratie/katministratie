
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class OverviewGastgezinnen
{ 
    public List<Gastgezin> _gastgezinnen = new();
    public List<Gastgezin> Gastgezinnen => _gastgezinnen;

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
        _navigationManager.NavigateTo("");
    }

    public void OnCreateGastgezin()
    {
        _navigationManager.NavigateTo("CreateGastgezin");
    }
}
