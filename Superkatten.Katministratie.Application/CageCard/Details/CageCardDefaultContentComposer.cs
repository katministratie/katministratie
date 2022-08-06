using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.CageCard.Details;

public class CageCardDefaultContentComposer : IComponent
{
    private IReadOnlyCollection<Superkat> _superkatten { get; init; }

    public CageCardDefaultContentComposer(IReadOnlyCollection<Superkat> superkatten)
    {
        _superkatten = superkatten;
    }

    public void Compose(IContainer container)
    {
        container.PaddingVertical(40).Column(column =>
        {
            column.Spacing(5);

            column.Item().Element(ComposeTable);
        });
    }

    private void ComposeTable(IContainer container)
    {
        container
            .BorderBottom(1)
            .BorderColor(Colors.Blue.Lighten1)
            .Padding(1)
            .Grid(grid =>
            {
                grid.Spacing(5);
                grid.AlignCenter();
                grid.Columns(3);

                foreach (var superkat in _superkatten)
                {
                    var superkatElement = new CageCardSuperkatDetailsComponent(superkat);
                    grid.Item().Element(superkatElement.Compose);
                }
            });
    }
}
