using DataLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib.Repositories {
    public class DocumentRepository : Repository<Document>, IDocumentRepository {
        public DocumentRepository(DbContext context) : base(context) { }

        /// <summary>
        /// Get documents connected with selected project (quried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (strat with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Project documents in selected page</returns>
        public ICollection<Document> GetDocumentsInProject(Project project, int pageNo, int pageSize = 50) {
            return (from doc in db.Documents.Include(p => p.Project)
                    where doc.Project.Id == project.Id
                    select doc).OrderBy(c => c.Id)
                   .Skip((pageNo - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
