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
                    .Image(GetBehaviourImageFileName(_superkat.Behaviour));

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

    private string GetBehaviourImageFileName(CatBehaviour behaviour)
    {
        return behaviour switch
        {
            CatBehaviour.Social => "./Images/Behavour/Sociaal.jpg",
            CatBehaviour.Unknown => "./Images/Behavour/Onbekend.jpg",
            CatBehaviour.Shy => "./Images/Behavour/Schuw.jpg",
            _ => "./Images/Behavour/Error.jpg"
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
