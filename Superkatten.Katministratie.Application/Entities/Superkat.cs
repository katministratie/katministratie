using Superkatten.Katministratie.Domain.Entities;
using System;

namespace Superkatten.Katministratie.Application.Entities
{
    public class Superkat
    {
        public Guid Id { get; init; }
        public int Number { get; init; }
        public DateTimeOffset Birthday { get; init; }
        public DateTimeOffset CatchDate { get; init; }
        public string CatchLocation { get; init; } = string.Empty;
        public string? Name { get; init; } = string.Empty;
        public bool Reserved { get; init; }
        public bool Retour { get; init; }
        public CatArea Area { get; init; }
        public int? CageNumber { get; init; }
        public CatBehaviour Behaviour { get; init; } = CatBehaviour.Unknown;
        public bool IsKitten { get; init; }

        public string DisplayableNumber
        {
            get
            {
                var yearPart = CatchDate.Year % 100;
                return yearPart + "-" + Number.ToString("000");
            }
        }
    }
}
