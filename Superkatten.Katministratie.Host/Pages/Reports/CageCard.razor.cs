using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services.Authentication;
using Superkatten.Katministratie.Host.Services.Interfaces;


namespace Superkatten.Katministratie.Host.Pages.Reports;

partial class CageCard
{
    [Inject] public IAuthenticationService AuthenticationService { get; set; }
    [Inject] public IReportingService ReportingService{ get; set; }
    [Inject] public Navigation Navigation { get; set; }

    private int CageNumber = 1;

    private CatArea SelectedItem { get; set; }

    public class MySelectModel
    {
        public int MyValueField { get; set; }
        public string MyTextField { get; set; } = string.Empty;
    }

    private static readonly string[] _selectionItems = Enum.GetNames<CatArea>();

    private IEnumerable<MySelectModel> myDdlData = Enumerable
        .Range(1, _selectionItems.Length)
        .Select(x => new MySelectModel
        {
            MyTextField = _selectionItems[x - 1],
            MyValueField = x
        });

    private int SelectedListValue
    {
        get
        {
            return (int)SelectedItem + 1;
        }
        set
        {
            if (value < 0)
            {
                SelectedItem = CatArea.Quarantine;
            }

            SelectedItem = (CatArea)(value - 1);
        }
    }

    private void MyListValueChangedHandler(int newValue)
    {
        SelectedListValue = newValue;
        StateHasChanged();
    }

    private async Task OnOk()
    {
        var email = AuthenticationService.User?.Email;
        if (email is null || string.IsNullOrEmpty(email))
        {
            return;
        }

        var parameters = new RequestCageCardEmailParameters
        {
            Email = email,
            CageNumber = CageNumber,
            CatArea = SelectedItem        
        };

        await ReportingService.EmailCageCardAsync(parameters);

        Navigation.NavigateBack();
    }

    public void OnCancel()
    {
        Navigation.NavigateBack();
    }

}

