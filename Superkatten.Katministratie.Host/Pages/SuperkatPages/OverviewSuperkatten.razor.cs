﻿using Microsoft.AspNetCore.Components;
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


    private string LoadingInfoMessage { get; set; } = string.Empty;
    private List<Superkat> Superkatten { get; set; } = new();

    private bool _showSimpleListView = false;

    private async Task OnChangeSimpleListView()
    {
        Superkatten.Clear();
        _showSimpleListView = !_showSimpleListView;

        await _localStorageService.SetItem<bool>(
            LocalStorageItems.LOCALSTORAGE_SETTING_SUPERKATTENLIST_TYPE,
            _showSimpleListView);

        await UpdateListAsync();
    }

    protected override async Task OnInitializedAsync()
    {
        LoadingInfoMessage = "Inlezen van alle superkatten";

        var enableSimpleListView = await _localStorageService.GetItem<bool>(LocalStorageItems.LOCALSTORAGE_SETTING_SUPERKATTENLIST_TYPE);
        _showSimpleListView = enableSimpleListView;

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
        _navigation.NavigateBack();
    }

    private Task Print(string printername)
    {
        return _superkatActionService.CreateSuperkatCageCardAsync(new());
    }
}
