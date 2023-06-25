namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents collection of documents accessible by a list of users
    /// </summary>
    public class Project
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
        
        /// <summary>
        /// References to all documents in project
        /// </summary>
        public List<Document> Documents { get; set; } = new(); // one-to-many
        /// <summary>
        /// References to all users who have access to this project
        /// </summary>
        public List<User> Users { get; set; } = new(); // transparent many-to-many
    }
}