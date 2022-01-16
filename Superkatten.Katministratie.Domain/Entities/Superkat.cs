using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Superkat
    {
        [Key]
        public int Id { get; set; } 
        public int Number { get; private set; }
        public string Name { get; private set; }

        public Superkat(
            int number,
            string name
        )
        {
            Number = number;
            Name = name;
        }

        public Superkat CreateUpdatedModel(string name)
        {
            return new Superkat(Number, name);
        }
    }
}
