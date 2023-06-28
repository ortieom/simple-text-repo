using TextRepo.DataAccessLayer.Repositories;
using TextRepo.Commons.Models;

namespace TextRepo.API.Services
{
    /// <summary>
    /// Business logic layer for contacts
    /// </summary>
    public class ContactService
    {
        private readonly IContactRepository _repo;
        
        /// <summary>
        /// Creates business logic layer for contacts with reference to repository
        /// </summary>
        /// <param name="repo">Contact repository</param>
        public ContactService(IContactRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Delete contact info from storage
        /// </summary>
        /// <param name="user"></param>
        public void DeleteContact(User user)
        {
            if (user.ContactInfo is not null)
            {
                _repo.Remove(user.ContactInfo);
                _repo.Commit();
            }
        }
    }
}