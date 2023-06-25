namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents optional contact information for user
    /// </summary>
    public class ContactInfo
    {
        /// <summary>
        /// Primary key
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// Reference to holder of this property
        /// </summary>
        public virtual User User { get; set; } = null!;
    
        /// <summary>
        /// Represents type of contact (email, tg, etc.)
        /// </summary>
        public string Type { get; set; } = null!;
        /// <summary>
        /// Represents contact itself
        /// </summary>
        public string Value { get; set; } = null!;
    }
}