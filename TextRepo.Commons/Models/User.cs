namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents user that can access several projects
    /// </summary>
    public class User
    {
        public int Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Surname { get; set; }
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;

        public virtual ContactInfo? ContactInfo { get; set; } // one-to-zero-or-one
        public List<Project> Projects { get; set; } = new(); // transparent many-to-many
    }
}