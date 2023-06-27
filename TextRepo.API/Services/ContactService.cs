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
        /// Edit contact information with optional parameters.
        /// Provide only arguments whose columns must be updated
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>Updated ContactInfo object</returns>
        public ContactInfo EditContact(User user, string? type = null, string? value = null)
        {
            if (user.ContactInfo != null)
            {
                user.ContactInfo.Type = type ?? user.ContactInfo.Type;
                user.ContactInfo.Value = value ?? user.ContactInfo.Value;
            }
            else
            {
                ContactInfo contactInfo = new() { Type = type ?? "", Value = value ?? "", User = user };
                user.ContactInfo = contactInfo;
            }

            _repo.Commit();
            return user.ContactInfo;
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