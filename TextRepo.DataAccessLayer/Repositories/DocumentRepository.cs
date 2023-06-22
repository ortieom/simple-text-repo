using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using TextRepo.DataAccessLayer.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        public DocumentRepository(DbContext context, ILogger<DocumentRepository> someLogger) : base(context, someLogger)
        {
        }

        /// <summary>
        /// Get documents connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Project documents in selected page</returns>
        public ICollection<Document> GetDocumentsInProject(Project project, int pageNo, int pageSize = 50)
        {
            logger.LogDebug("Requested documents from project {0} page {1}", project.Id, pageNo);

            return (from doc in db.Documents.Include(p => p.Project)
                    where doc.Project.Id == project.Id
                    select doc).OrderBy(c => c.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
    }
}