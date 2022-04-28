using QuestPDF.Drawing;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.CageCard;
public interface ICageCardComposer
{
    DocumentMetadata GetMetadata();
    string Compose(Superkat superkat);
}
