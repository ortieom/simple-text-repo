using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Net;
using System.Security.Claims;
using TextRepo.API.Tools;
using TextRepo.API.Responses;
using TextRepo.API.Services;

namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Login form 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
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
            
            var claims = new List<Claim> {new Claim(ClaimTypes.Email, email) };
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
                UserId = user.Id,
                Email =  user.Email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt)
            });
        }
    }
}