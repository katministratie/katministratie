using QuestPDF.Drawing;
using Superkatten.Katministratie.Application.Entities;

namespace Superkatten.Katministratie.Application.CageCard;
public interface ICageCardComposer
{
    DocumentMetadata GetMetadata();
    string Compose(Superkat superkat);
}
