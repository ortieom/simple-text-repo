namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents user that can access several projects
    /// </summary>
    public class User
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
        /// Hashed password
        /// </summary>
        public string HashedPassword { get; set; } = null!;
        
        /// <summary>
        /// Reference to optional contact information
        /// </summary>
        public virtual ContactInfo? ContactInfo { get; set; } // one-to-zero-or-one
        /// <summary>
        /// Reference to all projects where user has access
        /// </summary>
        public List<Project> Projects { get; set; } = new(); // many-to-many
        /// <summary>
        /// Reference to all user-project joining entities
        /// </summary>
        public List<ProjectUser> ProjectUsers { get; set; } = new(); // one user to many entities
    }
}