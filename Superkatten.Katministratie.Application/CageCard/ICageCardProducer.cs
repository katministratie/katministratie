﻿using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;

namespace Superkatten.Katministratie.Application.CageCard;

public interface ICageCardProducer
{
    string CreateCageCard(IReadOnlyCollection<Superkat> superkatten);
}