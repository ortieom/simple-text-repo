using DataLib.Models;
using Microsoft.EntityFrameworkCore;

namespace DataLib.Repositories {
    public class DocumentRepository : Repository<Document>, IDocumentRepository {
        public DocumentRepository(DbContext context) : base(context) { }

        public ICollection<Document> GetDocumentsInProject(Project project, int pageIndex, int pageSize = 50) {
            return (from doc in db.Documents.Include(p => p.Project)
                    where doc.Project.Id == project.Id
                    select doc).OrderBy(c => c.Id)
                   .Skip((pageIndex - 1) * pageSize)
                   .Take(pageSize)
                   .ToList();
        }
    }
}
