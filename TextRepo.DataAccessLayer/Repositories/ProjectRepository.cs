using Microsoft.Extensions.Logging;
using Microsoft.EntityFrameworkCore;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents data access layer for Project
    /// </summary>
    public class ProjectRepository : Repository<Project>, IProjectRepository
    {
        /// <summary>
        /// Creates DocumentRepository with basic Repository methods
        /// </summary>
        /// <param name="context"></param>
        public ProjectRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Get projects connected with selected user (queried by pages)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>User's projects on selected page</returns>
        public ICollection<Project> GetUserProjects(User user, int pageNo, int pageSize = 50)
        {
            return db.Projects
                .Where(p => p.Users.Any(u => u.Id == user.Id))
                .OrderBy(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        /// <summary>
        /// Check whether user has access to the project
        /// </summary>
        /// <param name="project"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool ProjectContainUser(Project project, User user)
        {
            return db.Projects
                .Where(p =>
                    p.Users.Any(u => u.Id == user.Id))
                .Where(p => p.Id == project.Id)
                .ToList().Count != 0;
        }
    }
}