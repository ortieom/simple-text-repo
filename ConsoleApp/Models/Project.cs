﻿namespace ConsoleApp.Models {
    public class Project {
        public ulong Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; }

        public ICollection<Document> Documents { get; set; } = null!;  // one-to-many

        public ICollection<User> Users { get; set; } = null!;  // transparent many-to-many
    }
}
