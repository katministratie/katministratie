using Microsoft.AspNetCore.Http;
using Superkatten.Katministratie.Contract.ApiInterface;
using Superkatten.Katministratie.Domain.Entities.Locations;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using Superkatten.Katministratie.Infrastructure.Persistence;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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

    public async Task StartSuperkattenAdoptionAsync(StartAdoptionSuperkattenParameters parameters)
    {
        var locations = await _locationRepository.GetLocationsAsync();
        var adoptant = locations
            .Where(o => o.LocationNaw.Name == parameters.AdoptantName)
            .Select(o => (Adoptant)o)
            .FirstOrDefault();

        adoptant ??= await CreateAdoptant(parameters.AdoptantName, parameters.AdoptantEmail);

        var sendResult = await SendStartAdoptionEmailAsync(adoptant, parameters.Superkatten);

        if (sendResult)
        {
            await Task.WhenAll(parameters.Superkatten.Select(StartAdoptionAsync));
        }
    }

    private async Task StartAdoptionAsync(Guid superkatId)
    {
        var superkat = await _superkattenRepository.GetSuperkatAsync(superkatId);
        superkat.StartAdoption();
        await _superkattenRepository.UpdateSuperkatAsync(superkat);
    }

    private async Task<Adoptant> CreateAdoptant(string adoptantName, string adoptantEmail)
    {
        var adoptant = new Adoptant(adoptantName, null, null, null, null, adoptantEmail);
        await _locationRepository.CreateLocationAsync(adoptant);
        return adoptant;
    }

    private async Task<bool> SendStartAdoptionEmailAsync(Adoptant adopter, IReadOnlyCollection<Guid> superkatten)
    {
        if (adopter.LocationNaw.Email is null)
        {
            return false;
        }

        var bodyText = $"Beste {adopter.LocationNaw.Name},{Environment.NewLine}" +
            $"{Environment.NewLine}," +
            $"Je hebt gekozen om het adoptie proces in gang te zetten voor de volgende katten:{Environment.NewLine}" +
            await CreateLineWithCatInfoAsync(superkatten) +
            "Voor het adoptie process zal je bij het ophalen van de katten een aantal formulieren ondertekenen." +
            "hierbij zitten ook de voorwaarden. Deze kan je alvast lezen via de volgende link: <a href=\"#\" \\>" +
            $"Klik op https://katministratie.azurewebsites.net/Adoption/{adopter.Id}/Start om verder te gaan. {Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"Met vriendelijke groet,{Environment.NewLine}" +
            "Stichting Superkatten" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"{Environment.NewLine}" +
            $"voorwaarden adoptie: https://katministratie.azurewebsites.net/voorwaarden_adoptie.pdf{Environment.NewLine}" +
            $"formulier die moet worden ondertekend: https://katministratie.azurewebsites.net/ander_formulier_adoptie.pdf{Environment.NewLine}" +
            $"{Environment.NewLine}";

        return await _mailService.MailToAsync(
            email: adopter.LocationNaw.Email,
            subject: "Adoptie stichting superkatten",
            bodyText: bodyText,
            documentData: Array.Empty<byte>()
        );
    }

    private async Task<string> CreateLineWithCatInfoAsync(IReadOnlyCollection<Guid> superkatten)
    {
        var line = string.Empty;
        foreach (var superkatId in superkatten)
        {
            var superkat = await _superkattenRepository.GetSuperkatAsync(superkatId);
            if (superkat == null) 
            {
                continue;
            }

            line += $"{superkat.Name} met nummer {superkat.UniqueNumber} /n";
        }
        return line;
    }
}
