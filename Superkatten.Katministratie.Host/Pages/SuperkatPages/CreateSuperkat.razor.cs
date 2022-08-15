using Blazorise;
using Blazorise.Snackbar;
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Helpers;
using Superkatten.Katministratie.Host.Services;
using Superkatten.Katministratie.Host.Services.Interfaces;
using System.Linq;

namespace Superkatten.Katministratie.Host.Pages.SuperkatPages;

public partial class CreateSuperkat
{
    public const int MAX_HOKNUMBER_ALLOWED = 50;

    public DateTime? CatchDate = DateTime.UtcNow;
    private Guid _catchLocationNameId = Guid.NewGuid();
    public string CatchLocationName = string.Empty;
    public LocationType CatchLocationType = LocationType.Farm;
    public CatArea CatArea = CatArea.Quarantine;
    public int CageNumber = 1;
    public CatBehaviour Behaviour = CatBehaviour.Unknown;
    public bool Retour = false;
    public AgeCategory AgeCategory = AgeCategory.Kitten;
    public Gender Gender = Gender.Unknown;
    public LitterGranuleType LitterType = LitterGranuleType.Normal;
    public bool WetFoodAllowed = true;
    public FoodType FoodType = FoodType.FirstPhase;
    public string CatColor = string.Empty;
    public bool StrongHoldGiven = false;
    public int EstimatedWeeksOld = 0;


    [Inject] IPageProgressService PageProgressService { get; set; } = null!;

    [Inject] public Navigation? Navigation { get; set; }

    [Inject] public ISuperkattenListService? SuperkattenService { get; set; }

    [Inject] public ILocationService? LocationService { get; set; }

    private const int NOTIFICATION_SHOW_TIME = 2500;

    private SnackbarStack _snackbarStack = null!;

    public IEnumerable<Location> Locations = new List<Location>();
    
    private bool CanEnterHokNumber => CatArea != CatArea.SmallEnclosure && CatArea != CatArea.BigEnclosure;

    protected override async Task OnInitializedAsync()
    {
        if (LocationService is null)
        {
            return;
        }

        Locations = await LocationService.GetLocationsAsync();

        await base.OnInitializedAsync();
    }


    public async Task OnAddSuperkat()
    {
        await StoreSuperkatAsync();
    }

    public void OnStopAndReturn()
    {
        Navigation?.NavigateBack();
    }

    private async Task<bool> StoreSuperkatAsync()
    {
        await PageProgressService.Go(null, options => { options.Color = Color.Info; });

        if (SuperkattenService is null)
        {
            return false;
        }

        if (string.IsNullOrEmpty(CatchLocationName))
        {
            await _snackbarStack.PushAsync("Vangplaats is leeg",
            SnackbarColor.Info,
            options =>
            {
                options.IntervalBeforeClose = NOTIFICATION_SHOW_TIME;
            });

            await PageProgressService.Go(-1);

            return false;
        }

        var catchLocation = new Location
        {
            Name = CatchLocationName,
            Type = CatchLocationType
        };

        var createSuperkatParameters = new CreateSuperkatParameters()
        {
            CatchDate = CatchDate ?? DateTime.UtcNow,
            CatchLocation = catchLocation,
            CatArea = CatArea,
            CageNumber = CageNumber,
            Behaviour = Behaviour,
            Retour = Retour,
            AgeCategory = AgeCategory,
            Gender = Gender,
            LitterType = LitterType,
            WetFoodAllowed = WetFoodAllowed,
            FoodType = FoodType,
            CatColor = CatColor,
            EstimatedWeeksOld = EstimatedWeeksOld,
            StrongholdGiven = StrongHoldGiven
        };

        var superkat = await SuperkattenService.CreateSuperkatAsync(createSuperkatParameters);

        await PageProgressService.Go(-1);

        if (superkat is null)
        {
            await _snackbarStack.PushAsync($"Fout bij het opslaan van de gegevens",
            SnackbarColor.Danger,
            options =>
            {
                options.IntervalBeforeClose = NOTIFICATION_SHOW_TIME;
            });
            return false;
        }

        await _snackbarStack.PushAsync($"Superkat toegevoegd met nummer: {superkat.Number}",
            SnackbarColor.Info,
            options =>
            {
                options.IntervalBeforeClose = NOTIFICATION_SHOW_TIME;
            });

        return true;
    }
}
