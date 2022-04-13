namespace Superkatten.Katministratie.Web.Services
{
    public interface ISuperkatActionService
    {
        public Task ToggleReserveSuperkatAsync(int superkatNumber);
        public Task ToggleRetourSuperkatAsync(int superkatNumber);
    }
}
