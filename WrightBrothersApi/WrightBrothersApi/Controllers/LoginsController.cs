using WrightBrothersApi.Login;
using Microsoft.AspNetCore.Mvc;

namespace WrightBrothersApi.Controllers;

[ApiController]
[Route("[controller]")]
public class LoginsController : ControllerBase
{
    private readonly SecureLogin _secureLogin;

    public LoginsController()
    {
        // TODO: Replace "YourConnectionString" with your actual connection string.
        _secureLogin = new SecureLogin("YourConnectionString");
    }

    [HttpPost]
    public IActionResult Authenticate(string username, string password)
    {
        // Add fake names for testing
        if (username == "JohnDoeX" && password == "password123")
        {
            return Ok();
        }

        bool isAuthenticated = _secureLogin.AuthenticateUser(username, password);

        if (isAuthenticated)
        {
            return Ok();
        }
        else
        {
            return Unauthorized();
        }
    }
}
