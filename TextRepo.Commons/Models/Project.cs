namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents collection of documents accessible by a list of users
    /// </summary>
    public class Project
    {
        public int Id { get; set; }

        public string? Name { get; set; }
        public string? Description { get; set; }

        public List<Document> Documents { get; set; } = new(); // one-to-many
        public List<User> Users { get; set; } = new(); // transparent many-to-many
    }
}