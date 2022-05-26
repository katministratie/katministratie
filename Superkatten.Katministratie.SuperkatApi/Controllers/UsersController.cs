﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Superkatten.Katministratie.Application.Authenticate;
using Superkatten.Katministratie.Application.Configuration;
using Superkatten.Katministratie.Application.Services;
using Superkatten.Katministratie.Domain.Authenticate;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[Authorize]
[ApiController]
[Route("[controller]")]
public class UsersController : ControllerBase
{
    private IUserService _userService;
    private IUserAuthorisationMapper _userAuthorisationMapper;
    private readonly UserAuthorisationConfiguration _userAuthorisationConfiguration;

    public UsersController(
        IUserService userService,
        IUserAuthorisationMapper userAuthorisationMapper,
        IOptions<UserAuthorisationConfiguration> userAuthorisationConfiguration)
    {
        _userService = userService;
        _userAuthorisationMapper = userAuthorisationMapper;
        _userAuthorisationConfiguration = userAuthorisationConfiguration.Value;
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate(AuthenticateRequest model)
    {
        var response = _userService.Authenticate(model);
        return Ok(response);
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public IActionResult Register(RegisterRequest model)
    {
        _userService.Register(model);
        return Ok(new { message = "Registration successful" });
    }

    [HttpGet]
    public IActionResult GetAll()
    {
        var users = _userService.GetAll();
        return Ok(users);
    }

    [HttpGet("{id}")]
    public IActionResult GetById(int id)
    {
        var user = _userService.GetById(id);
        return Ok(user);
    }

    [HttpPut("{id}")]
    public IActionResult Update(int id, UpdateRequest model)
    {
        _userService.Update(id, model);
        return Ok(new { message = "User updated successfully" });
    }

    [HttpDelete("{id}")]
    public IActionResult Delete(int id)
    {
        _userService.Delete(id);
        return Ok(new { message = "User deleted successfully" });
    }
}