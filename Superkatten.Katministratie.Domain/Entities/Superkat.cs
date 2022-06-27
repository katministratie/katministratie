using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public Guid Id { get; init; }
        public int Number { get; private set; }
        public DateTime Birthday { get; private set; }
        public DateTime CatchDate { get; private set; }
        public string CatchLocation { get; private set; }
        public string Name { get; private set; } = string.Empty;
        public bool Reserved { get; private set; }
        public bool Retour { get; private set; }
        public CatArea CatArea { get; private set; } = CatArea.Quarantine;
        public int? CageNumber { get; private set; }
        public CatBehaviour Behaviour { get; private set; } = CatBehaviour.Unknown;
        public bool IsKitten { get; private set; } = true;
        public Gender Gender { get; private set; } = Gender.Unknown;
        public LitterGranuleType LitterType { get; private set; } = LitterGranuleType.Normal;
        public bool WetFoodAllowed { get; private set; } = true;
        public FoodType FoodType { get; private set; } = FoodType.FirstPhase;
        public string Color { get; private set; } = string.Empty;
        public Superkat(
            int number,
            DateTime catchDate,
            string catchLocation
        )
        {
            Id = Guid.NewGuid();
            Number = number;
            CatchDate = catchDate;
            CatchLocation = catchLocation;
        }

        public void SetBirthday(DateTime birthday)
        {
            Birthday = birthday;
        }

        public void SetName(string name)
        {
            Name = name;
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

        public void SetArea(CatArea area)
        {
            CatArea = area;
        }

        public void SetCageNumber(int? cageNumber)
        {
            if (cageNumber < 0)
            {
                throw new DomainException($"Cagenumber {cageNumber} cannot be less than zero");
            }

            CageNumber = cageNumber;
        }

        public void SetIsKitten(bool isKitten)
        {
            IsKitten = isKitten;
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
    }
}
