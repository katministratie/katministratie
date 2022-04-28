using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.CageCard;

public class CageCardComposer : ICageCardComposer
{
    private const string FILENAME_CAGECARD = "catCard.pdf";
    private Superkat? _superkat;
    public DocumentMetadata GetMetadata() => DocumentMetadata.Default;

    public string Compose(Superkat superkat)
    {
        _superkat = superkat;
        var document = Document.Create(Handler);
        document.GeneratePdf(FILENAME_CAGECARD);

        return FILENAME_CAGECARD;
    }

    private void Handler(IDocumentContainer documentContainer)
    {
        if (_superkat is null)
        {
            throw new ServiceException("Superkat is available");
        }

        var headerComposer = new CageCardHeaderComposer(_superkat);
        var contentComposer = new CageCardContentComposer(_superkat);
        var footerComposer = new CargeCardFooterComposer(_superkat);

        documentContainer.Page(page =>
        {
            page.Size(PageSizes.A4);
            page.Margin(2, Unit.Centimetre);
            page.PageColor(Colors.White);
            page.DefaultTextStyle(x => x.FontSize(20));

            page.Header().Element(headerComposer.Compose);
            page.Content().Element(contentComposer.Compose);
            page.Footer().Element(footerComposer.Compose);
        });
    }
}
