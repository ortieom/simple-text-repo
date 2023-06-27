namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents joining entity for many-to-many relation between users and projects
    /// </summary>
    public class ProjectUser
    {
        /// <summary>
        /// Id of connected User
        /// </summary>
        public int UserId { get; set; }
        /// <summary>
        /// Connected User
        /// </summary>
        public User User { get; set; }
        
        /// <summary>
        /// Id of connected Project
        /// </summary>
        public int ProjectId { get; set; }
        /// <summary>
        /// Connected Project
        /// </summary>
        public Project Project { get; set; }
    }
}