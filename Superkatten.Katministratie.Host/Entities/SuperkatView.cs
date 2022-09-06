using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Entities
{
    public class SuperkatView
    {
        public Superkat Superkat { get; set; }

        public string GenderIcon => $"./images/Gender/{Superkat.Gender}.png";

        public bool IsAtGastgezin => Superkat.GastgezinId is not null;

        public SuperkatView(Superkat superkat)
        {
            if (superkat is null)
            {
                throw new ArgumentNullException(nameof(superkat));
            }

            Superkat = superkat;
        }

        public bool IsVisible { get; set; } = false;

        public string CatLocationAsString =>
            Superkat.CatArea switch
            {
                CatArea.Quarantine => $"Q-{Superkat.CageNumber}",
                CatArea.Infirmary => $"ZB-{Superkat.CageNumber}",
                CatArea.SmallEnclosure => $"{Superkat.CageNumber}",
                CatArea.BigEnclosure => $"{Superkat.CageNumber}",
                CatArea.Room2 => $"{Superkat.CageNumber}",
                CatArea.HostFamily => "GG",
                _ => $"??-{Superkat.CageNumber}"
            };
    }
}
