using Superkatten.Katministratie.Domain.Exceptions;
using System.ComponentModel.DataAnnotations;

namespace Superkatten.Katministratie.Domain.Entities
{
    public class Gastgezin
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; private set; }

        public Gastgezin(string name)
        {
            if (string.IsNullOrEmpty(name))
            {
                throw new DomainException($"{nameof(Name)} should not be null or empty");
            }

            Name = name;
        }
    }
}
