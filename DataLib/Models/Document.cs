namespace ConsoleApp.Models {
    public class Document {
        public ulong Id { get; set; }

        public string? Name { get; set; }

        public string? Description { get; set; } 

        public string? Contents { get; set; }

        public Project Project { get; set; } = null!;  // foreign key
    }
}
