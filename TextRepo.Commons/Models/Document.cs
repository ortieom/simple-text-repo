namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents text file that belongs to exact project
    /// </summary>
    public class Document
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
        /// Reference to project of this document
        /// </summary>
        public Project Project { get; set; } = null!; // foreign key
        /// <summary>
        /// Id of referenced project
        /// </summary>
        public int ProjectId { get; set; }
    }
}