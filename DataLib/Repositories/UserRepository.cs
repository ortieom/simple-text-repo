using DataLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(DbContext context) : base(context) { }

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

        public ICollection<User> GetUsersInProject(Project project, int pageIndex, int pageSize = 50) {
            return (from user in db.Users
                   where user.Projects.Any(p => p.Id == project.Id)
                   select user).OrderBy(c => c.Id)
                   .Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
