using Superkatten.Katministratie.Application.Contracts.Entities;

namespace Superkatten.Katministratie.Api.Client.Entities;

public class SuperkatView
{
    public SuperkatDto Superkat { get; init; }
 
    public SuperkatView(SuperkatDto superkat)
    {
        Superkat = superkat;
    }

    public string Number => Superkat.Entered.ToString("yy") + "-" + Superkat.Number.ToString("000");

    public string Entered => Superkat.Entered.ToString("dd/MMM/yy");
}
