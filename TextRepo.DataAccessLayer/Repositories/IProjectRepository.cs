using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public interface IProjectRepository : IRepository<Project>
    {
        /// <summary>
        /// Get projects connected with selected user (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>User's projects on selected page</returns>
        public ICollection<Project> GetUserProjects(User user, int pageNo, int pageSize);
    }
}