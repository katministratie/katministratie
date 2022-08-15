
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SuperkatComponent : ComponentBase
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

            _superkatView = new SuperkatView(value);
        }
    }

    private SuperkatView? _superkatView;

    private async Task ReloadSuperkatData()
    {
        if (_superkatView is null)
        {
            return;
        }

        var superkat = await _superkattenService.GetSuperkatAsync(_superkatView.Id);
        _superkatView = new SuperkatView(superkat);
    }
}
