using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Pages.SuperkatPages;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class SuperkatAdoption
{
    [Inject] public Navigation Navigation { get; set; } = null!;
    [Inject] public ISuperkattenListService SuperkattenService { get; set; } = null!;
    [Inject] public ISuperkatActionService SuperkattenActionService { get; set; } = null!;
    [Inject] public IGastgezinService GastegezinService { get; set; } = null!;

    [Parameter] public Guid GastgezinId { get; set; }
    private List<Superkat> _assignedSuperkatten = null!;
    private List<Superkat> _selectedSuperkatten = new();
    private string _emailAddress = string.Empty;
    private string _name = string.Empty; 
    private Gastgezin? _gastgezin;
    protected override async Task OnInitializedAsync()
    {
        _gastgezin = await GastegezinService.GetGastgezinAsync(GastgezinId);

        var superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        var assignedSuperkatten = superkatten
            .Where(o => o.GastgezinId == _gastgezin?.Id && !o.Reserved)
            .OrderBy(s => s.Number)
            .ToList();
        
        _selectedSuperkatten = assignedSuperkatten
            .Where(s => s.State != SuperkatState.Monitoring)
            .ToList();

        _assignedSuperkatten = assignedSuperkatten is null
            ? new()
            : assignedSuperkatten
                .Where(s => s.State == SuperkatState.Monitoring)
                .ToList() ?? new();
            ;
    }

    private void OnSendAdoptionPapers()
    {
        var adoptees = _selectedSuperkatten
            .Select(s => s.Id)
            .ToList();
        SuperkattenActionService.AdoptSuperkatten(GastgezinId, adoptees, _name, _emailAddress);
        Navigation.NavigateBack();
    }

    private void OnBack()
    {
        Navigation.NavigateBack();
    }

    private void AddSuperkatToSelectionAsync(Superkat superkat)
    {
        _assignedSuperkatten.Remove(superkat);
        _selectedSuperkatten.Add(superkat);
    }

    private void RemoveSuperkatFromSelection(Superkat superkat)
    {
        _selectedSuperkatten.Remove(superkat);
        _assignedSuperkatten.Add(superkat);
    }
}