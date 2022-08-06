using QuestPDF.Fluent;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.CageCard;

public class CageCardProducer : ICageCardProducer
{
    private readonly ICageCardComposerFactory _composerFactory;

    public CageCardProducer(ICageCardComposerFactory composerFactory)
    {
        _composerFactory = composerFactory;
    }

    public string CreateCageCard(IReadOnlyCollection<Superkat> superkatten)
    {
        var filePath = Guid.NewGuid().ToString();

        var document = new CageCardDocument(_composerFactory, superkatten);
        document.GeneratePdf(filePath);

        return filePath;
    }
}