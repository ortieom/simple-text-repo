using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextRepo.API.Tools;
using TextRepo.Commons.Models;
using TextRepo.Services;
namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Controllers related to projects.
    /// </summary>
    [Route("project")]
    public class ProjectController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly DocumentService _documentService;
        
        /// <summary>
        /// Creates ProjectController
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="projectService"></param>
        /// <param name="documentService"></param>
        public ProjectController(UserService userService, ProjectService projectService, DocumentService documentService)
        {
            _userService = userService;
            _projectService = projectService;
            _documentService = documentService;
        }
        
        /// <summary>
        /// Check whether user can do this action with project
        /// </summary>
        /// <param name="user"></param>
        /// <param name="project"></param>
        /// <returns></returns>
        private bool HasAccess(User? user, Project? project)
        {
            if (user is null || project is null)
                return false;
            
            return _projectService.HasAccessToProject(user, project);
        } 
        
        /// <summary>
        /// Get information about project with corresponding id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{projectId}")]
        public IActionResult GetProjectInfo(int projectId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }
            
            if (project is null)
            {
                return NotFound();
            }

            return Ok(project);
        }
        
        /// <summary>
        /// Create new project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("create")]
        public IActionResult CreateProject()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.CreateProject(user!);
            return Ok(project.Id);
        }
        
        /// <summary>
        /// Edit project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="name"></param>
        /// <param name="description"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{projectId}/edit")]
        public IActionResult EditProject(int projectId, string? name = null, string? description = null)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }

            _projectService.Edit(project!, name, description);
            return Ok();
        }
        
        /// <summary>
        /// Grant user access to project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="addedUserId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{projectId}/adduser")]
        public IActionResult AddUser(int projectId, int addedUserId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }

            var addedUser = _userService.Get(addedUserId);
            if (addedUser is null)
            {
                return BadRequest("User with id " + addedUserId + " not found");
            }

            _projectService.AddUserToProject(addedUser, project!);
            return Ok();
        }
        
        /// <summary>
        /// Remove project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{projectId}")]
        public IActionResult DeleteProject(int projectId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }
            
            _projectService.Delete(project!);
            return Ok();
        }
        
        /// <summary>
        /// Get project's users by pages of size 50
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageNo">from 1</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{projectId}/users/{pageNo}")]
        public IActionResult ListUser(int projectId, int pageNo = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }

            return Ok(_userService.GetUsersInProjectPaginated(project!, pageNo, 50));
        }
        
        /// <summary>
        /// Get project's documents by pages of size 50
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageNo">from 1</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{projectId}/documents/{pageNo}")]
        public IActionResult ListDocuments(int projectId, int pageNo = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.getUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Unauthorized();
            }
            
            return Ok(_documentService.GetProjectDocumentsPaginated(project!, pageNo, 50));
        }

    }
}