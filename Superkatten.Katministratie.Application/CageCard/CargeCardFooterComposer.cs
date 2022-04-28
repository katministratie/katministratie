using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.CageCard
{
    public class CargeCardFooterComposer : ICageCardPartComposer
    {
        private Superkat _superkat { get; init; }

        public CargeCardFooterComposer(Superkat superkat)
        {
            _superkat = superkat;
        }

        public void Compose(IContainer container)
        {
            container
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Superkatten katkaart ");
            });
        }
    }
}
