using Superkatten.Katministratie.Application.Authenticate;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Contract.Authenticate;
using Superkatten.Katministratie.Domain.Entities;
using Superkatten.Katministratie.Infrastructure.Interfaces;
using System.Collections.Generic;
using System.Linq;
using BcryptNet = BCrypt.Net.BCrypt;

namespace Superkatten.Katministratie.Application.Services;

public class UserService : IUserService
{
    private readonly IJwtUtils _jwtUtils;
    private readonly IUserAuthorisationRepository _userAuthorisationRepository;
    private readonly IUserAuthorisationMapper _userAuthorisationMapper;

    public UserService(
        IJwtUtils jwtUtils,
        IUserAuthorisationRepository userAuthorisationRepository,
        IUserAuthorisationMapper userAuthorisationMapper)
    {
        _jwtUtils = jwtUtils;
        _userAuthorisationRepository = userAuthorisationRepository;
        _userAuthorisationMapper = userAuthorisationMapper;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        if (model.Username is null)
        {
            throw new AuthorisationException("Given username may not be null");
        }

        var user = _userAuthorisationRepository.GetUserByName(model.Username);

        if (user is null)
        {
            throw new AuthorisationException("Username is incorrect");
        }

        if (!user.IsEnabled)
        {
            throw new AuthorisationException("User is disabled");
        }

        if (!BcryptNet.Verify(model.Password, user.PasswordHash))
        {
            throw new AuthorisationException("Password is incorrect");
        }

        // authentication successful
        var token = _jwtUtils.GenerateToken(user);        
        return _userAuthorisationMapper.MapToAuthenticateResponse(user, token);
    }

    public IEnumerable<User> GetAll()
    {
        return _userAuthorisationRepository.GetAllUsers();
    }

    public User? GetById(int id)
    {
        return _userAuthorisationRepository
            .GetAllUsers()
            .FirstOrDefault(u => u.Id == id);
    }

    public void Register(RegisterRequest model)
    {
        // validate
        var userExsist = _userAuthorisationRepository
            .GetAllUsers()
            .Any(x => x.Username == model.Username);

        if (userExsist)
        {
            throw new AuthorisationException("Username '" + model.Username + "' is already taken");
        }

        // hash password
        var passwordHash = BcryptNet.HashPassword(model.Password);

        // create domain user model
        var domainUser = new User
        {
            Name = model.Name,
            Email = model.Email,
            IsEnabled = true,
            Username = model.Username,
            PasswordHash = passwordHash
        };

        // save user
        _userAuthorisationRepository.StoreUser(domainUser);
    }

    public void Update(int id, UpdateRequest updateRequest)
    {
        var user = _userAuthorisationRepository
            .GetAllUsers()
            .FirstOrDefault(x => x.Id== id);

        if (user is null)
        {
            throw new AuthorisationException($"Username with id '{id}' is not found");
        }

        // validate
        var userExsist = _userAuthorisationRepository
            .GetAllUsers()
            .Any(x => x.Username == updateRequest.Username);

        if (updateRequest.Username != user.Username && userExsist)
        {
            throw new AuthorisationException("Username '" + updateRequest.Username + "' is already taken");
        }

        // hash password if it was entered
        var passwordHash = string.IsNullOrEmpty(updateRequest.Password)
            ? string.Empty
            :BcryptNet.HashPassword(updateRequest.Password);

        // copy model to user and save
        user = user.Update(updateRequest.Username, updateRequest.Email, updateRequest.Name, passwordHash);

        _userAuthorisationRepository.UpdateUser(user);
    }

    public void Delete(int id)
    {
        _userAuthorisationRepository.DeleteUserById(id);
    }

    public void SetUserEnabledState(int id, bool enabled)
    {
        throw new System.NotImplementedException();
    }
}
