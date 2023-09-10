﻿using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Api.Client.Entities;
using Superkatten.Katministratie.Api.Client.Services;

namespace Superkatten.Katministratie.Api.Client.Pages;

public partial class Index
{
    [Inject] public ISuperkattenService SuperkattenService { get; set; } = null!;

    private IReadOnlyList<SuperkatView> Superkatten { get; set; } = new List<SuperkatView>();

    private async Task RefreshPage()
    {
        Superkatten = await SuperkattenService.GetSuperkattenAsync();
        
        StateHasChanged();
    }
}
