namespace TextRepo.DataAccessLayer.Models
{
    public class ContactInfo
    {
        public int Id { get; set; }
        public virtual User User { get; set; } = null!;

        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}