using DataLib;
using DataLib.Models;
using DataLib.Repositories;

namespace Services {
    public class ProjectService {
        private readonly IUnitOfWork _repos;

        public ProjectService (IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public Project CreateProject(User creator, String? name = null, String? description = null) {
            Project project = new() { Users = { creator }, Name = name, Description = description };
            _repos.Projects.Add(project);
            return project;
        }

        public Project AddUserToProject(User user, Project project) {
            project.Users.Add(user);
            return project;
        }
    }
}
