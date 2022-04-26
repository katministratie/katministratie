using Microsoft.AspNetCore.Components;

namespace Superkatten.Katministratie.Host.Components;

public partial class IsLoadingMessage
{
    [Parameter]
    public string InfoTitle { get; set; } = string.Empty;

    [Parameter]
    public string InfoMessage { get; set; } = string.Empty;
}