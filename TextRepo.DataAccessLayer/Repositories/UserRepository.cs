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
            var request = db.Users
                .Where(u => u.Id == userId)
                .GroupJoin(
                    db.Contacts, 
                    u => u.Id,
                    ci => ci.User.Id,
                    (x,y) => new { User = x, ContactInfos = y })
                .SelectMany(
                    x => x.ContactInfos.DefaultIfEmpty(),
                    (x,y) => new UserInfo() { User = x.User, ContactInfo = y});
            return request.SingleOrDefault();
        }

        /// <summary>
        /// Get User and ContactInfo united by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoByEmail(string email)
        {
            var request = db.Users
                .Where(u => u.Email == email)
                .GroupJoin(
                    db.Contacts, 
                    u => u.Id,
                    ci => ci.User.Id,
                    (x,y) => new { User = x, ContactInfos = y })
                .SelectMany(
                    x => x.ContactInfos.DefaultIfEmpty(),
                    (x,y) => new UserInfo() { User = x.User, ContactInfo = y});
            return request.SingleOrDefault();
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
            return db.Projects
                .Where(x => x.Id == project.Id)
                .SelectMany(s => s.Users)                
                .OrderBy(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}