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
                column.Spacing(2);

                column.Item()
                    .Padding(2)
                    .Background(Colors.White)
                    .PaddingBottom(5)
                    .Text(_superkat.UniqueNumber)
                    .SemiBold();

                column.Item()
                    .Padding(2)
                    .Text($"Verjaardag: {_superkat.Birthday.ToShortDateString()}");

                column.Item()
                    .Padding(2)
                    .Text($"Kleur: {_superkat.Color}");

                column.Item()
                    .Padding(2)
                    .Text($"Age categorie: {GetAgeCategoryText(_superkat.AgeCategory)}");


                column.Item()
                    .Padding(2)
                    .Text($"Gedrag: {GetGedragText(_superkat.Behaviour)}");
            });
    }

    private string GetGedragText(CatBehaviour behaviour)
    {
        return behaviour switch
        {
            CatBehaviour.Social => "Sociaal",
            CatBehaviour.Unknown => "Onbekend",
            CatBehaviour.Shy => "Schuw",
            _ => string.Empty
        };
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
