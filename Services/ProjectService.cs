using DataLib;
using DataLib.Models;

namespace Services {
    public class ProjectService {
        private readonly IUnitOfWork _repos;

        public ProjectService (IUnitOfWork unitOfWork) {
            _repos = unitOfWork;
        }

        public Boolean HasAccesToProject(User user, Project project) {
            return project.Users.Contains(user);
        }

        public Project? Get(int id) {
            return _repos.Projects.Get(id);
        }

        public Project CreateProject(User creator, String? name = null, String? description = null) {
            Project project = new() { Users = new List<User> { creator }, Name = name, Description = description };
            _repos.Projects.Add(project);
            _repos.Commit();
            return project;
        }

        public Project AddUserToProject(User user, Project project) {
            project.Users.Add(user);
            _repos.Commit();
            return project;
        }

        public ICollection<Project> GetUserProjectsPaginated(User user, int pageIndex, int pageSize) {
            return _repos.Projects.GetUserProjects(user, pageIndex, pageSize);
        }

        public Project Edit(Project project, String? name = null, String? description = null) {
            project.Name = name ?? project.Name;
            project.Description = description ?? project.Description;

            _repos.Commit();
            return project;
        }

        public void Delete(Project project) {
            _repos.Delete(project);
        }
    }
}
