using QuestPDF.Fluent;
using QuestPDF.Helpers;
using QuestPDF.Infrastructure;
using Superkatten.Katministratie.Domain.Entities;
using System;

namespace Superkatten.Katministratie.Application.CageCard.Details;

public class CageCardSuperkatDetailsComponent : IComponent
{
    private readonly Superkat _superkat;

    public CageCardSuperkatDetailsComponent(Superkat superkat)
    {
        _superkat = superkat;
    }

    public void Compose(IContainer container)
    {
        container
            .Padding(10)
            .Border(1)
            .BorderColor(Colors.Amber.Accent1)
            .Column(column =>
            {
                column.Item()
                    .Image(_superkat.Photo ?? Array.Empty<byte>());

                column.Item()
                    .Padding(2)
                    .Background(Colors.White)
                    .PaddingBottom(5)
                    .Text(_superkat.UniqueNumber)
                    .SemiBold();

                column.Item()
                    .Text($"Jarig op {_superkat.Birthday.ToShortDateString()}");

                column.Item()
                    .Text($"Kleur: {_superkat.Color}");

                column.Item()
                    .Text($"{GetAgeCategoryText(_superkat.AgeCategory)}");

            });
    }

    private static string GetAgeCategoryText(AgeCategory ageCategory)
    {
        return ageCategory switch
        {
            AgeCategory.Juvenile => "",
            AgeCategory.Adult => "Volwassen",
            AgeCategory.Kitten => "Kitten",
            _ => string.Empty
        };
    }
}
