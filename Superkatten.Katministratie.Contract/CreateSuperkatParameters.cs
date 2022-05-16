﻿using Superkatten.Katministratie.Contract.Entities;

namespace Superkatten.Katministratie.Contract;

public class CreateSuperkatParameters
{
    public string CatchLocation { get; init; } = string.Empty;
    public int EstimatedWeeksOld { get; init; }
    public DateTime CatchDate { get; init; }
    public bool Retour { get; init; }
    public int? CageNumber { get; init; }
    public CatArea CatArea { get; init; }
    public CatBehaviour Behaviour { get; init; }
    public bool IsKitten { get; init; }
    public Gender Gender { get; init; }
}
