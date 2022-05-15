using Superkatten.Katministratie.Contract;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class CreateGastgezin
{
    private CreateOrUpdateGastgezinParameters parameters =
                    new CreateOrUpdateGastgezinParameters()
                    {
                        Name = string.Empty,
                        Address = string.Empty,
                        City = string.Empty,
                        Phone = string.Empty
                    };
                    
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
        var gastgezin = await _gastgezinService.CreateGastgezinAsync(parameters);
        if (gastgezin is null)
        {
            await _message.Error($"Fout bij het opslaan van een nieuw gastgezin.", 3);
            return;
        }

        await _message.Success($"Gastgezin {gastgezin.Name} is opgeslagen.", 3);        
    }
}
