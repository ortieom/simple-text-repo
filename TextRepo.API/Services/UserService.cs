using TextRepo.Commons.Models;
using TextRepo.DataAccessLayer;
using BC = BCrypt.Net.BCrypt;
using TextRepo.DataAccessLayer.Repositories;

namespace TextRepo.API.Services
{
    /// <summary>
    /// Business logic layer for users
    /// </summary>
    public class UserService
    {
        private readonly IUserRepository _repo;
        
        /// <summary>
        /// Creates business logic layer for users with reference to repository
        /// </summary>
        /// <param name="repo">User repository</param>
        public UserService(IUserRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Get hashed password
        /// </summary>
        /// <param name="password"></param>
        /// <returns>Hashed password</returns>
        private string HashPassword(string password)
        {
            return BC.HashPassword(password);
        }

        /// <summary>
        /// Compare stored password with one provided by user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        private bool ValidatePassword(User user, string password)
        {
            return BC.Verify(password, user.HashedPassword);
            ;
        }

        /// <summary>
        /// Get user by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>User with corresponding id</returns>
        public User? Get(int id)
        {
            return _repo.Get(id);
        }
        
        /// <summary>
        /// Get user by email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>User with corresponding email</returns>
        public User? GetByEmail(string email)
        {
            return _repo.GetUserInfoByEmail(email)?.User;
        }

        /// <summary>
        /// Create new user with necessary and optional parameters
        /// </summary>
        /// <param name="name">Necessary</param>
        /// <param name="email">Necessary</param>
        /// <param name="password"></param>
        /// <param name="surname"></param>
        /// <returns>New User object</returns>
        public User CreateUser(string name, string email, string password, string? surname = null)
        {
            string hashedPassword = HashPassword(password);
            User user = new() { Name = name, Surname = surname, Email = email, HashedPassword = hashedPassword };

            _repo.Add(user);

            _repo.Commit();

            return user;
        }
        
        /// <summary>
        /// Checks whether user with email exist in db
        /// </summary>
        /// <param name="email"></param>
        /// <returns></returns>
        public bool ExistUser(string email)
        {
            UserInfo? user = _repo.GetUserInfoByEmail(email);
            return user is not null;
        }

        /// <summary>
        /// Save contact info for user
        /// </summary>
        /// <param name="user"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns>New ContactInfo object</returns>
        public ContactInfo AddContactInfo(User user, string type, string value)
        {
            ContactInfo contact = new() { Type = type, Value = value, User = user };

            user.ContactInfo = contact;

            _repo.Commit();

            return contact;
        }

        /// <summary>
        /// Get user by pair email & password.
        /// Returns null if such user is not found
        /// </summary>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns>Requested user</returns>
        public User? GetUser(string email, string password)
        {
            User? user = _repo.GetUserInfoByEmail(email)?.User;
            if (user == null || !ValidatePassword(user, password))
            {
                return null;
            }

            return user;
        }

        /// <summary>
        /// Get users connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProjectPaginated(Project project, int pageNo, int pageSize)
        {
            return _repo.GetUsersInProject(project, pageNo, pageSize);
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
        public User Edit(User user, string? name = null, string? surname = null, string? email = null,
            string? password = null)
        {
            user.Name = name ?? user.Name;
            user.Surname = surname ?? user.Surname;
            user.Email = email ?? user.Email;
            if (password != null)
            {
                user.HashedPassword = HashPassword(password);
            }

            _repo.Commit();
            return user;
        }

        /// <summary>
        /// Delete user from storage
        /// </summary>
        /// <param name="user"></param>
        public void Delete(User user)
        {
            _repo.Remove(user);
            _repo.Commit();
        }

        
    }
}