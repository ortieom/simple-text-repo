using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public class ContactRepository : Repository<ContactInfo>, IContactRepository
    {
        public ContactRepository(DbContext context, ILogger<ContactRepository> someLogger) : base(context, someLogger)
        {
        }
    }
}
