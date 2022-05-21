using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class CreateGastgezin
{
    private Gastgezin parameters = new();
                    
    private bool ValidName => string.IsNullOrWhiteSpace(parameters.Name);

    public async Task OnOk()
    {
        await StoreGastgezin();
        _navigationManager.NavigateTo("OverviewGastgezinnen");
    }

    public void OnCancel()
    {
        _navigationManager.NavigateTo("OverviewGastgezinnen");
    }

    private async Task StoreGastgezin()
    {
        var createGastgezin = new CreateOrUpdateNawGastgezinParameters
        {
            Name = parameters.Name,
            Address = parameters.Address,
            City = parameters.City,
            Phone = parameters.Phone
        };

        var gastgezin = await _gastgezinService.CreateGastgezinAsync(createGastgezin);
        if (gastgezin is null)
        {
            await _message.Error($"Fout bij het opslaan van een nieuw gastgezin.", 3);
            return;
        }

        await _message.Success($"Gastgezin {gastgezin.Name} is opgeslagen.", 3);        
    }
}
