using Superkatten.Katministratie.Contract.ApiInterface;
using System.Collections.Generic;
using System.Threading.Tasks;
using System;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Persistence;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Domain.Entities.Locations;
using System.Linq;

namespace Superkatten.Katministratie.Application.Services;

public class AdoptionService : IAdoptionService
{
    private readonly IMailService _mailService;
    private readonly ISuperkattenRepository _superkattenRepository;
    private readonly ILocationRepository _locationRepository;

    public AdoptionService(
        ILocationRepository locationRepository,
        ISuperkattenRepository superkattenRepository,
        IMailService mailService
    )
    {
        _mailService = mailService;
        _superkattenRepository = superkattenRepository;
        _locationRepository = locationRepository;
    }

    public async Task StartSuperkattenAdoptionAsync(ReserveSuperkattenParameters reserveSuperkattenParameters)
    {
        var adoptant = await CreateAdoptant(
            reserveSuperkattenParameters.AdoptantName,
            reserveSuperkattenParameters.AdoptantEmail
        );

        await ChangeSuperkattenStateToAdoptionRunningAsync(reserveSuperkattenParameters.Superkatten);

        await InformAdoptantAsync(adoptant, reserveSuperkattenParameters.Superkatten);
    }

    private async Task ChangeSuperkattenStateToAdoptionRunningAsync(IReadOnlyCollection<Guid> superkatten)
    {
        foreach (var superkatId in superkatten)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(superkatId);
            var updatedSuperkat = superkat.SetState(SuperkatState.AdoptionRunning);
            await _superkattenRepository.UpdateSuperkatAsync(updatedSuperkat);
        }
    }

    private async Task<Adoptant> CreateAdoptant(string adoptantName, string adoptantEmail)
    {
        var adoptant = new Adoptant(adoptantName, null, null, null, null, adoptantEmail);
        await _locationRepository.CreateLocationAsync(adoptant);
        return adoptant;
    }

    private Task InformAdoptantAsync(Adoptant adoptant, IReadOnlyCollection<Guid> superkatten)
    {
        if (adoptant.Naw.Email is null)
        {
            throw new ApplicationException($"Email of the adoptant '{adoptant.Naw.Name}' must be a valid");
        }

        var bodyText = $"Beste {adoptant.Naw.Name},/n/n" +
            "Je hebt gekozen om het adoptie proces in gang te zetten. /n" +
            $"Klik op XXXXX om verder te gaan. /n/n" +
            "Met vriendelijke groet,/n" +
            "Stichting Superkatten";

            return _mailService.MailToAsync(
                email: adoptant.Naw.Email,
                subject: "Adoptie stichting superkatten",
                bodyText: bodyText,
                documentData: Array.Empty<byte>()
            );
    }
}
