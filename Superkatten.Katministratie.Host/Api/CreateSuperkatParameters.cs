﻿using Superkatten.Katministratie.Host.Entities;

namespace Superkatten.Katministratie.Host.Api
{
    public class CreateSuperkatParameters
    {
        public string CatchLocation { get; set; } = string.Empty;
        public int EstimatedWeeksOld { get; set; }
        public DateTimeOffset CatchDate { get; set; }
        public bool Retour { get; set; }
        public CatArea Area { get; set; }
        public int? CageNumber { get; set; }
        public CatBehaviour Behaviour { get; set; }
        public bool IsKitten { get; set; }
        public Gender Gender { get; set; }
    }
}