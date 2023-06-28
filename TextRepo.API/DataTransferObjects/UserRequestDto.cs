namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for User in request
    /// </summary>
    public class UserRequestDto
    {
        /// <summary>
        /// Main username
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Optional user surname
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Unique email
        /// </summary>
        public string? Email { get; set; }
        /// <summary>
        /// New user password
        /// </summary>
        public string? Password { get; set; }
    }
}