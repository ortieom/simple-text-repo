using System.Security.Claims;
using TextRepo.Commons.Models;
using TextRepo.API.Services;
namespace TextRepo.API.Tools
{
    /// <summary>
    /// Represents collection of methods to get entities by claims in JWT
    /// </summary>
    public class TokenEntities
    {
        /// <summary>
        /// Get User by email claim
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="userService"></param>
        /// <returns></returns>
        public static User? GetUser(ClaimsIdentity? identity, UserService userService)
        {
            if (identity is null)
                return null;
            IEnumerable<Claim> claim = identity.Claims; 

            var userEmailClaim = claim
                .FirstOrDefault(x => x.Type == ClaimTypes.Email);
            if (userEmailClaim is null)
                return null;

            return userService.GetByEmail(userEmailClaim.Value);
        }
    }
}