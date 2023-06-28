namespace TextRepo.API.Responses
{
    /// <summary>
    /// Basic response for authentication methods
    /// </summary>
    public class AuthResponse
    {
        /// <summary>
        /// User name
        /// </summary>
        public string Username { get; set; } = null!;
        /// <summary>
        /// User primary key
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Another user identifier
        /// </summary>
        public string Email { get; set; } = null!;
        /// <summary>
        /// Access token (currently jwt)
        /// </summary>
        public string Token { get; set; } = null!;
    }
}