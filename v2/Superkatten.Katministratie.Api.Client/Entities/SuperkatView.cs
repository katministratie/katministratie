using Superkatten.Katministratie.Application.Contracts.Entities;

namespace Superkatten.Katministratie.Api.Client.Entities;

public class SuperkatView
{
    public SuperkatDto Superkat { get; init; }
 
    public SuperkatView(SuperkatDto superkat)
    {
        Superkat = superkat;
    }
}
