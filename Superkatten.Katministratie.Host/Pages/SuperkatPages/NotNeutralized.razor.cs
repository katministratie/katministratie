using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.LocalStorage;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Interfaces;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class NotNeutralized
{
    [Inject] private Navigation _navigation { get; set; } = null!;

    [Inject] private ISuperkattenListService _superkattenListService { get; set; } = null!;

    [Inject] private ILocalStorageService _localStorageService { get; set; } = null!;


    private List<Superkat> Superkatten { get; set; } = new();
    private readonly static List<NeutralizedLocationView> _neutralizeFilterOptions = Enum.GetValues(typeof(NeutralizedLocationView)).Cast<NeutralizedLocationView>().ToList();
    private static List<string> _neutralizeFilterOptionNames = null!;



    protected override async Task OnInitializedAsync()
    {
        _neutralizeFilterOptionNames = _neutralizeFilterOptions.Select(x => x.ToString()).ToList();
    }

    private async Task UpdateListAsync(NeutralizedLocationView filter)
    {
        var unsortedSuperkatten = await _superkattenListService.GetAllNotNeutralizedSuperkattenAsync();

        var superkatten = unsortedSuperkatten.OrderBy(o => o.UniqueNumber).ToList();

        Superkatten = filter switch
        {
            NeutralizedLocationView.All => superkatten,
            NeutralizedLocationView.Refuge => superkatten.Where(o => o.GastgezinId == null).ToList(),
            NeutralizedLocationView.HostFamily => superkatten.Where(o => o.GastgezinId != null).ToList(),
            _ => throw new InvalidEnumArgumentException(nameof(filter), (int)filter, typeof(NeutralizedLocationView))
        };
    }

    private async Task OnSelectFilter(NeutralizedLocationView selectedFilter)
    {
        await UpdateListAsync(selectedFilter);
    }

    private void OnBackHome()
    {
        _navigation.NavigateTo("/");
    }
}
