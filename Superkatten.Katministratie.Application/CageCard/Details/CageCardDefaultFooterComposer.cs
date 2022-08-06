using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.CageCard.Details
{
    public class CageCardDefaultFooterComposer : IComponent
    {
        private IReadOnlyCollection<Superkat> _superkatten { get; init; }

        public CageCardDefaultFooterComposer(IReadOnlyCollection<Superkat> superkatten)
        {
            _superkatten = superkatten;
        }

        public void Compose(IContainer container)
        {
            container
                .PaddingTop(10)
                .Column(column =>
                {
                    column.Spacing(5);
                    column.Item()
                        .BorderBottom(1)
                        .PaddingBottom(5)
                        .Text($"Kooikaart superkatten (c) {DateTime.UtcNow.Year}")
                        .SemiBold();
                });
        }

    }
}
