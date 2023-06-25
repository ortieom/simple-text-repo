using Microsoft.EntityFrameworkCore;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents data access layer for ContactInfo
    /// </summary>
    public class ContactRepository : Repository<ContactInfo>, IContactRepository
    {
        /// <summary>
        /// Creates ContactRepository with basic Repository methods
        /// </summary>
        /// <param name="context"></param>
        public ContactRepository(DbContext context) : base(context)
        {
        }
    }
}
