using Microsoft.EntityFrameworkCore;
using TextRepo.Commons.Models;

namespace TextRepo.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents data access layer for Document
    /// </summary>
    public class DocumentRepository : Repository<Document>, IDocumentRepository
    {
        /// <summary>
        /// Creates DocumentRepository with basic Repository methods
        /// </summary>
        /// <param name="context"></param>
        public DocumentRepository(DbContext context) : base(context)
        {
        }

        /// <summary>
        /// Gets documents connected with selected project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>Project documents in selected page</returns>
        public ICollection<Document> GetDocumentsInProject(Project project, int pageNo, int pageSize = 50)
        {
            return db.Documents
                .Where(d => d.Project == project)
                .OrderBy(d => d.Id)
                .Skip((pageNo - 1) * pageSize)
                .Take(pageSize)
                .ToList();
        }
        
        /// <summary>
        /// Check whether user has access to the document
        /// </summary>
        /// <param name="document"></param>
        /// <param name="user"></param>
        /// <returns></returns>
        public bool DocumentAccessibleToUser(Document document, User user)
        {
            return db.Projects
                .Where(p =>
                    p.Users.Any(u => u.Id == user.Id))
                .Where(p => p.Id == document.ProjectId)
                .ToList().Count != 0;
        }
    }
}