using DataLib.Models;

namespace DataLib.Repositories {
    public interface IProjectRepository : IRepository<Project> {
        public ICollection<Project> GetUserProjects(User user, int pageIndex, int pageSize);
    }
}
