using TextRepo.DataAccessLayer;
using TextRepo.DataAccessLayer.Models;
using Microsoft.Extensions.Logging;

namespace TextRepo.Services
{
    public class DocumentService
    {
        private readonly IUnitOfWork _repos;
        private readonly ILogger _logger;

        public DocumentService(IUnitOfWork unitOfWork, ILogger<DocumentService> logger)
        {
            _repos = unitOfWork;
            _logger = logger;
        }

        /// <summary>
        /// Check if user can do anything with this document
        /// </summary>
        /// <param name="user"></param>
        /// <param name="document"></param>
        /// <returns>true if user has access</returns>
        public Boolean HasAccessToDocument(User user, Document document)
        {
            return document.Project.Users.Contains(user);
        }

        /// <summary>
        /// Get document by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Document with corresponding id</returns>
        public Document? Get(int id)
        {
            return _repos.Documents.Get(id);
        }

        /// <summary>
        /// Create new document in project with optional parameters
        /// </summary>
        /// <param name="project">Project where document belongs</param>
        /// <param name="title"></param>
        /// <param name="description"></param>
        /// <param name="text">Contents of the new document</param>
        /// <returns>New Document object</returns>
        public Document CreateDocument(Project project, String? title = null, String? description = null,
            String? text = null)
        {
            Document doc = new() { Project = project, Title = title, Description = description, Contents = text };
            _repos.Documents.Add(doc);
            project.Documents.Add(doc);
            _repos.Commit();
            return doc;
        }

        /// <summary>
        /// Delete document from storage
        /// </summary>
        /// <param name="document"></param>
        public void Delete(Document document)
        {
            _repos.Delete(document);
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
        public Document Edit(Document doc, String? title = null, String? description = null, String? text = null)
        {
            doc.Title = title ?? doc.Title;
            doc.Description = description ?? doc.Description;
            doc.Contents = text ?? doc.Contents;
            _repos.Commit();
            return doc;
        }
    }
}