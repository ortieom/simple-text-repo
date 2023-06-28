namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for ContactInfo in request
    /// </summary>
    public class ContactInfoRequestDto
    {
        /// <summary>
        /// Represents type of contact (email, tg, etc.)
        /// </summary>
        public string? Type { get; set; }
        /// <summary>
        /// Represents contact itself
        /// </summary>
        public string? Value { get; set; }
    }
}