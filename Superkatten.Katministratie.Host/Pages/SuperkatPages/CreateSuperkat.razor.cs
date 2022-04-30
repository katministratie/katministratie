using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Api;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkat
{ 
    private string VangdatumText { get; init; } = "Vangdatum";
    private string GeboortedatumText { get; init; } = "Geboortedatum";
    private DateTime InitialDateTimeNow => DateTime.UtcNow;

    private bool ValidCatchLocation => string.IsNullOrWhiteSpace(parameters.CatchLocation);

    private void OnChangeCatchDate(DateTimeChangedEventArgs args)
    {
        parameters.CatchDate = args.Date;
    }

    private void OnChangeBirthDate(DateTimeChangedEventArgs args)
    {
        parameters.CatchDate = args.Date;
    }

    private CreateSuperkatParameters parameters =
        new CreateSuperkatParameters()
            {
                CatchDate = DateTime.UtcNow,
                CatchLocation = string.Empty,
                Area = CatArea.Unknown,
                CageNumber = null,
                Behaviour = CatBehaviour.Unknown,
                Retour = false,
                IsKitten = true,
                Gender = Gender.Unknown
            };

    public async Task OnOk()
    {
        await StoreSuperkat();
        NavigationManager.NavigateTo("/");
    }

    public async Task OnOkAndNew()
    {
        await StoreSuperkat();
        await _message.Warning("Voer de volgende superkat in", 2);
    }

    public void OnCancel()
    {
        NavigationManager.NavigateTo("/");
    }

    private async Task StoreSuperkat()
    {
        var superkat = await _superkattenService.CreateSuperkatAsync(parameters);
        if (superkat is null)
        {
            await _message.Error($"Fout bij het opslaan van de nieuwe superkat.", 3);
            return;
        }

        await _message.Success($"Superkat is opgeslagen onder nummer {superkat.DisplayableNumber}", 3);        
    }
}
