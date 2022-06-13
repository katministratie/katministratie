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

        var user = _userAuthorisationRepository
            .GetUserByName(model.Username);

        if (user is null)
        {
            throw new AuthorisationException("Username is incorrect");
        }

        if (!user.IsEnabled)
        {
            throw new AuthorisationException("User is not enabled yet");
        }

        if (!BcryptNet.Verify(model.Password, user.PasswordHash))
        {
            throw new AuthorisationException("Password is incorrect");
        }

        // authentication successful
        var token = _jwtUtils.GenerateToken(user);
        var response = _userAuthorisationMapper.MapToAuthenticateResponse(user, token);
        return response;
    }

    public IEnumerable<User> GetAll()
    {
        return _userAuthorisationRepository.GetAllUsers();
    }

    public User? GetById(int id)
    {
        var user = _userAuthorisationRepository
            .GetAllUsers()
            .FirstOrDefault(u => u.Id == id);

        return user;
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

        // map model to new user object
        var user = _userAuthorisationMapper.MapModelToUser(model, passwordHash);

        // save user
        _userAuthorisationRepository.StoreUser(user);
    }

    public void Update(int id, UpdateRequest model)
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
            .Any(x => x.Username == model.Username);

        if (model.Username != user.Username && userExsist)
        {
            throw new AuthorisationException("Username '" + model.Username + "' is already taken");
        }

        // hash password if it was entered
        var passwordHash = string.IsNullOrEmpty(model.Password)
            ? string.Empty
            :BcryptNet.HashPassword(model.Password);

        // copy model to user and save
        user = _userAuthorisationMapper.MapModelToUser(id, model, passwordHash);

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
