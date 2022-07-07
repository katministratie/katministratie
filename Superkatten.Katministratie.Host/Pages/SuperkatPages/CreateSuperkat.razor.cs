﻿using AntDesign;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkat
{
    public const int MAX_HOKNUMBER_ALLOWED = 50;

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


    [Inject]
    public Navigation Navigation { get; set; }
    private bool ValidCatchLocation => string.IsNullOrWhiteSpace(CatchLocation);

    private void OnChangeCatchDate(DateTimeChangedEventArgs args)
    {
        CatchDate = args.Date;
    }

    public async Task OnOk()
    {
        await StoreSuperkat();
        Navigation.NavigateBack();
    }

    public async Task OnOkNoReturn()
    {
        await StoreSuperkat();
    }

    public void OnCancel()
    {
        Navigation.NavigateBack();
    }

    private async Task StoreSuperkat()
    {
        var createSuperkatParameters = new CreateSuperkatParameters()
        {
            CatchDate = CatchDate,
            CatchLocation = CatchLocation,
            CatArea = CatArea,
            CageNumber = CageNumber,
            Behaviour = Behaviour,
            Retour = Retour,
            IsKitten = IsKitten,
            Gender = Gender,
            LitterType = LitterType,
            WetFoodAllowed = WetFoodAllowed,
            FoodType = FoodType,
            CatColor = CatColor,
            EstimatedWeeksOld = EstimatedWeeksOld
        };
        var superkat = await _superkattenService.CreateSuperkatAsync(createSuperkatParameters);
        if (superkat is null)
        {
            await _message.Error($"Fout bij het opslaan van de nieuwe superkat.", 3);
            return;
        }

        _message.Success($"Superkat {superkat.CatchDate.Year % 100}-{superkat.Number.ToString("000")} is aangemaakt", 3);        
    }
}
