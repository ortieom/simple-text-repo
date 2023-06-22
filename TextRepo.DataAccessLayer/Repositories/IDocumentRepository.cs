using TextRepo.DataAccessLayer.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public interface IDocumentRepository : IRepository<Document>
    {
        /// <summary>
        /// Get documents connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Project documents in selected page</returns>
        public ICollection<Document> GetDocumentsInProject(Project project, int pageNo, int pageSize);
    }
}