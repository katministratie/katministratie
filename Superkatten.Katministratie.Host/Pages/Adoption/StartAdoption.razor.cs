using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Pages.Adoption;

public partial class StartAdoption
{
    [Parameter] public Guid AdopterId { get; set; }

    private IReadOnlyCollection<Superkat> _superkatten = new List<Superkat>();
    private string _adopterName = string.Empty;
    private string _adopterAddress = string.Empty;
    private string _adopterPostcode = string.Empty;
    private string _adopterCity = string.Empty;
    private string _adopterPhone = string.Empty;

    protected override Task OnInitializedAsync()
    {
        // Read adopter data already given
        // and show this on screen

        return Task.CompletedTask;
    }

}
