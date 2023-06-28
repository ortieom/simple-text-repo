namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for ContactInfo in request
    /// </summary>
    public class DocumentRequestDto
    {
        /// <summary>
        /// Optional name of the document
        /// </summary>
        public string? Title { get; set; }
        /// <summary>
        /// Optional document details
        /// </summary>
        public string? Description { get; set; }
        /// <summary>
        /// Optional document text 
        /// </summary>
        public string? Contents { get; set; }
    }
}