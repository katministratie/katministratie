﻿using Superkatten.Katministratie.Application.Entities;
using DomainEntities = Superkatten.Katministratie.Domain.Entities;

namespace Superkatten.Katministratie.Application.Mappers
{
    public interface ISuperkattenMapper
    {
        public DomainEntities.Superkat MapToDomain(Superkat superkat);
        public Superkat MapFromDomain(DomainEntities.Superkat superkat);
    }
}
