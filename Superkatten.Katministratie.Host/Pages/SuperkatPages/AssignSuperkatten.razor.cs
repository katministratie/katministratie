using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class AssignSuperkatten
{
    [Inject]
    public ISuperkattenListService? _superkattenService { get; set; }

    [Inject]
    public IGastgezinService? _gastgezinService { get; set; }

    [Inject]
    public Navigation? _navigation { get; set; }

    [Parameter]
    public Guid GastgezinId { get; set; }

    [Parameter]
    public EventCallback OnFinish { get; set; }


    private Gastgezin? _gastgezin;
    private List<Superkat> AssignedSuperkatten { get; set; } = new();
    private List<Superkat> AvailableSuperkatten { get; set; } = new();

    protected override async Task OnInitializedAsync()
    {
        if (_superkattenService is null)
        {
            return;
        }

        if (_gastgezinService is null)
        {
            return;
        }

        _gastgezin = await _gastgezinService.GetGastgezinAsync(GastgezinId);

        var superkatten = await _superkattenService.GetAllNotAssignedSuperkattenAsync();
        AvailableSuperkatten = superkatten
            .AsQueryable()
            .OrderByDescending(s => s.Number)
            .ToList();

        superkatten = await _superkattenService.GetAllSuperkattenAsync();
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
        if (_superkattenService is null)
        {
            return Task.CompletedTask;
        }

        AvailableSuperkatten.Remove(superkat);
        AssignedSuperkatten.Add(superkat);

        return _superkattenService.UpdateSuperkatAsync(
            superkat.Id,
            new UpdateSuperkatParameters
            {
                GastgezinId = _gastgezin.Id
            }
        );
    }

    private Task RemoveSuperkatFromSelection(Superkat superkat)
    {
        if (_superkattenService is null)
        {
            return Task.CompletedTask;
        }

        AvailableSuperkatten.Add(superkat);
        AssignedSuperkatten.Remove(superkat);

        // Remove by having null as guid
        return _superkattenService.UpdateSuperkatAsync(superkat.Id, new UpdateSuperkatParameters());
    }

    private async Task OnClose()
    {
        if (_navigation is null)
        {
            return;
        }

        await OnFinish.InvokeAsync();
        _navigation.NavigateBack();
    }
}
