namespace Superkatten.Katministratie.Host.Services;

public interface ISuperkatActionService
{
    public Task ToggleReserveSuperkatAsync(int superkatNumber);
    public Task ToggleRetourSuperkatAsync(int superkatNumber);
    public Task CreateSuperkatCardAsync(Guid id);
}
