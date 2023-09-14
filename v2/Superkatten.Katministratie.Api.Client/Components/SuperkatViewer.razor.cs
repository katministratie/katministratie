using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Api.Client.Entities;

namespace Superkatten.Katministratie.Api.Client.Components;

partial class SuperkatViewer
{
    [Parameter]public SuperkatView Superkat { get; set; } = null!;


}