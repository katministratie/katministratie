using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.LocalStorage;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class OverviewSuperkatten
{
    [Inject]
    private Navigation  _navigation { get; set; }

    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

    [Inject]
    private ILocalStorageService _localStorageService { get; set; }

    [Inject]
    private ISuperkatActionService _superkatActionService { get; set; }


    private List<Superkat> Superkatten { get; set; } = new();

    private bool _showSimpleListView = false;

    private async Task OnChangeSimpleListViewAsync()
    {
        Superkatten.Clear();
        
        _showSimpleListView = !_showSimpleListView;

        await _localStorageService.SetItem(
            LocalStorageItems.LOCALSTORAGE_SETTING_SUPERKATTENLIST_TYPE,
            _showSimpleListView);

        await UpdateListAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        _showSimpleListView = await _localStorageService.GetItem<bool>(LocalStorageItems.LOCALSTORAGE_SETTING_SUPERKATTENLIST_TYPE);
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
            return;
        }

        if (superkatten?.Count == 0)
        {
            return;
        }

        Superkatten = superkatten!
            .AsQueryable()
            .OrderByDescending(sk => sk.Number)
            .ToList();
    }

    private void OnBackHome()
    {
        _navigation.NavigateBack();
    }

    private Task Print(string printername)
    {
        return _superkatActionService.CreateSuperkatCageCardAsync(new());
    }
}
