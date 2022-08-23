using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Host.Entities
{
    public class SuperkatView
    {
        public Superkat Superkat { get; set; }

        public string UserFriendlyNumber => Superkat.CatchDate.Year.ToString() + "-" + Superkat.Number.ToString("000");

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
    }
}
