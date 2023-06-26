using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using TextRepo.API.Tools;
using TextRepo.API.Responses;
using TextRepo.Services;

namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Login form 
    /// </summary>
    [ApiController]
    [Route("login")]
    [Produces("application/json")]
    public class LoginController : Controller
    {
        private readonly IOptions<AuthOptionsModel> _authOptions;
        private readonly UserService _userService;

        /// <summary>
        /// Create login controller
        /// </summary>
        /// <param name="authOptions">JWT settings</param>
        /// <param name="userService"></param>
        public LoginController(IOptions<AuthOptionsModel> authOptions, UserService userService)
        {
            _authOptions = authOptions;
            _userService = userService;
        }
        
        /// <summary>
        /// Handles GET request - returns login form with email and password
        /// </summary>
        [HttpGet]
        public async Task Login()
        {
            string content = @"<form method='post'>
                <label>Email:</label><br />
                <input email='email' /><br />
                <label>Password:</label><br />
                <input type='password' name='password' /><br />
                <input type='submit' value='Send' />
            </form>";
            Response.ContentType = "text/html;charset=utf-8";
            await Response.WriteAsync(content);
        }
        
        /// <summary>
        /// Handles POST login request
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>AuthResponse with jwt on success, otherwise 401</returns>
        [HttpPost]
        public ActionResult<AuthResponse> Login(string email, string password)
        {
            var user = _userService.GetUser(email, password); 
            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.Unauthorized;
                return Unauthorized();
            }
            
            var claims = new List<Claim> {new Claim("email", email) };
            var jwt = new JwtSecurityToken(
                issuer: _authOptions.Value.Issuer,
                claims: claims,
                expires: DateTime.UtcNow.Add(TimeSpan.FromMinutes(_authOptions.Value.Lifetime)),
                signingCredentials: new SigningCredentials(
                    KeyLoader.GetKey(_authOptions.Value.KeyLocation), 
                    SecurityAlgorithms.HmacSha512));
            
            return Ok(new AuthResponse
            {
                Username = user.Name + " " + user.Surname,
                Email =  user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt)
            });
        }
    }
}