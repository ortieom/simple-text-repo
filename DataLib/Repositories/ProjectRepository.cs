using DataLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib.Repositories {
    public class ProjectRepository : Repository<Project>, IProjectRepository {
        public ProjectRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Get projects connected with selected user (quried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (strat with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>User's projects on selected page</returns>
        public ICollection<Project> GetUserProjects(User user, int pageNo, int pageSize = 50) {
            return (from project in db.Projects
                    where project.Users.Any(u => u.Id == user.Id)
                    select project).OrderBy(c => c.Id)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
