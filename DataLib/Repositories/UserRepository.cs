using DataLib.Models;

namespace DataLib.Repositories {
    public class UserRepository : Repository<User>, IUserRepository {
        public UserRepository(Context context) : base(context) { }

        public UserInfo GetUserInfoById(ulong userId) {
            return db.Users.Join(db.Contacts,
                u => u.ContactInfo!.Id,
                c => c.Id,
                (u, c) => new UserInfo {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    HashedPassword = u.HashedPassword,
                    Projects = u.Projects,
                    ContactType = c.Type,
                    ContactValue = c.Value
                })
                .Where(x => x.Id == userId).First();
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
