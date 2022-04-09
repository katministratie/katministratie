namespace Superkatten.Katministratie.Web.Services
{
    public interface ISuperkatActionService
    {
        public Task ReserveSuperkatAsync(int superkatNumber);
        public Task RetourSuperkatAsync(int superkatNumber);
    }
}
