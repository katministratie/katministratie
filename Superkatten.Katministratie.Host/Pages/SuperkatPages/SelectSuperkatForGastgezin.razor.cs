using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class SelectSuperkatForGastgezin
{
    public Gastgezin? Gastgezin { get; set; } = new Gastgezin { Name = "John Doee" };

    private void OnBackHome()
    {
        _navigationManager.NavigateTo("");
    }

    private void OnOk()
    {
        // Store the gastgezin item
        _navigationManager.NavigateTo("");
    }
    
    private void OnCancel()
    {
        _navigationManager.NavigateTo("");
    }
}