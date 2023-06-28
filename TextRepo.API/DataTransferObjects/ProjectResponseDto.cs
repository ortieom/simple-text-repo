namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for Project in response
    /// </summary>
    public class ProjectResponseDto
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        
        /// <summary>
        /// Optional project name
        /// </summary>
        public string? Name { get; set; }
        /// <summary>
        /// Optional information about project
        /// </summary>
        public string? Description { get; set; }
    }
}