using QuestPDF.Drawing;
using QuestPDF.Fluent;
using QuestPDF.Infrastructure;
using SkiaSharp;
using Superkatten.Katministratie.Application.CageCard.Details.SuperkatCard;
using Superkatten.Katministratie.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Application.CageCard.Details
{
    public class CageCardDefaultFooterComposer : IComponent
    {
        private IReadOnlyCollection<Superkat> _superkatten { get; init; }

        public CageCardDefaultFooterComposer(IReadOnlyCollection<Superkat> superkatten)
        {
            _superkatten = superkatten;
        }

        public void Compose(IContainer container)
        {
            var foods = _superkatten
                .Select(s => s.FoodType)
                .Distinct()
                .ToList();

            var litterTypes = _superkatten
                .Select(s => s.LitterType)
                .Distinct()
                .ToList();

            var wedFoods = _superkatten
                .Select(s => s.WetFoodAllowed)
                .Distinct()
                .ToList();

            container
                .PaddingTop(10)
                .Column(column =>
                {
                    column.Item()
                        .Text(string.Join("-", foods));
                    column.Spacing(5);

                    column.Item()
                        .Text(string.Join("-", litterTypes));

                    column.Spacing(5);

                    column.Item()
                        .Text(string.Join("-", wedFoods));
                    column.Spacing(5);

                    column.Item()
                        .BorderBottom(1)
                        .PaddingBottom(5)
                        .AlignCenter()
                        .Text($"Kooikaart superkatten (c) {DateTime.UtcNow.Year}")
                        .SemiBold();
                });
        }

    }
}
