namespace Superkatten.Katministratie.Host.Entities
{
    public class Superkat
    {
        public Guid Id { get; set; }
        public bool Reserved { get; set; }
        public bool Retour { get; set; }
        public DateTime Birthday { get; set; }
        public DateTime CatchDate { get; set; }
        public string CatchLocation { get; set; } = string.Empty;
        public int Number { get; set; }
        public string Name { get; set; } = string.Empty;
        public Gender Gender { get; set; } = Gender.Unknown;
        public CatBehaviour Behaviour { get; set; } = CatBehaviour.Unknown;
        public bool IsKitten { get; set; }
        public int? CageNumber { set; get; }
        public CatArea CatArea { get; set; }
        public string DisplayableNumber => CatchDate.Year % 100 + "-" + Number.ToString("000");
    }
}
