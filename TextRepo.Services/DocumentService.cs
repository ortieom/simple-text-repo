using TextRepo.Commons.Models;
using TextRepo.DataAccessLayer.Repositories;

namespace TextRepo.Services
{
    /// <summary>
    /// Business logic layer for documents
    /// </summary>
    public class DocumentService
    {
        private readonly IDocumentRepository _repo;
        
        /// <summary>
        /// Creates business logic layer for documents with reference to repository
        /// </summary>
        /// <param name="repo">Document repository</param>
        public DocumentService(IDocumentRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Check if user can do anything with this document
        /// </summary>
        /// <param name="user"></param>
        /// <param name="document"></param>
        /// <returns>true if user has access</returns>
        public bool HasAccessToDocument(User user, Document document)
        {
            return _repo.DocumentAccessibleToUser(document, user);
        }

        /// <summary>
        /// Get document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document with corresponding id</returns>
        public Document? Get(int id)
        {
            return _repo.Get(id);
        }

        /// <summary>
        /// Create new document in project with optional parameters
        /// </summary>
        /// <param name="project">Project where document belongs</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="text">Contents of the new document</param>
        /// <returns>New Document object</returns>
        public Document CreateDocument(Project project, string? title = null, string? description = null,
            string? text = null)
        {
            Document doc = new() { Project = project, Title = title, Description = description, Contents = text };
            _repo.Add(doc);
            project.Documents.Add(doc);
            _repo.Commit();
            return doc;
        }

        /// <summary>
        /// Delete document from storage
        /// </summary>
        /// <param name="document"></param>
        public void Delete(Document document)
        {
            _repo.Remove(document);
            _repo.Commit();
        }

        /// <summary>
        /// Edit document with optional parameters.
        /// Provide only arguments whose columns must be updated
        /// </summary>
        /// <param name="doc"></param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="text"></param>
        /// <returns>Updated Document</returns>
        public Document Edit(Document doc, string? title = null, string? description = null, string? text = null)
        {
            doc.Title = title ?? doc.Title;
            doc.Description = description ?? doc.Description;
            doc.Contents = text ?? doc.Contents;
            _repo.Commit();
            return doc;
        }
        
        /// <summary>
        /// Get documents connected with project (queried by pages)
        /// </summary>
        /// <param name="project"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>User's projects on selected page</returns>
        public ICollection<Document> GetProjectDocumentsPaginated(Project project, int pageNo, int pageSize)
        {
            return _repo.GetDocumentsInProject(project, pageNo, pageSize);
        }
    }
}