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
            .FontSize(18)
            .SemiBold();

        container
            .Border(1)
            .PaddingBottom(5)
            .Row(row =>
            {
                row.RelativeItem()
                    .Text(GetCageNumberHeaderText(_superkatten))
                    .Style(titleStyle)
                    .FontSize(20);

                row.RelativeItem()
                    .Text(GetFirstCatchDate(_superkatten))
                    .Style(titleStyle)
                    .FontSize(20);

                row.RelativeItem()
                    .Text(GetCatchLocation(_superkatten))
                    .Style(titleStyle)
                    .FontSize(20);
            });
    }

    private static string GetCatchLocation(IReadOnlyCollection<Superkat> superkatten)
    {
        var firstCatchLocation = superkatten
            .OrderBy(s => s.CatchDate)
            .ToList()
            .Select(s => s.CatchLocation)
            .FirstOrDefault();

        return firstCatchLocation?.Name ?? string.Empty;
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
            .FirstOrDefault();

        var cageNumber = superkatten
            .Select(s => s.CageNumber)
            .Distinct()
            .ToList()
            .FirstOrDefault();
        
        var catAreaCode = ConvertCatAreaToShowString(catArea);

        return string.IsNullOrEmpty(catAreaCode)
            ? $"{cageNumber}"
            : $"{catAreaCode}-{cageNumber}";
    }

    private static string ConvertCatAreaToShowString(CatArea catArea)
    {
        return catArea switch
        {
            CatArea.Quarantine => "Q",
            CatArea.Infirmary => "ZB",
            CatArea.SmallEnclosure => string.Empty,
            CatArea.BigEnclosure => string.Empty,
            CatArea.Room2 => string.Empty,
            _ => string.Empty
        };
    }
}
