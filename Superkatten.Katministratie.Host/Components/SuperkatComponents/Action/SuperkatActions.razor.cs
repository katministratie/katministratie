
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents.Action;

public partial class SuperkatActions
{
    [Parameter]
    public SuperkatView SuperkatView { get; set; }

    [Parameter]
    public EventCallback OnActionExecuted { get; set; }

    private async Task ToggleReserve()
    {
        await _superkatActionService.ToggleReserveSuperkatAsync(SuperkatView.Id);
        await OnActionExecuted.InvokeAsync();
    }

    private async Task ToggleRetour()
    {
        await _superkatActionService.ToggleRetourSuperkatAsync(SuperkatView.Id);
        await OnActionExecuted.InvokeAsync();
    }
}
