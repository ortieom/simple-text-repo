using DataLib.Models;

namespace DataLib.Repositories {
    public interface IUserRepository : IRepository<User> {
        public UserInfo GetUserInfoById(int userId);
        public UserInfo? GetUserInfoByEmail(string email);
        public ICollection<User> GetUsersInProject(Project project, int pageIndex, int pageSize);
    }
}
