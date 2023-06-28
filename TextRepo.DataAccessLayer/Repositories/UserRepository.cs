using Microsoft.EntityFrameworkCore;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents data access layer for User
    /// </summary>
    public class UserRepository : Repository<User>, IUserRepository
    {
        /// <summary>
        /// Creates UserRepository with basic Repository methods
        /// </summary>
        /// <param name="context"></param>
        public UserRepository(Context context) : base(context)
        {
        }

        /// <summary>
        /// Get User and ContactInfo united by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoById(int userId)
        {
            var user = Db.Users
                .Include(x => x.ContactInfo)
                .SingleOrDefault(u => u.Id == userId);

            return user is null ? null : new UserInfo {User = user, ContactInfo = user?.ContactInfo};
        }

        /// <summary>
        /// Get User and ContactInfo united by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoByEmail(string email)
        {
            var user = Db.Users
                .Include(x => x.ContactInfo)
                .SingleOrDefault(u => u.Email == email);

            return user is null ? null : new UserInfo {User = user, ContactInfo = user?.ContactInfo};
        }

        /// <summary>
        /// Get users connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProject(Project project, int pageNo, int pageSize = 50)  // TODO: id
        {
            return Db.Projects
                .Where(x => x.Id == project.Id)
                .SelectMany(s => s.Users)                
                .OrderBy(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}