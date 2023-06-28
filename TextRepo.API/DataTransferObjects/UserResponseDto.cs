namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for User in response
    /// </summary>
    public class UserResponseDto
    {
        
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Main username
        /// </summary>
        public string Name { get; set; } = null!;
        /// <summary>
        /// Optional user surname
        /// </summary>
        public string? Surname { get; set; }
        /// <summary>
        /// Unique email
        /// </summary>
        public string Email { get; set; } = null!;
        
        /// <summary>
        /// Contact type
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Contact value
        /// </summary>
        public string? Value { get; set; }
    }
}