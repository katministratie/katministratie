using Superkatten.Katministratie.Application.Authenticate;
using Superkatten.Katministratie.Application.Exceptions;
using Superkatten.Katministratie.Domain.Authenticate;
using Superkatten.Katministratie.Domain.Entities;
using System.Collections.Generic;
using System.Linq;
using BcryptNet =  BCrypt.Net.BCrypt;

namespace Superkatten.Katministratie.Application.Services;

public class UserService : IUserService
{
    private readonly IUserAuthorisationRepository _userAuthorisationRepository;
//    private DataContext _context;
    private IJwtUtils _jwtUtils;
    private readonly IUserAuthorisationMapper _userAuthorisationMapper;

    public UserService(
        IUserAuthorisationRepository userAuthorisationRepository,
//        DataContext context,
        IJwtUtils jwtUtils,
        IUserAuthorisationMapper userAuthorisationMapper)
    {
        _userAuthorisationRepository = userAuthorisationRepository;
        //        _context = context;
        _jwtUtils = jwtUtils;
        _userAuthorisationMapper = userAuthorisationMapper;
    }

    public AuthenticateResponse Authenticate(AuthenticateRequest model)
    {
        var user = _userAuthorisationRepository
            .GetUserByName(model.Username);
        //        var user = _context.Users.SingleOrDefault(x => x.Username == model.Username);

        // validate
        if (user is null)
        {
            throw new AuthorisationException("Username is incorrect");
        }

        if (!BcryptNet.Verify(model.Password, user.PasswordHash))
        {
            throw new AuthorisationException("Password is incorrect");
        }

        // authentication successful
        var response = _userAuthorisationMapper.MapToAuthenticateResponse(user);
        response.Token = _jwtUtils.GenerateToken(user);
        return response;
    }

    public IEnumerable<User> GetAll()
    {
        return _userAuthorisationRepository.GetAllUsers();
//        return _context.Users;
    }

    public User GetById(int id)
    {
        return getUser(id);
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

        // map model to new user object
        var user = _userAuthorisationMapper.Map<User>(model);

        // hash password
        user.PasswordHash = BcryptNet.HashPassword(model.Password);

        // save user
        _userAuthorisationRepository.StoreUser(user);
//        _context.Users.Add(user);
//        _context.SaveChanges();
    }

    public void Update(int id, UpdateRequest model)
    {
        var user = getUser(id);

        // validate
        var userExsist = _userAuthorisationRepository
            .GetAllUsers()
            .Any(x => x.Username == model.Username);

        if (model.Username != user.Username && userExsist)
        {
            throw new AuthorisationException("Username '" + model.Username + "' is already taken");
        }

        // copy model to user and save
        user = _userAuthorisationMapper.MapModelToUser(model);

        // hash password if it was entered
        if (!string.IsNullOrEmpty(model.Password))
        {
            user.PasswordHash = BcryptNet.HashPassword(model.Password);
        }

        _userAuthorisationRepository.UpdateUser(user);
//        _context.Users.Update(user);
//        _context.SaveChanges();
    }

    public void Delete(int id)
    {
        _userAuthorisationRepository.DeleteUserById(id)
//        var user = getUser(id);
//        _context.Users.Remove(user);
//        _context.SaveChanges();
    }

    // helper methods

    private User getUser(int id)
    {
        var user = _userAuthorisationRepository.
            GetAllUsers()
            .FirstOrDefault(u => u.Id == id);
//        var user = _context.Users.Find(id);
        if (user is null)
        {
            throw new KeyNotFoundException("User not found");
        }

        return user;
    }
}
