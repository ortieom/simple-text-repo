using DataLib;
using DataLib.Models;
using BC = BCrypt.Net.BCrypt;

namespace Services {
    public class UserService {
        private readonly IUnitOfWork _repos;

        public UserService(IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public String HashPassword(String password) {
            return BC.HashPassword(password);
        }

        public Boolean ValidatePassword(User user, String password) {
            return BC.Verify(password, user.HashedPassword); ;
        }

        public User? Get(int id) {
            return _repos.Users.Get(id);
        }

        public User CreateUser(String name, String email, String password, String? surname = null) {
            String hashedPassword = HashPassword(password);
            User user = new() { Name = name, Surname = surname, Email = email, HashedPassword = hashedPassword };

            _repos.Users.Add(user);

            _repos.Commit();

            return user;
        }

        public ContactInfo AddContactInfo(User user, String type, String value) {
            ContactInfo contact = new() { Type = type, Value = value, User = user };

            user.ContactInfo = contact;

            _repos.Commit();

            return contact;
        }

        public User? GetUser(String email, String password) {
            User? user = _repos.Users.GetUserInfoByEmail(email)?.User;
            if (user == null || !ValidatePassword(user, password)) { 
                return null;
            }
            return user;
        }

        public ICollection<User> GetUsersInProjectPaginates(Project project, int pageIndex, int pageSize) {
            return _repos.Users.GetUsersInProject(project, pageIndex, pageSize);
        }

        public User Edit(User user, String? name = null, String? surname = null, String? email = null, String? password = null) {
            user.Name = name ?? user.Name;
            user.Surname = surname ?? user.Surname;
            user.Email = email ?? user.Email;
            if (password != null) {
                user.HashedPassword = HashPassword(password);
            }
            _repos.Commit();
            return user;
        }

        public void Delete(User user) {
            _repos.Delete(user);
        }

        public ContactInfo EditContact(User user, String? type = null, String? value = null) {
            if (user.ContactInfo != null) {
                user.ContactInfo.Type = type ?? user.ContactInfo.Type;
                user.ContactInfo.Value = value ?? user.ContactInfo.Value;
            } else {
                ContactInfo contactInfo = new() { Type = type ?? "", Value = value ?? "", User = user };
                user.ContactInfo = contactInfo;
            }
            
            _repos.Commit();
            return user.ContactInfo;
        }

        public void DeleteContact(User user) {
            _repos.Delete(user.ContactInfo);
        }
    }
}
