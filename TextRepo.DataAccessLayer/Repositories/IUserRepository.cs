using TextRepo.DataAccessLayer.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public interface IUserRepository : IRepository<User>
    {
        /// <summary>
        /// Get User and ContactInfo united by user id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoById(int userId);

        /// <summary>
        /// Get User and ContactInfo united by user email
        /// </summary>
        /// <param name="email"></param>
        /// <returns>UserInfo object</returns>
        public UserInfo? GetUserInfoByEmail(string email);

        /// <summary>
        /// Get users connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Users in project on selected page</returns>
        public ICollection<User> GetUsersInProject(Project project, int pageNo, int pageSize);
    }
}