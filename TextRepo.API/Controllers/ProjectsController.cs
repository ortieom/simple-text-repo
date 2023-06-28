using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextRepo.API.Tools;
using TextRepo.Commons.Models;
using TextRepo.API.Services;
using TextRepo.API.DataTransferObjects;

namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Controllers related to projects.
    /// </summary>
    [Route("[controller]")]
    public class ProjectsController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly DocumentService _documentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates ProjectController
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="projectService"></param>
        /// <param name="documentService"></param>
        /// <param name="mapper"></param>
        public ProjectsController(UserService userService, ProjectService projectService, 
            DocumentService documentService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _documentService = documentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get information about project with corresponding id
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{projectId}")]
        [ProducesResponseType(typeof(ProjectResponseDto), 200)]
        public IActionResult GetProjectInfo(int projectId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
            }
            
            if (project is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<ProjectResponseDto>(project));
        }
        
        /// <summary>
        /// Create new project
        /// </summary>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("create")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult CreateProject()
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.CreateProject(user!);
            return Ok(project.Id);
        }
        
        /// <summary>
        /// Edit project
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="projectRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{projectId}")]
        public IActionResult EditProject(int projectId, ProjectRequestDto projectRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
            }

            var newProject = _mapper.Map<Project>(projectRequest);

            _projectService.Edit(project!, newProject);
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
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
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
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
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
        [ProducesResponseType(typeof(List<UserResponseDto>), 200)]
        public IActionResult ListUser(int projectId, int pageNo = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
            }

            return Ok(_userService.GetUsersInProjectPaginated(project!, pageNo, 50)
                .Select(u => _mapper.Map<UserResponseDto>(u))
                .ToList());
        }
        
        /// <summary>
        /// Create new document in project
        /// </summary>
        /// <param name="projectId"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{projectId}/adddocument")]
        [ProducesResponseType(typeof(int), 200)]
        public IActionResult CreateDocument(int projectId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
            }

            var doc = _documentService.CreateDocument(project!);
            return Ok(doc.Id);
        }
        
        /// <summary>
        /// Get project's document's id by pages of size 50
        /// </summary>
        /// <param name="projectId"></param>
        /// <param name="pageNo">from 1</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{projectId}/documents/{pageNo}")]
        [ProducesResponseType(typeof(List<int>), 200)]
        public IActionResult ListDocuments(int projectId, int pageNo = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var project = _projectService.Get(projectId);
            if (!HasAccess(user, project))
            {
                return Forbid();
            }

            var result = _documentService
                .GetProjectDocumentsPaginated(project!, pageNo, 50)
                .Select(d => d.Id).ToList();
            
            return Ok(result);
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
    }
}