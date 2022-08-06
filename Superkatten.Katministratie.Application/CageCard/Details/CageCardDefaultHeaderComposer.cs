using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.CageCard.Details;

public class CageCardDefaultHeaderComposer : IComponent
{
    private IReadOnlyCollection<Superkat> _superkatten { get; init; }

    public CageCardDefaultHeaderComposer(IReadOnlyCollection<Superkat> superkatten)
    {
        _superkatten = superkatten;
    }

    public void Compose(IContainer container)
    {
        var titleStyle = TextStyle
            .Default
            .FontSize(30)
            .SemiBold()
            .FontColor(Colors.Blue.Medium);

        container.Column(column =>
        {
            column.Item()
                .Text($"Locatie: {_superkatten.First().CatArea} Hok: {_superkatten.First().CageNumber}")
                .Style(titleStyle)
                .FontSize(40);

            column.Item()
                .Row(row =>
                {
                    row.RelativeItem()
                    .Padding(5)
                    .Border(1)
                    .Column(column =>
                    {
                        column.Item()
                            .AlignMiddle()
                            .AlignCenter()
                            .Text($"Aantal:\n{_superkatten.Count}")
                            .Style(titleStyle);
                    });

                    row.RelativeItem()
                        .Padding(5)
                        .Border(1)
                        .Column(column =>
                        {
                            column.Item()
                                .AlignMiddle()
                                .AlignCenter()
                                .Text($"{_superkatten.First().CatchDate.ToString("dd.MMMM yyyy")}")
                                .Style(titleStyle);
                            column.Item()
                                .AlignMiddle()
                                .AlignCenter()
                                .Text($"{_superkatten.First().CatchLocation}")
                                .Style(titleStyle);
                        });
                });
        });
    }
}
