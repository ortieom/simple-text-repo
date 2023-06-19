using DataLib.Models;

namespace DataLib.Repositories {
    public interface IDocumentRepository : IRepository<Document> {
        public ICollection<Document> GetDocumentsInProject(Project project, int pageIndex, int pageSize);
    }
}
