﻿namespace TextRepo.Commons.Models
{
    /// <summary>
    /// Represents optional contact information for user
    /// </summary>
    public class ContactInfo
    {
        public int Id { get; set; }
        public virtual User User { get; set; } = null!;

        public string Type { get; set; } = null!;
        public string Value { get; set; } = null!;
    }
}