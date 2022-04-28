using Superkatten.Katministratie.Domain.Exceptions;
using System;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        public Guid Id { get; private set; }
        public int Number { get; private set; }
        public DateTimeOffset Birthday { get; private set; }
        public DateTimeOffset CatchDate { get; private set; }
        public string CatchLocation { get; private set; }
        public string? Name { get; private set; } = string.Empty;
        public bool Reserved { get; private set; }
        public bool Retour { get; private set; }
        public CatArea CatArea { get; private set; } = CatArea.Unknown;
        public int? CageNumber { get; private set; }
        public CatBehaviour Behaviour { get; private set; } = CatBehaviour.Unknown;
        public bool IsKitten { get; private set; } = true;
        public Gender Gender { get; private set; } = Gender.Unknown;

        public Superkat(
            Guid id,
            DateTimeOffset catchDate,
            string catchLocation
        )
        {
            Id = id;
            CatchDate = catchDate;
            CatchLocation = catchLocation;
        }

        public void SetNumber(int number)
        {
            if (number < 0)
            {
                throw new DomainException($"Negative number {number} is not allowed");
            }

            Number = number;
        }

        public void SetBirthday(DateTimeOffset birthday)
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
    }
}
