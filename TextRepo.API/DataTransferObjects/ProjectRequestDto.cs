namespace TextRepo.API.DataTransferObjects
{
    /// <summary>
    /// Represents DTO for Project in request
    /// </summary>
    public class ProjectRequestDto
    {
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