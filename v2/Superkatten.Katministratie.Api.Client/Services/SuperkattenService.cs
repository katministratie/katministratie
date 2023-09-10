using Superkatten.Katministratie.Api.Client.Entities;
using Superkatten.Katministratie.Api.Client.Mappers;
using Superkatten.Katministratie.Application.Contracts.Entities;

namespace Superkatten.Katministratie.Api.Client.Services;

public class SuperkattenService : ISuperkattenService
{
    private readonly IHttpService _httpService;
    private readonly ISuperkatMapper _superkatMapper;

    public SuperkattenService(
        IHttpService httpService,
        ISuperkatMapper superkatMapper
    )
    {
        _httpService = httpService;
        _superkatMapper = superkatMapper;
    }

    public async Task<IReadOnlyList<SuperkatView>> GetSuperkattenAsync()
    {
        var uri = "superkat";

        var superkatten = await _httpService.Get<List<SuperkatDto>>(uri);

        return superkatten is null
            ? new List<SuperkatView>().AsReadOnly()
            : superkatten
                .Select(_superkatMapper.MapToView)
                .ToList()
                    .AsReadOnly();
    }
}
