using Superkatten.Katministratie.Application.Interfaces;
using Superkatten.Katministratie.Application.Printing;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.CageCard;
public class SuperkatCageCard : ISuperkatCageCard
{
    private const string FILENAME = "cagecard.pdf";

    private readonly ISuperkattenService _superkatService;
    private readonly ICageCardComposer _cageCardComposer;

    public SuperkatCageCard(ISuperkattenService superkatService, ICageCardComposer cageCardComposer)
    {
        _superkatService = superkatService;
        _cageCardComposer = cageCardComposer;
    }

    public async Task<string> CreateCageCardAsync(Guid id)
    {
        var superkat = await _superkatService.ReadSuperkatAsync(id);
        return _cageCardComposer.Compose(superkat);
    }
}