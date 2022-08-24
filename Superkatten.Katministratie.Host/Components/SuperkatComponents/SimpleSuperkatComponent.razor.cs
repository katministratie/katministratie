
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SimpleSuperkatComponent : ComponentBase
{
    [Inject]
    private ISuperkattenListService _superkattenService { get; set; } = null!;

    [Parameter]
    public Superkat? Superkat
    {
        set
        {
            if (value is null)
            {
                return;
            }

            SuperkatView = new SuperkatView(value);
        }
    }

    private SuperkatView SuperkatView { get; set; } = null!;

    private async Task ReloadSuperkatData()
    {
        if (SuperkatView is null)
        {
            return;
        }

        var superkat = await _superkattenService.GetSuperkatAsync(SuperkatView.Superkat.Id);
        SuperkatView = new SuperkatView(superkat);
    }

    private string CageNumberDescription()
    {
        var catAreaCode = ConvertCatAreaToShowString(SuperkatView.Superkat.CatArea);

        return string.IsNullOrEmpty(catAreaCode)
            ? $"{SuperkatView.Superkat.CageNumber}"
            : $"{catAreaCode}-{SuperkatView.Superkat.CageNumber}";
    }

    private static string ConvertCatAreaToShowString(CatArea catArea)
    {
        return catArea switch
        {
            CatArea.Quarantine => "Q",
            CatArea.Infirmary => "ZB",
            CatArea.SmallEnclosure => string.Empty,
            CatArea.BigEnclosure => string.Empty,
            CatArea.Room2 => string.Empty,
            _ => string.Empty
        };
    }
}
