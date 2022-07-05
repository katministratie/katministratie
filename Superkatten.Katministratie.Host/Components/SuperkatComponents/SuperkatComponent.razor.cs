
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SuperkatComponent
{
    [Inject]
    private ISuperkattenListService? _superkattenService { get; set; }

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

    private SuperkatView SuperkatView { get; set; }

    private async Task ReloadSuperkatData()
    {
        if (SuperkatView is null)
        {
            return;
        }

        if (_superkattenService is null)
        {
            return;
        }

        var superkat = await _superkattenService.GetSuperkatAsync(SuperkatView.Id);
        SuperkatView = new SuperkatView(superkat);
    }
}
