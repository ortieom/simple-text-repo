using DataLib;
using DataLib.Models;

namespace Services {
    public class DocumentService {
        private readonly IUnitOfWork _repos;

        public DocumentService(IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public Document CreateDocument(User user, Project project, String? title = null, String? description = null, String? text = null) {
            Document doc = new() { Project = project, Title = title, Description = description, Contents = text };
            _repos.Documents.Add(doc);
            project.Documents.Add(doc);
            return doc;
        }
    }
}
