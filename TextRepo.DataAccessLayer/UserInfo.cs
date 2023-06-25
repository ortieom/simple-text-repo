using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer
{
    /// <summary>
    /// Represents concatenation of all user properties
    /// </summary>
    public class UserInfo
    {
        /// <summary>
        /// main User instance
        /// </summary>
        public User User = null!;

        /// <summary>
        /// Optional information
        /// </summary>
        public ContactInfo? ContactInfo;
    }
}