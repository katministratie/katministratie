using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.CageCard
{
    public class CageCardHeaderComposer : ICageCardPartComposer
    {
        private Superkat _superkat { get; init; }

        public CageCardHeaderComposer(Superkat superkat)
        {
            _superkat = superkat;
        }

        public void Compose(IContainer container)
        {
            var titleStyle = TextStyle
            .Default
            .FontSize(20)
            .SemiBold()
            .FontColor(Colors.Blue.Medium);

            container.Row(row =>
            {
                row.RelativeItem()
                    .Border(1)
                    .Column(column =>
                    {
                        column.Item().AlignMiddle().AlignCenter().Text($"{_superkat.CatchDate.Year}{_superkat.Number}").Style(titleStyle);
                    });

                row.RelativeItem()
                    .Border(1)
                    .Column(column =>
                    {
                        column.Item().AlignMiddle().AlignCenter().Text($"{_superkat.Gender}").Style(titleStyle);
                    });


                row.RelativeItem()
                    .Border(1)
                    .Column(column =>
                    {
                        column.Item().AlignMiddle().AlignCenter().Text($"{_superkat.CatchDate.ToString("dd.MMMM yyyy")}").Style(titleStyle);
                        column.Item().AlignMiddle().AlignCenter().Text($"{_superkat.CatchLocation}").Style(titleStyle);
                    });

                if (_superkat.CatArea == CatArea.Unknown)
                {
                    row.RelativeItem()
                        .Border(1)
                        .Column(column =>
                        {
                            column.Item().AlignCenter().Text($"Hok nr.").Style(titleStyle);
                            column.Item().AlignCenter().Text($"{_superkat.CageNumber}").Style(titleStyle);
                        });
                }
                else
                {
                    row.RelativeItem()
                        .Border(1)
                        .Column(column =>
                        {
                            column.Item().AlignMiddle().Text($"Geplaatst in").Style(titleStyle);
                            column.Item().AlignMiddle().Text($"de grote ren").Style(titleStyle);
                        });
                }
            });
        }
    }
}
