using DataLib.Models;

namespace DataLib {
    public class UserInfo {
        public ulong Id { get; set; }

        public string Name { get; set; } = null!;
        public string? Surname { get; set; }
        public string Email { get; set; } = null!;
        public string HashedPassword { get; set; } = null!;

        public string ContactType { get; set; } = null!;
        public string ContactValue { get; set; } = null!;

        public ICollection<Project> Projects { get; set; } = null!;  // transparent many-to-many


    }
}
