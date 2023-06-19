using DataLib.Models;

namespace DataLib.Repositories {
    public class ProjectRepository : Repository<Project>, IProjectRepository {
        public ProjectRepository(Context context) : base(context) { }

        public ICollection<Project> GetUserProjects(User user, int pageIndex, int pageSize = 50) {
            return (from project in db.Projects
                   where project.Users.Any(u => u.Id == user.Id)
                   select project).OrderBy(c => c.Id)
                   .Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
