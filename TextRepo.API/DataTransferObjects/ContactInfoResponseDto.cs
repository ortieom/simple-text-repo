namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for ContactInfo in response
    /// </summary>
    public class ContactInfoResponseDto
    {
        /// <summary>
        /// Primary user key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Represents type of contact (email, tg, etc.)
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// Represents contact itself
        /// </summary>
        public string Value { get; set; } = null!;
    }
}