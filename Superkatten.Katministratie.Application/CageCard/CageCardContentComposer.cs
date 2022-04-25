using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Application.Entities;
using System;

namespace Superkatten.Katministratie.Application.CageCard
{
    public class CageCardContentComposer : ICageCardPartComposer
    {
        private Superkat _superkat { get; init; }

        public CageCardContentComposer(Superkat superkat)
        {
            _superkat = superkat;
        }

        public void Compose(IContainer container)
        {
            container.PaddingVertical(40)
            .Column(column =>
            {
                column.Spacing(5);

                column.Item().Text(text =>
                {
                    text.Span("Geboren op: ").SemiBold();
                    text.Span($"{_superkat.Birthday:d}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Gedrag: ").SemiBold();
                    text.Span($"{_superkat.Behaviour}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Vanglocatie: ").SemiBold();
                    text.Span($"{_superkat.CatchLocation}");
                });

                column.Item().Text(text =>
                {
                    text.Span("Retour: ").SemiBold();
                    text.Span(_superkat.Retour ? "Gaat retour" : "Gaat het normale adoptieproces in");
                });

            });
        }
    }
}
