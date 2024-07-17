using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;


[Route("api/[controller]")]
[ApiController]
public class AccountController : Controller
{
    private readonly IUserService _userService;
    private readonly IConfiguration _configuration;

    public AccountController(IUserService userService, IConfiguration configuration)
    {
        _userService = userService;
        _configuration = configuration;
    }

    [HttpGet]
    public IActionResult Login()
    {
        return Ok();
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginRequest model)
    {
        var user = await _userService.Authenticate(model.Username, model.Password);
        if (user == null )
        {
            ModelState.AddModelError("", "Invalid username or password.");
            return Ok();
        }

        var token = GenerateJwtToken(user);
        HttpContext.Session.SetString("Token", token);  // Store token in session

        return RedirectToAction("Index", "Employee");  // Redirect to the Employee Index page
    }

    private string GenerateJwtToken(User user)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes(_configuration["Authentication:Key"]);
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim(ClaimTypes.Name, user.Username),
                new Claim(ClaimTypes.Role, string.Join(",", user.Roles))  // Add roles to the token
            }),
            Expires = DateTime.UtcNow.AddHours(1),
            Issuer = _configuration["Authentication:Issuer"],
            Audience = _configuration["Authentication:Audience"],
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
        };

        var token = tokenHandler.CreateToken(tokenDescriptor);
        return tokenHandler.WriteToken(token);
    }
}

public class LoginRequest
{
    public string Username { get; set; }
    public string Password { get; set; }
}
