namespace TextRepo.API
{
    /// <summary>
    /// Settings for jwt
    /// </summary>
    public class AuthOptionsModel
    {
        /// <summary>
        /// Issuer identifier
        /// </summary>
        public string Issuer { get; set; } = null!;
        /// <summary>
        /// Secret file location
        /// </summary>
        public string KeyLocation { get; set; } = null!;
        /// <summary>
        /// Token life span in minutes
        /// </summary>
        public int Lifetime { get; set; }
    }
}