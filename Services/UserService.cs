using DataLib;
using DataLib.Models;
using BC = BCrypt.Net.BCrypt;

namespace Services {
    public class UserService {
        private readonly IUnitOfWork _repos;

        public UserService(IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        /// <summary>
        /// Get hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Hashed password</returns>
        public String HashPassword(String password) {
            return BC.HashPassword(password);
        }

        /// <summary>
        /// Compare stored password with one provided by user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        public Boolean ValidatePassword(User user, String password) {
            return BC.Verify(password, user.HashedPassword); ;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User with corresponding id</returns>
        public User? Get(int id) {
            return _repos.Users.Get(id);
        }

       /// <summary>
       /// Create new user with necessary and optional parameters
       /// </summary>
       /// <param name="name">Necessary</param>
       /// <param name="email">Necessary</param>
       /// <param name="password"></param>
       /// <param name="surname"></param>
       /// <returns>New User object</returns>
        public User CreateUser(String name, String email, String password, String? surname = null) {
            String hashedPassword = HashPassword(password);
            User user = new() { Name = name, Surname = surname, Email = email, HashedPassword = hashedPassword };

            _repos.Users.Add(user);

            _repos.Commit();

            return user;
        }

        /// <summary>
        /// Save contact info for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>New ContactInfo object</returns>
        public ContactInfo AddContactInfo(User user, String type, String value) {
            ContactInfo contact = new() { Type = type, Value = value, User = user };

            user.ContactInfo = contact;

            _repos.Commit();

            return contact;
        }

        /// <summary>
        /// Get user by pair email & password.
        /// Returns null if such user is not found
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Requested user</returns>
        public User? GetUser(String email, String password) {
            User? user = _repos.Users.GetUserInfoByEmail(email)?.User;
            if (user == null || !ValidatePassword(user, password)) { 
                return null;
            }
            return user;
        }

        /// <summary>
        /// Get users connected with selected project (quried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (strat with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProjectPaginated(Project project, int pageNo, int pageSize) {
            return _repos.Users.GetUsersInProject(project, pageNo, pageSize);
        }

        /// <summary>
        /// Edit user with optional parameters.
        /// Provide only arguments whose columns must be updated
        /// </summary>
        /// <param name="user"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Updated User object</returns>
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

        /// <summary>
        /// Delete user from storage
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user) {
            _repos.Delete(user);
        }

        /// <summary>
        /// Edit contact information with optional parameters.
        /// Provide only arguments whose columns must be updated
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>Updated ContactInfo object</returns>
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

        /// <summary>
        /// Delete contact info from storage
        /// </summary>
        /// <param name="user"></param>
        public void DeleteContact(User user) {
            _repos.Delete(user.ContactInfo);
        }
    }
}
