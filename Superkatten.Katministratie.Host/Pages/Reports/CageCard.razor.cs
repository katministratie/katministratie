using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services.Authentication;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages.Reports;

partial class CageCard
{
    [Inject] public IAuthenticationService? AuthenticationService { get; init; }
    [Inject] public IReportingService? ReportingService { get; init; }
    [Inject] public Navigation? Navigation { get; init; }


    private readonly List<CatArea> _areaSelectionList = Enum.GetValues(typeof(CatArea)).Cast<CatArea>().ToList();
    private List<int> _cageNumberSelectionList = Array.Empty<int>().ToList();
    private CatArea _catArea { get; set; } = CatArea.Quarantine;
    private int _cageNumber { get; set; } = 1;

    public CageCard()
    {
        UpdateCatAreaHokNumbers();
    }

    private void OnCatAreaChanged(CatArea catArea)
    {
        _catArea = catArea;

        UpdateCatAreaHokNumbers();
        StateHasChanged();
    }

    private void UpdateCatAreaHokNumbers()
    {
        // TODO: haal hoknummers uit applicatielaag, voor nu hardcoded
        _cageNumberSelectionList = _catArea switch
        {
            CatArea.Quarantine => new List<int>() { 1, 2, 3, 4, 5, 6, 7, 8, 9, 10 },
            CatArea.Room2 => new List<int>() { 11, 12, 13, 14, 15, 16, 17, 18, 19, 20 },
            CatArea.Infirmary => new List<int>() { 1, 2, 3, 4, 5, 6, 7},
            CatArea.SmallEnclosure => new List<int>() { 1, 2, 3, 4 },
            CatArea.BigEnclosure => new List<int>() { 1 },
            _ => new List<int>()
        };
    }

    private void OnCageNumberChanged(int cageNumber)
    {
        _cageNumber = cageNumber;
    }

    private async Task OnOk()
    {
        if (AuthenticationService is null 
            || AuthenticationService.User is null 
            || ReportingService is null
            || Navigation is null)
        {
            return;
        }

        var email = AuthenticationService.User.Email;
        if (email is null || string.IsNullOrEmpty(email))
        {
            return;
        }

        var parameters = new RequestCageCardEmailParameters
        {
            Email = email,
            CageNumber = _cageNumber,
            CatArea = _catArea        
        };

        await ReportingService.EmailCageCardAsync(parameters);

        Navigation.NavigateBack();
    }

    public void OnCancel()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateBack();
    }
}

