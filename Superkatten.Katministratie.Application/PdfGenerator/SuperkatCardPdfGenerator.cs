using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Application.Entities;
using Superkatten.Katministratie.Application.Interfaces;
using System;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.PdfGenerator;
public class SuperkatCardPdfGenerator : ISuperkatCardPdfGenerator
{
    private readonly ISuperkattenService _superkatService;
    private Superkat _superkat;

    public SuperkatCardPdfGenerator(ISuperkattenService superkatService)
    {
        _superkatService = superkatService;
    }

    public async Task CreateSuperkatCardAsync(Guid id)
    {
        _superkat = await _superkatService.ReadSuperkatAsync(id);
        if (_superkat is null)
        {
            throw new ApplicationException($"Superkat with id {id} cannot be found.");
        }

        Document.Create(container =>
        {
            container.Page(page =>
            {
                page.Size(PageSizes.A4);
                page.Margin(2, Unit.Centimetre);
                page.PageColor(Colors.White);
                page.DefaultTextStyle(x => x.FontSize(20));

                page.Header().Element(ComposeHeader);
                page.Content().Element(ComposeContent);
                page.Footer().Element(ComposeFooter);
            });
        })
        .GeneratePdf("hello.pdf");
    }

    private void ComposeHeader(IContainer container)
    {
        var titleStyle = TextStyle
            .Default
            .FontSize(20)
            .SemiBold()
            .FontColor(Colors.Blue.Medium);

        container.Row(row =>
        {
            row.RelativeItem()
                .Column(column =>
                {
                    column.Item().Text($"Superkat #{_superkat.DisplayableNumber}").Style(titleStyle);

                    column.Item().Text(text =>
                    {
                        text.Span("Gender: ").SemiBold();
                        text.Span($"{_superkat.Gender}");
                    });

                    column.Item().Text(text =>
                    {
                        text.Span("Datum: ").SemiBold();
                        text.Span($"{_superkat.CatchDate:d}");
                    });
                });


            row.RelativeItem()
                .Column(column =>
                {
                    column.Item().Height(50).Placeholder("Placeholder");

                    column.Item().Text($"Hok: #{_superkat.CageNumber}").Style(titleStyle);

                });
        });
    }

    private void ComposeContent(IContainer container)
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

    private void ComposeFooter(IContainer container)
    {
        container
            .AlignCenter()
            .Text(x =>
            {
                x.Span("Superkatten katkaart ");
            });
    }
}
