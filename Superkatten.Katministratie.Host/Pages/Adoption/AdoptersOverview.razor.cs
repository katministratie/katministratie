using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Pages.Adoption;

public partial class AdoptersOverview
{
    private IReadOnlyCollection<Adoptant> _adopters;

    protected override Task OnInitializedAsync()
    {
        // Lees de adoptanten waarvan de adoptie nog niet is afgerond.

        return Task.CompletedTask;
    }
}