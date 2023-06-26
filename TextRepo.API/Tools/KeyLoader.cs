using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;

namespace TextRepo.API.Tools
{
    /// <summary>
    /// Represents secret storage
    /// </summary>
    public class KeyLoader
    {
        private static SymmetricSecurityKey? _key = null;
        private static string? _location = null;
        /// <summary>
        /// Lazy load SymmetricSecurityKey
        /// </summary>
        /// <param name="location">file to read</param>
        /// <returns>SymmetricSecurityKey</returns>
        public static SymmetricSecurityKey GetKey(string location)
        {
            if (_key is null || _location is null || _location != location)
            {
                var keyString = File.ReadAllBytes(location);
                _key = new SymmetricSecurityKey(keyString);
                _location = location;
            }
            return _key;
        }
    }
}