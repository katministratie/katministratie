
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Helpers;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents.Action;

public partial class SuperkatActions: ComponentBase
{
    [Inject]
    public Navigation Navigation { get; set; }

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

    private void AddMedicalProcedure()
    {
        var navigateToUrl = "AddMedicalProcedure/" + SuperkatView.Id.ToString();
        Navigation.NavigateTo(navigateToUrl);
    }
}
