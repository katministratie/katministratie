namespace Superkatten.Katministratie.Contract.Entities
{
    public class Superkat
    {
        public Guid Id { get; set; }
        public int Number { get; set; }
        public DateTimeOffset Birthday { get; set; }
        public DateTimeOffset CatchDate { get; set; }
        public string CatchLocation { get; set; } = String.Empty;
        public string? Name { get; set; }
        public bool Reserved { get; set; }
        public bool Retour { get; set; }
        public CatArea CatArea { get; set; }
        public int? CageNumber { get; set; }
        public CatBehaviour Behaviour { get; set; }
        public bool IsKitten { get; set; } = true;
        public Gender Gender { get; set; }
    }
}
