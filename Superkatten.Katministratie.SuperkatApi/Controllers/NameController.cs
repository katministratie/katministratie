using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Superkatten.Katministratie.SuperkatApi.Authentication;

namespace Superkatten.Katministratie.SuperkatApi.Controllers;

[Authorize]
[Route("api/[Controller]")]
[ApiController]
public class NameController : ControllerBase
{
    private readonly IJwtAuthenticationManager _jwtAuthenticationManager;
    public NameController(IJwtAuthenticationManager jwtAuthenticationManager)
    {
        _jwtAuthenticationManager = jwtAuthenticationManager;
    }

    // GET: api/Name
    [HttpGet]
    public IEnumerable<string> Get()
    {
        return new string[] { "New York", "New Jersey" };
    }

    // GET: api/Name/5
    [HttpGet("{id}", Name = "Get")]
    public string Get(int id)
    {
        return "New Jersey";
    }

    [AllowAnonymous]
    [HttpPost("authenticate")]
    public IActionResult Authenticate([FromBody] UserCred userCred)
    {
        var token = _jwtAuthenticationManager.Authenticate(userCred.Username, userCred.Password);

        if (token == null)
        {
            return Unauthorized();
        }

        return Ok(token);
    }

    public class UserCred
    {
        public string Username { get; set; }
        public string Password { get; set; }
    }
}
