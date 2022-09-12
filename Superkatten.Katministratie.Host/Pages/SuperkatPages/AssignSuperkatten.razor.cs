using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class AssignSuperkatten
{
    [Inject]
    public ISuperkattenListService SuperkattenService { get; set; } = null!;

    [Inject]
    public IGastgezinService GastgezinService { get; set; } = null!;

    [Inject]
    public Navigation Navigation { get; set; } = null!;

    [Parameter]
    public Guid GastgezinId { get; set; }

    [Parameter]
    public EventCallback OnFinish { get; set; }


    private Gastgezin? _gastgezin;
    private List<Superkat>? AssignedSuperkatten { get; set; }
    private List<Superkat>? AvailableSuperkatten { get; set; }

    protected override async Task OnInitializedAsync()
    {
        _gastgezin = await GastgezinService.GetGastgezinAsync(GastgezinId);

        var superkatten = await SuperkattenService.GetAllNotAssignedSuperkattenAsync();
        AvailableSuperkatten = superkatten
            .AsQueryable()
            .OrderByDescending(s => s.Number)
            .ToList();

        superkatten = await SuperkattenService.GetAllSuperkattenAsync();
        AssignedSuperkatten = superkatten
            .Where(o => o.GastgezinId == _gastgezin?.Id)
            .OrderByDescending(s => s.Number)
            .ToList();
    }

    private static string GetSuperkatNumber(Superkat superkat)
    {
        return superkat.CatchDate.Year.ToString() + "-" + superkat.Number.ToString("000");
    }

    private Task AddSuperkatToSelectionAsync(Superkat superkat)
    {
        if (_gastgezin is null)
        {
            return Task.CompletedTask;
        }

        AvailableSuperkatten?.Remove(superkat);
        AssignedSuperkatten?.Add(superkat);

        return SuperkattenService.ReallocateSuperkatAsync(
            superkat.Id,
            new ReallocateSuperkatParameters
            {
                CatArea = CatArea.HostFamily,
                CageNumber = 1,
                GastgezinId = _gastgezin.Id
            }
        );
    }

    private void RemoveSuperkatFromSelection(Superkat superkat)
    {
        AvailableSuperkatten?.Add(superkat);
        AssignedSuperkatten?.Remove(superkat);

        Navigation.NavigateTo($"MoveSuperkat/{superkat.Id}");
    }

    private async Task OnClose()
    {
        await OnFinish.InvokeAsync();
        Navigation.NavigateBack();
    }
}
