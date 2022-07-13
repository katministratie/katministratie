using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Pages.GastgezinPages;

public partial class CreateGastgezin
{

    [Inject] 
    public NavigationManager? NavigationManager { get; set; }

    [Inject]
    public IGastgezinService? GastgezinService { get; set; }

    [Inject]
    public AntDesign.MessageService? Message { get; set; }


    public HostFamilyNawData GastgezinData { get; set; } = new();
                    
    private bool ValidName => string.IsNullOrWhiteSpace(GastgezinData?.Name);

    public async Task OnOk()
    {
        if(NavigationManager is null)
        {
            return;
        }

        await StoreGastgezin();
        NavigationManager.NavigateTo("OverviewGastgezinnen");
    }

    public void OnCancel()
    {
        if (NavigationManager is null)
        {
            return;
        }

        NavigationManager.NavigateTo("OverviewGastgezinnen");
    }

    private async Task StoreGastgezin()
    {
        if (Message is null)
        {
            return;
        }

        if (GastgezinService is null)
        {
            return;
        }

        var createGastgezin = new CreateUpdateGastgezinParameters
        {
            Name = GastgezinData.Name,
            Address = GastgezinData.Address,
            City = GastgezinData.City,
            Phone = GastgezinData.Phone
        };

        var gastgezin = await GastgezinService.CreateGastgezinAsync(createGastgezin);
        if (gastgezin is null)
        {
            await Message.Error($"Fout bij het opslaan van een nieuw gastgezin.", 1);
            return;
        }

        await Message.Success($"Gastgezin {gastgezin.Name} is opgeslagen.", 1);        
    }
}
