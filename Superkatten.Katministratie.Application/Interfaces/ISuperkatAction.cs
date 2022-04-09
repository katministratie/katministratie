using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkatAction
    {
        Task GoingRetourAsync(int number);
        Task ReserveAsync(int superkatNumber);
    }
}
