namespace TextRepo.DataAccessLayer
{
    /// <summary>
    /// Represents options for database connection
    /// </summary>
    public class DbSettingsModel
    {
        /// <summary>
        /// Data source
        /// </summary>
        public string ConnectionString { get; set; } = null!;
        /// <summary>
        /// Flag for verbose logging output
        /// </summary>
        public bool Verbose { get; set; }
    }
}

