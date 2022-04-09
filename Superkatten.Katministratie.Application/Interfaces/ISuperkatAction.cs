using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkatAction
    {
        Task GoingRetourAsync(int superkatNumber, bool reserve);
        Task ReserveAsync(int superkatNumber, bool reserve);
    }
}
