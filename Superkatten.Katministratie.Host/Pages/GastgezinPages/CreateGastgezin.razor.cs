using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class CreateGastgezin
{
    public NawData GastgezinData { get; set; } = new();
                    
    private bool ValidName => string.IsNullOrWhiteSpace(GastgezinData?.Name);

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
        var createGastgezin = new CreateUpdateGastgezinParameters
        {
            Name = GastgezinData.Name,
            Address = GastgezinData.Address,
            City = GastgezinData.City,
            Phone = GastgezinData.Phone
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
