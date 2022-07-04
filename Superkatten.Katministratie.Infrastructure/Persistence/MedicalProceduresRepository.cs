﻿using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class MedicalProceduresRepository : IMedicalProceduresRepository
{
    private readonly ILogger<MedicalProceduresRepository> _logger;
    private readonly SuperkattenDbContext _context;

    public MedicalProceduresRepository(
        ILogger<MedicalProceduresRepository> logger,
        SuperkattenDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public async Task AddMedicalProcedureAsync(AddMedicalProcedureParameters addMedicalProcedureParameters)
    {
        var gastgezinExists = _context
            .SuperKatten
            .Any(o => o.Id == addMedicalProcedureParameters.SuperkatId);

        var medicalProcedure = new MedicalProcedure(
            addMedicalProcedureParameters.ProcedureType,
            addMedicalProcedureParameters.SuperkatId,
            addMedicalProcedureParameters.Timestamp,
            addMedicalProcedureParameters.Remark
        );

        await _context.MedicalProcedures.AddAsync(medicalProcedure);
        await _context.SaveChangesAsync();
    }
}
