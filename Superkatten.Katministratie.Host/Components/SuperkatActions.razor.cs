
using Microsoft.AspNetCore.Components;
using Superkatten.Katministratie.Host.Entities;
using Superkatten.Katministratie.Host.Services;

namespace Superkatten.Katministratie.Host.Components;

public partial class SuperkatActions
{ 
    [Parameter]
    public Superkat Superkat { get; set; } = new();

    [Parameter]
    public EventCallback<Superkat> SuperkatChanged { get; set; }

    private async Task ToggleReserve()
    {
        await _superkatActionService.ToggleReserveSuperkatAsync(Superkat.Id);
        await UpdateSuperkat(!Superkat.Reserved, Superkat.Retour);
    }

    private async Task ToggleRetour()
    {
        await _superkatActionService.ToggleRetourSuperkatAsync(Superkat.Id);
        await UpdateSuperkat(Superkat.Reserved, !Superkat.Retour);
    }

    private async Task UpdateSuperkat(bool reserve, bool retour)
    {
        await SuperkatChanged.InvokeAsync(Superkat = new Superkat
        {
            Id = Superkat.Id,
            Name = Superkat.Name,
            Birthday = Superkat.Birthday,
            CatchLocation = Superkat.CatchLocation,
            CatchDate = Superkat.CatchDate,
            Number = Superkat.Number,
            Reserved = reserve,
            Retour = retour,
            CatArea = Superkat.CatArea,
            CageNumber = Superkat.CageNumber
        });
    }   

    public Task PrintSuperkatCageCard()
    {
        _printingService.PrintCageCard(Superkat.Id);
        return Task.CompletedTask;
    }
}
