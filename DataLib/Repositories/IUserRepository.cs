using DataLib.Models;

namespace DataLib.Repositories {
    public interface IUserRepository : IRepository<User> {
        public UserInfo GetUserInfoById(ulong userId);
        public ICollection<User> GetUsersInProject(Project project, int pageIndex, int pageSize);
    }
}
