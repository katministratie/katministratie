using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Components.SuperkatComponents;

public partial class SuperkatGenderComponent
{
    [Parameter]
    public Gender GenderView { get; set; }
}
