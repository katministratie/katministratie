using System.Threading.Tasks;

namespace Superkatten.Katministratie.Application.Interfaces
{
    public interface ISuperkatAction
    {
        Task ToggleRetourAsync(int superkatNumber);
        Task ToggleReserveAsync(int superkatNumber);
    }
}
