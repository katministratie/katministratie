using Microsoft.Extensions.Logging;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Exceptions;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Superkatten.Katministratie.Infrastructure.Persistence;

public class UserAuthorisationRepository : IUserAuthorisationRepository
{
    private readonly ILogger<SuperkattenRepository> _logger;
    private readonly SuperkattenDbContext _context;
    public UserAuthorisationRepository(
        ILogger<SuperkattenRepository> logger,
        SuperkattenDbContext context
    )
    {
        _logger = logger;
        _context = context;
    }

    public void DeleteUserById(int userId)
    {
        var user = GetUser(userId);
        
        _context.Users.Remove(user);
        _context.SaveChanges();
    }

    private User GetUser(int id)
    {
        var user = GetAllUsers()
            .FirstOrDefault(u => u.Id == id);

        if (user is null)
        {
            throw new DatabaseException("User not found");
        }

        return user;
    }

    public IReadOnlyCollection<User> GetAllUsers()
    {
        return _context.Users
            .ToList();
    }

    public User? GetUserByName(string userName)
    {
        var user = _context.Users
            .SingleOrDefault(x => x.Username == userName);

        return user;
    }

    public void StoreUser(User user)
    {
        _context.Users.Add(user);
        _context.SaveChanges();
    }

    public void UpdateUser(User user)
    {
        _context.Users.Update(user);
        _context.SaveChanges();
    }
}
