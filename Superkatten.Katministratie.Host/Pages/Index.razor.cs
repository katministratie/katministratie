using Blazorise;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Components.Web;
using Superkatten.Katministratie.Contract.ApiInterface.Reporting;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Pages.Users;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Authentication;
using Superkatten.Katministratie.Host.Services.Interfaces;

namespace Superkatten.Katministratie.Host.Pages;

public partial class Index
{ 
    [Inject] public Navigation Navigation { get; set; } = null!;

    [Inject] public ISuperkattenListService SuperkattenService { get; set; } = null!;

    [Inject] public IAuthenticationService AuthenticationService { get; set; } = null!;

    [Inject] public IReportingService ReportingService { get; set; } = null!;
    
    
    private LoginModel _loginModel = null!;
    private Modal _authenticationDialog = null!;

    private bool IsAuthenticated => AuthenticationService.IsAuthenticated;

    protected override async Task OnInitializedAsync()
    {
        if (AuthenticationService is null)
        {
            return;
        }

        await base.OnInitializedAsync();
        await AuthenticationService.InitializeAsync();
    }

    private void OnRegister()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("Register");
    }

    private async Task OnLogin()
    {
        if (_authenticationDialog is null)
        {
            return;
        }

        _loginModel = new();
        await _authenticationDialog.Show();
    }

    private async Task OnLogout()
    {
        if (AuthenticationService is null)
        {
            return;
        }

        await AuthenticationService.LogoutAsync();
    }

    private void OnCreateSuperkat()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("CreateSuperkat");
    }

    private void OnShowOverviewSuperkatten()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("OverviewSuperkatten");
    }

    private void OnShowOverviewGastgezinnen()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("OverviewGastgezinnen");
    }

    private void OnShowMedicalProcedures()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("OverviewMedicalProcedures");
    }

    private Task HideModal()
    {
        if (_authenticationDialog is null)
        {
            return Task.CompletedTask;
        }

        return _authenticationDialog.Hide();
    }

    private async Task SaveAndHideModal()
    {
        if (_authenticationDialog is null)
        {
            return;
        }

        if (_loginModel is not null)
        {
            await AuthenticationService.AuthenticateUserAsync(_loginModel.Username, _loginModel.Password);
        }

        await _authenticationDialog.Hide();
    }

    private async Task OnKeyPress(KeyboardEventArgs eventArgs)
    {
        if(eventArgs.Key != "Enter" || _loginModel is not null && !_loginModel.AllFilledIn)
        {
            return;
        }

        await SaveAndHideModal();        
    }

    private void OnEmailCageCard()
    {
        if (Navigation is null)
        {
            return;
        }

        Navigation.NavigateTo("CageCard");
    }

    private async Task OnCreateWakkerDierInventoryReport()
    {
        if (ReportingService is null)
        {
            return;
        }

        if (AuthenticationService is null)
        {
            return;
        }

        if (AuthenticationService.User is null)
        {
            return;
        }

        var email = AuthenticationService.User.Email;
        if (string.IsNullOrEmpty(email))
        {
 //           _notificationString = "Email van ingelogde gebruiker is niet ingevuld. De email kan niet worden verstuurd.";
 //           await _notification.Show();
        }

        var requestParameters = new RequestCatchLocationEmailParameters
        {
            Email = email,
            From = DateTime.UtcNow.AddMonths(-3),
            To = DateTime.UtcNow
        };

        await ReportingService.EmailInventoryDetailsReportAsync(requestParameters);
    }
}