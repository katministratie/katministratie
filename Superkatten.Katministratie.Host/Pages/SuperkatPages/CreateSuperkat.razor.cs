using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkat
{
    public DateTime CatchDate = DateTime.UtcNow;
    public string CatchLocation = string.Empty;
    public CatArea CatArea = CatArea.Quarantine;
    public int CageNumber = 1;
    public CatBehaviour Behaviour = CatBehaviour.Unknown;
    public bool Retour = false;
    public bool IsKitten = true;
    public Gender Gender = Gender.Unknown;
    public LitterGranuleType LitterType = LitterGranuleType.Normal;
    public bool WetFoodAllowed = true;
    public FoodType FoodType = FoodType.FirstPhase;
    public string CatColor = string.Empty;
    public bool StrongHoldGiven = false;
    public int EstimatedWeeksOld = 0;

    private bool ValidCatchLocation => string.IsNullOrWhiteSpace(CatchLocation);

    private void OnChangeCatchDate(DateTimeChangedEventArgs args)
    {
        CatchDate = args.Date;
    }

    private void OnChangeBirthDate(DateTimeChangedEventArgs args)
    {
        CatchDate = args.Date;
    }

    public async Task OnOk()
    {
        await StoreSuperkat();
        NavigationManager.NavigateTo("/");
    }

    public void OnCancel()
    {
        NavigationManager.NavigateTo("/");
    }

    private async Task StoreSuperkat()
    {
        var createSuperkatParameters = new CreateSuperkatParameters()
        {
            CatchDate = DateTime.UtcNow,
            CatchLocation = string.Empty,
            CatArea = CatArea.Quarantine,
            CageNumber = null,
            Behaviour = CatBehaviour.Unknown,
            Retour = false,
            IsKitten = true,
            Gender = Gender.Unknown,
            LitterType = LitterGranuleType.Normal,
            WetFoodAllowed = true,
            FoodType = FoodType.FirstPhase,
            Color = string.Empty,
            EstimatedWeeksOld = EstimatedWeeksOld
        };
        var superkat = await _superkattenService.CreateSuperkatAsync(createSuperkatParameters);
        if (superkat is null)
        {
            await _message.Error($"Fout bij het opslaan van de nieuwe superkat.", 3);
            return;
        }

        await _message.Success($"Superkat is opgeslagen met jaar: {superkat.CatchDate.Year % 100} en nummer {superkat.Number}", 3);        
    }
}
