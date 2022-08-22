using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.CageCard.Details.SuperkatCard;

internal class SuperktCardBodyComponent : IComponent
{
    private readonly Superkat _superkat;

    public SuperktCardBodyComponent(Superkat superkat)
    {
        _superkat = superkat;
    }

    public void Compose(IContainer container)
    {
        throw new System.NotImplementedException();
    }
}
