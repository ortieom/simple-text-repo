namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for Document in response
    /// </summary>
    public class DocumentResponseDto
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        
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
        
        /// <summary>
        /// Id of referenced project
        /// </summary>
        public int ProjectId { get; set; }
    }
}