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
    private List<Superkat> _assignedSuperkatten;
    private List<Superkat> _selectedSuperkatten = new();
    private string _emailAddress = string.Empty;
    private Gastgezin? _gastgezin;
    protected override async Task OnInitializedAsync()
    {
        _gastgezin = await GastegezinService.GetGastgezinAsync(GastgezinId);

        var superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        var assignedSuperkatten = superkatten
            .Where(o => o.GastgezinId == _gastgezin?.Id)
            .OrderBy(s => s.Number)
            .ToList();
        _assignedSuperkatten = assignedSuperkatten ?? new();
    }

    private void OnSendAdoptionPapers()
    {
        SuperkattenActionService.ReserveSuperkatten(GastgezinId, _selectedSuperkatten);
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