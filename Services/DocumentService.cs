using DataLib;
using DataLib.Models;

namespace Services {
    public class DocumentService {
        private readonly IUnitOfWork _repos;

        public DocumentService(IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public Boolean HasAccesToDocument(User user, Document document) {
            return document.Project.Users.Contains(user);
        }

        public Document? Get(int id) {
            return _repos.Documents.Get(id);
        }

        public Document CreateDocument(Project project, String? title = null, String? description = null, String? text = null) {
            Document doc = new() { Project = project, Title = title, Description = description, Contents = text };
            _repos.Documents.Add(doc);
            project.Documents.Add(doc);
            _repos.Commit();
            return doc;
        }


        public void Delete(Document document) {
            _repos.Delete(document);
        }

        public Document Edit(Document doc, String? title = null, String? description = null, String? text = null) {
            doc.Title = title ?? doc.Title;
            doc.Description = description ?? doc.Description;
            doc.Contents = text ?? doc.Contents;
            _repos.Commit();
            return doc;
        }
    }
}
