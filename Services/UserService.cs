using DataLib;
using DataLib.Models;
using BC = BCrypt.Net.BCrypt;

namespace Services {
    public class UserService {
        private readonly IUnitOfWork _repos;

        public UserService(IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public Boolean HasAccesToProject(User user, Project project) {
            return project.Users.Contains(user);
        }

        public Boolean HasAccesToDocument(User user, Document document) {
            return HasAccesToProject(user, document.Project);
        }

        public String HashPassword(String password) {
            return BC.HashPassword(password);
        }

        public Boolean ValidatePassword(User user, String password) {
            return BC.Verify(password, user.HashedPassword); ;
        }

        public User CreateUser(String name, String email, String password, String? surname = null) {
            String hashedPassword = HashPassword(password);
            User user = new() { Name = name, Surname = surname, Email = email, HashedPassword = hashedPassword };

            _repos.Users.Add(user);

            return user;
        }

        public ContactInfo AddContactInfo(User user, String type, String value) {
            ContactInfo contact = new() { Type = type, Value = value, User = user };

            user.ContactInfo = contact;

            return contact;
        }

    }
}
