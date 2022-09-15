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
        public CatArea CatArea { get; private set; } = CatArea.Quarantine;
        public int? CageNumber { get; private set; }
        public CatBehaviour Behaviour { get; private set; } = CatBehaviour.Unknown;
        public AgeCategory AgeCategory{ get; private set; }
        public Gender Gender { get; private set; } = Gender.Unknown;
        public LitterGranuleType LitterType { get; private set; } = LitterGranuleType.Normal;
        public bool WetFoodAllowed { get; private set; } = true;
        public FoodType FoodType { get; private set; } = FoodType.FirstPhase;
        public string Color { get; private set; } = string.Empty;
        public Guid? GastgezinId { get; private set; }
        public byte[]? Photo { get; private set; }

#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        // Constructor is needed for EF
        public Superkat() { }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
                public Superkat(
            int number,
            DateTime catchDate,
            CatchOrigin catchOrigin
        )
        {
            if (catchOrigin is null)
            {
                throw new DomainException($"{nameof(catchOrigin)} may not be null");
            }

            Id = Guid.NewGuid();

            Number = number;
            CatchDate = catchDate;
            CatchOrigin = catchOrigin;
        }

        public string UniqueNumber
        {
            get
            {
                return CatchDate.Year.ToString() + "-" + Number.ToString("000");
            }
        }

        public void SetBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }

        public void SetBehaviour(CatBehaviour catBehaviour)
        {
            Behaviour = catBehaviour;
        }

        public void SetRetour(bool retour)
        {
            Retour = retour;
        }

        public void SetReserved(bool reserved)
        {
            Reserved = reserved;
        }

        public void SetAgeCategory(AgeCategory ageCategory)
        {
            AgeCategory = AgeCategory;
        }
        public void SetGender(Gender gender)
        {
            Gender = gender;
        }

        public void SetLitterType(LitterGranuleType litterType)
        {
            LitterType = litterType;
        }

        public void SetWetFoodAllowed(bool wetFoodAllowed)
        {
            WetFoodAllowed = wetFoodAllowed;
        }

        public void SetFoodType(FoodType foodType)
        {
            FoodType = foodType;
        }

        public void SetColor(string color)
        {
            Color = color;
        }

        public Superkat WithGastgezinId(Guid? gastgezinId)
        {
            GastgezinId = gastgezinId;

            if (gastgezinId is not null)
            {
                CageNumber = 1;
                CatArea = CatArea.HostFamily;
            }

            return this;
        }

        public Superkat WithPhoto(byte[] photo)
        {
            Photo = photo;

            return this;
        }

        public Superkat WithCatArea(CatArea area)
        {
            CatArea = area;

            return this;
        }

        public Superkat WithCageNumber(int? cageNumber)
        {
            if (cageNumber < 0)
            {
                throw new DomainException($"Cagenumber {cageNumber} cannot be less than zero");
            }

            CageNumber = cageNumber;

            return this;
        }

        public Superkat WithName(string name)
        {
            Name = name;

            return this;
        }

        public Superkat WithState(SuperkatState desiredState)
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
