namespace Superkatten.Katministratie.Domain.Models
{
    public class Superkat
    {
        public int Nummer { get; init; }
        public string Name { get; init; }
        public Superkat(int nummer, string name)
        {
            Nummer = nummer;
            Name = name;
        }
    }
}
