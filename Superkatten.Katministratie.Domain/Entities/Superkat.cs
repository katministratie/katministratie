using Superkatten.Katministratie.Domain.Entities.Locations;
using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public Guid Id { get; init; }
        public int Number { get; private set; }
        public SuperkatState State { get; set; } = SuperkatState.Monitoring;
        public DateTime Birthday { get; private set; }
        public DateTime CatchDate { get; private set; } = DateTime.UtcNow;
        public CatchOrigin CatchOrigin { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public bool Reserved { get; private set; }
        public bool Retour { get; private set; }
        public CatBehaviour Behaviour { get; private set; } = CatBehaviour.Unknown;
        public AgeCategory AgeCategory { get; private set; }
        public Gender Gender { get; private set; } = Gender.Unknown;
        public LitterGranuleType LitterType { get; private set; } = LitterGranuleType.Normal;
        public bool WetFoodAllowed { get; private set; } = true;
        public FoodType FoodType { get; private set; } = FoodType.FirstPhase;
        public string Color { get; private set; } = string.Empty;
        public BaseLocation Location { get; private set; }
        public byte[]? Photo { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // Constructor is needed for EF
        public Superkat() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        public Superkat(
            int number,
            DateTime catchDate,
            CatchOrigin catchOrigin,
            BaseLocation location
        )
        {
            Id = Guid.NewGuid();

            Number = number;
            CatchDate = catchDate;
            CatchOrigin = catchOrigin;
            Location = location;
        }

        public string UniqueNumber => CatchDate.Year.ToString() + "-" + Number.ToString("000");

        public Superkat CreateUpdatedModel(
            DateTime birthday,
            CatBehaviour catBehaviour,
            bool retour,
            bool reserved,
            AgeCategory ageCategory,
            Gender gender,
            LitterGranuleType litterType,
            bool wetFoodAllowed,
            FoodType foodType,
            string color,
            byte[] photo,
            string name
        )
        {
            if (CatchOrigin is null)
            {
                throw new DomainException($"{nameof(CatchOrigin)} may not be null");
            }

            if (Location is null)
            {
                throw new DomainException($"{nameof(Location)} may not be null");
            }

            return new Superkat(Number, CatchDate, CatchOrigin, Location)
            {
                Id = Id,

                Birthday = birthday,
                Behaviour = catBehaviour,
                Retour = retour,
                Reserved = reserved,
                AgeCategory = AgeCategory,
                Gender = gender,
                LitterType = litterType,
                WetFoodAllowed = wetFoodAllowed,
                FoodType = foodType,
                Color = color,
                Photo = photo,
                Name = name
            };
        }

        public void Relocate(BaseLocation newLocation)
        {
            Location = newLocation;
        }

        public Superkat SetState(SuperkatState desiredState)
        {
            State = (State, desiredState) switch
            {
                (SuperkatState.Monitoring, SuperkatState.AdoptionRunning) => desiredState,
                (SuperkatState.AdoptionRunning, SuperkatState.WaitForPayment) => desiredState,
                (SuperkatState.WaitForPayment, SuperkatState.FinalizeChecks) => desiredState,
                (SuperkatState.WaitForPayment, SuperkatState.Monitoring) => desiredState,
                (SuperkatState.FinalizeChecks, SuperkatState.Done) => desiredState,
                _ => throw new DomainException($"It is not possible to transition from {State} to {desiredState}")
            };

            return this;
        }
    }
}
