using DataLib.Models;

namespace DataLib.Repositories {
    public interface IDocumentRepository : IRepository<Document> {
        /// <summary>
        /// Get documents connected with selected project (quried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (strat with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Project documents in selected page</returns>
        public ICollection<Document> GetDocumentsInProject(Project project, int pageNo, int pageSize);
    }
}
