using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using System;
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
            .FontSize(20)
            .SemiBold()
            .FontColor(Colors.Blue.Medium);

        container.Column(column =>
        {
            column.Item()
                .Text(GetCageNumberHeaderText(_superkatten))
                .Style(titleStyle)
                .FontSize(20);

            column.Item()
                .Text(GetFirstCatchDate(_superkatten))
                .Style(titleStyle)
                .FontSize(20);

            column.Item()
                .Text(GetCatchLocation(_superkatten))
                .Style(titleStyle)
                .FontSize(20);
        });
    }

    private string GetCatchLocation(IReadOnlyCollection<Superkat> superkatten)
    {
        var catchLocations = superkatten
            .OrderBy(s => s.CatchDate)
            .ToList()
            .Select(s => s.CatchLocation)
            .ToList();

        return catchLocations?.ToString() ?? string.Empty;
    }

    private static string GetFirstCatchDate(IReadOnlyCollection<Superkat> superkatten)
    {
        var firstCatchDate = superkatten
            .OrderBy(s => s.CatchDate)
            .ToList()
            .Select(s => s.CatchDate)
            .FirstOrDefault();

        return firstCatchDate.ToShortDateString();
    }

    private static string GetCageNumberHeaderText(IReadOnlyCollection<Superkat> superkatten)
    {
        var catArea = superkatten
            .Select(s => s.CatArea)
            .Distinct()
            .ToList()
            .First();

        var cageNumber = superkatten
            .Select(s => s.CageNumber)
            .Distinct()
            .ToList()
            .First();

        return catArea.ToString() + "-" + cageNumber.ToString();
    }
}
