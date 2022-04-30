
using Microsoft.AspNetCore.Components;

namespace Superkatten.Katministratie.Host.Components;

public partial class SuperkatAction
{ 
    [Parameter]
    public string ActionIcon { get; set; } = string.Empty;

    [Parameter]
    public EventCallback OnClickCallback { get; set; }

    public async void ExecuteAction() 
    {
        await OnClickCallback.InvokeAsync();
    }
}
