using Superkatten.Katministratie.Domain.Entities;
using System;

namespace Superkatten.Katministratie.Application.Entities
{
    public class CreateSuperkatParameters
    {
        public string CatchLocation { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }
        public DateTimeOffset CatchDate { get; set; }
        public bool Retour { get; set; }
        public CatArea Area { get; set; }
        public int? CageNumber { get; set; }
        public CatBehaviour Behaviour { get; set; }
    }
}
