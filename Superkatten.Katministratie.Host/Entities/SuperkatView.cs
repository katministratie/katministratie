using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Entities
{
    public class SuperkatView
    {
        public Guid Id { get; }
        public bool Retour { get; }
        public bool Reserved { get; }
        public int Number { get; }
        public CatBehaviour Behaviour { get; }
        public Gender Gender { get; }
        public DateTime CatchDate { get; }
        public string CatchLocation { get; }
        public string CatArea { get; }
        public string? CageNumber { get; }

        public string UserFriendlyNumber => CatchDate.Year.ToString() + "-" + Number.ToString("000");

        public SuperkatView(Superkat superkat)
        {
            if (superkat is null)
            {
                throw new ArgumentNullException(nameof(superkat));
            }

            Id = superkat.Id;
            Retour = superkat.Retour;
            Reserved = superkat.Reserved;
            CatchDate = superkat.CatchDate;
            Number = superkat.Number;
            Behaviour = superkat.Behaviour;
            Gender = superkat.Gender;
            CatchLocation = superkat.CatchLocation;
            CatArea = superkat.CatArea.ToString();
            CageNumber = superkat.CageNumber is null ? superkat.CageNumber.ToString() : string.Empty;
        }
    }
}
