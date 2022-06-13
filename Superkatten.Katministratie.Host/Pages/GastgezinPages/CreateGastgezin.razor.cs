using Superkatten.Katministratie.Contract;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class CreateGastgezin
{
    public Gastgezin? Gastgezin { get; set; }
                    
    private bool ValidName => string.IsNullOrWhiteSpace(Gastgezin?.Name);

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
        if (Gastgezin is null)
        {
            throw new Exception("No gastgezin to store");
        }

        var createGastgezin = new CreateOrUpdateNawGastgezinParameters
        {
            Name = Gastgezin.Name,
            Address = Gastgezin.Address,
            City = Gastgezin.City,
            Phone = Gastgezin.Phone
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
