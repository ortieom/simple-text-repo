using DataLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(DbContext context) : base(context) { }
        /// <summary>
        /// Get User and ContactInfo united by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoById(int userId) {
            var request = from u in db.Users
                          where u.Id == userId
                          join c in db.Contacts on u.ContactInfo.Id equals c.Id into gj
                          from p in gj.DefaultIfEmpty()
                          select new UserInfo {
                              User = u,
                              ContactInfo = p
                          };
            return request.FirstOrDefault();
        }

        /// <summary>
        /// Get User and ContactInfo united by user email
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoByEmail(string email) {
            var request = from u in db.Users where u.Email == email
                          join c in db.Contacts on u.ContactInfo.Id equals c.Id into gj
                          from p in gj.DefaultIfEmpty()
                          select new UserInfo {
                              User = u,
                              ContactInfo = p
                          };
            return request.FirstOrDefault();
        }

        /// <summary>
        /// Get users connected with selected project (quried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (strat with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProject(Project project, int pageNo, int pageSize = 50) {
            return (from user in db.Users
                   where user.Projects.Any(p => p.Id == project.Id)
                   select user).OrderBy(c => c.Id)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
