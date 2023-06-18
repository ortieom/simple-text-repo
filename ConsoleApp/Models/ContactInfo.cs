using System.ComponentModel.DataAnnotations.Schema;

namespace ConsoleApp.Models {
    public class ContactInfo {
        public ulong Id { get; set; }
        public virtual User User { get; set; } = null!;

        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}
