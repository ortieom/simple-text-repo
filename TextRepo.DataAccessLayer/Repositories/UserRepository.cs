using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TextRepo.DataAccessLayer.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public class UserRepository : Repository<User>, IUserRepository
    {
        public UserRepository(DbContext context, ILogger<UserRepository> someLogger) : base(context, someLogger)
        {
        }

        /// <summary>
        /// Get User and ContactInfo united by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoById(int userId)
        {
            logger.LogDebug("Requested user with id {0}", userId);

            var request = from u in db.Users
                where u.Id == userId
                join c in db.Contacts on u.ContactInfo.Id equals c.Id into gj
                from p in gj.DefaultIfEmpty()
                select new UserInfo
                {
                    User = u,
                    ContactInfo = p
                };

            try
            {
                var res = request.SingleOrDefault();
                return res;
            }
            catch (InvalidOperationException)
            {
                logger.LogError("More than 1 user with id {0}", userId);
            }

            return null;
        }

        /// <summary>
        /// Get User and ContactInfo united by user email
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoByEmail(string email)
        {
            logger.LogDebug("Requested user with email {0}", email);

            var request = from u in db.Users
                where u.Email == email
                join c in db.Contacts on u.ContactInfo.Id equals c.Id into gj
                from p in gj.DefaultIfEmpty()
                select new UserInfo
                {
                    User = u,
                    ContactInfo = p
                };

            try
            {
                var res = request.SingleOrDefault();
                return res;
            }
            catch (InvalidOperationException)
            {
                logger.LogError("More than 1 user with email {0}", email);
            }

            return null;
        }

        /// <summary>
        /// Get users connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProject(Project project, int pageNo, int pageSize = 50)
        {
            logger.LogDebug("Requested users for project {0} page {1}", project.Id, pageNo);

            return (from user in db.Users
                    where user.Projects.Any(p => p.Id == project.Id)
                    select user).OrderBy(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}