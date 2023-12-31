using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using TextRepo.API.Tools;
using TextRepo.API.Responses;
using TextRepo.API.Services;

namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Registration 
    /// </summary>
    [ApiController]
    [Route("[controller]")]
    [Produces("application/json")]
    public class RegisterController : Controller
    {
        private readonly IOptions<AuthOptionsModel> _authOptions;
        private readonly UserService _userService;
        
        /// <summary>
        /// Creates registration controller
        /// </summary>
        /// <param name="authOptions">JWT settings</param>
        /// <param name="userService"></param>
        public RegisterController(IOptions<AuthOptionsModel> authOptions, UserService userService)
        {
            _authOptions = authOptions;
            _userService = userService;
        }
        
        /// <summary>
        /// Creates new user
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <param name="name"></param>
        /// <returns>Works as login for new user in success, otherwise 400</returns>
        [HttpPost]
        [ProducesResponseType(typeof(AuthResponse), 200)]
        public IActionResult Register(string email, string password, string name)
        {
            if (_userService.ExistUser(email))
            {
                return BadRequest("User already exists");
            }
        
            var user = _userService.CreateUser(email: email, password: password, name: name);
            
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
                Username = name,
                UserId = user.Id,
                Email =  email,
                Token = new JwtSecurityTokenHandler().WriteToken(jwt)
            });
        }
    }
}