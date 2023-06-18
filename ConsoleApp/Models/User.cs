namespace ConsoleApp.Models {
    public class User {
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;

        public string Email { get; set; } = null!;

        public string HashedPassword { get; set; } = null!;

        public virtual ContactInfo ContactInfo { get; set; } = null!;  // one-to-zero-or-one

        public ICollection<Project> Projects { get; set; } = null!;  // transparent many-to-many
    }
}
