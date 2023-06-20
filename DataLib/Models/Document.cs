namespace DataLib.Models {
    public class Document {
        public int Id { get; set; }

        public string? Title { get; set; }

        public string? Description { get; set; } 

        public string? Contents { get; set; }

        public Project Project { get; set; } = null!;  // foreign key
        public int ProjectId { get; set; }
    }
}
