using System;

namespace Superkatten.Katministratie.Application.Contracts
{
    public class CreateSuperkatParameters
    {
        public string Location { get; set; } = string.Empty;
        public DateTimeOffset Birthday { get; set; }
        public DateTimeOffset CatchDate{ get; set; }
        public bool Retour { get; set; }
        public int HokNumber { get; set; }
    }
}
