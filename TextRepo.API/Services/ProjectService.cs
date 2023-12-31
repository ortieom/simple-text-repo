﻿using TextRepo.Commons.Models;
using TextRepo.DataAccessLayer.Repositories;

namespace TextRepo.API.Services
{
    /// <summary>
    /// Business logic layer for projects
    /// </summary>
    public class ProjectService
    {
        private readonly IProjectRepository _repo;
        
        /// <summary>
        /// Creates business logic layer for projects with reference to repository
        /// </summary>
        /// <param name="repo">Project repository</param>
        public ProjectService(IProjectRepository repo)
        {
            _repo = repo;
        }

        /// <summary>
        /// Check if user can do anything with this project
        /// </summary>
        /// <param name="user"></param>
        /// <param name="project"></param>
        /// <returns>true if user has access</returns>
        public bool HasAccessToProject(User user, Project project)
        {
            return _repo.ProjectContainUser(project, user);
        }

        /// <summary>
        /// Get project by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Project with corresponding id</returns>
        public Project? Get(int id)
        {
            return _repo.Get(id);
        }

        /// <summary>
        /// Create new project with optional parameters
        /// </summary>
        /// <param name="creator">First member of project</param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns>New Project object</returns>
        public Project CreateProject(User creator, string? name = null, string? description = null)
        {
            Project project = new() { Users = new List<User> { creator }, Name = name, Description = description };
            _repo.Add(project);
            _repo.Commit();
            return project;
        }

        /// <summary>
        /// Connect user with project
        /// </summary>
        /// <param name="user"></param>
        /// <param name="project"></param>
        /// <returns>Updated Project</returns>
        public void AddUserToProject(User user, Project project)
        {
            project.Users.Add(user);
            _repo.Commit();
        }

        /// <summary>
        /// Get projects connected with selected user (queried by pages)
        /// </summary>
        /// <param name="user"></param>
        /// <param name="pageNo">Number of requested page (start with 1)</param>
        /// <param name="pageSize">Count of objects on one page</param>
        /// <returns>User's projects on selected page</returns>
        public ICollection<Project> GetUserProjectsPaginated(User user, int pageNo, int pageSize)
        {
            return _repo.GetUserProjects(user, pageNo, pageSize);
        }

        /// <summary>
        /// Edit project with optional parameters.
        /// Provide only arguments whose columns must be updated
        /// </summary>
        /// <param name="oldProject"></param>
        /// <param name="newProject"></param>
        /// <returns>Updated Project</returns>
        public void Edit(Project oldProject, Project newProject)
        {
            _repo.Update(oldProject, newProject);
            _repo.Commit();
        }

        /// <summary>
        /// Delete project from storage
        /// </summary>
        /// <param name="project"></param>
        public void Delete(Project project)
        {
            _repo.Remove(project);
            _repo.Commit();
        }
    }
}