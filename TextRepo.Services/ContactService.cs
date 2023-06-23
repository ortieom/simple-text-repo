using TextRepo.DataAccessLayer.Repositories;
using TextRepo.DataAccessLayer.Models;

namespace TextRepo.Services
{
    public class ContactService
    {
        private readonly IContactRepository _repo;

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
                _repo.Remove(user.ContactInfo);
        }
    }
}