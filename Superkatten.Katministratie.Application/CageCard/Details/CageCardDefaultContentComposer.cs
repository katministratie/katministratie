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
        var columns = _superkatten.Count < 4
            ? _superkatten.Count
            : _superkatten.Count % 4;

        container
            .PaddingBottom(5)
            .Grid(grid =>
            {
                grid.Spacing(5);
                grid.AlignCenter();
                grid.Columns(columns);

                foreach (var superkat in _superkatten)
                {
                    var superkatElement = new CageCardSuperkatDetailsComponent(superkat);
                    grid.Item().Element(superkatElement.Compose);
                }

                var filling = _superkatten.Count % columns;
                for (var fillingIndex = 0; fillingIndex < filling; fillingIndex++)
                {
                    grid.Item().Border(1);
                }
            });
    }
}
