using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextRepo.API.DataTransferObjects;
using TextRepo.Commons.Models;
using TextRepo.API.Services;
namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Controllers related to users.
    /// </summary>
    [Route("[controller]")]
    public class UsersController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly ContactService _contactService;
        private readonly IMapper _mapper;
        
        /// <summary>
        /// Creates UserController
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="projectService"></param>
        /// <param name="mapper"></param>
        public UsersController(UserService userService, ProjectService projectService, 
            ContactService contactService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _contactService = contactService;
            _mapper = mapper;
        }

        /// <summary>
        /// Gets information about user with corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{userId}")]
        [ProducesResponseType(typeof(UserResponseDto), 200)]
        public IActionResult GetUserInfo(int userId)
        {
            var user = _userService.Get(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(_mapper.Map<UserResponseDto>(user));
        }
        
        /// <summary>
        /// Add contact information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="contactInfo"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{userId}/contact")]
        public IActionResult SetUserContact(int userId, ContactInfoRequestDto contactInfo)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Forbid();
            }

            if (user!.ContactInfo is not null)
            {
                _contactService.DeleteContact(user);
            }
            
            _userService.AddContactInfo(user!, _mapper.Map<ContactInfo>(contactInfo));
            return Ok();
        }
        
        /// <summary>
        /// Remove user's contact
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{userId}/contact")]
        public IActionResult DeleteContact(int userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Forbid();
            }
            
            _contactService.DeleteContact(user!);
            return Ok();
        }
        
        /// <summary>
        /// Edit user's main data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="userRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{userId}")]
        public IActionResult EditUser(int userId, UserRequestDto userRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Forbid();
            }

            var updatedUser = _mapper.Map<User>(userRequest);
            updatedUser.Id = user!.Id;

            _userService.Edit(user!, updatedUser);
            return Ok();
        }
        
        /// <summary>
        /// Remove user
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{userId}")]
        public IActionResult DeleteUser(int userId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Forbid();
            }
            
            _userService.Delete(user!);
            return Ok();
        }

        /// <summary>
        /// Get user's projects by pages of size 50
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNo">from 1</param>
        /// <param name="pageSize"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{userId}/projects/{pageNo}")]
        [ProducesResponseType(typeof(List<ProjectResponseDto>), 200)]
        public IActionResult ListProjects(int userId, int pageNo = 1, int pageSize = 50)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Forbid();
            }

            return Ok(
                _projectService.GetUserProjectsPaginated(user!, pageNo, pageSize)
                    .Select(p => _mapper.Map<ProjectResponseDto>(p))
                    .ToList());
        }
        
        /// <summary>
        /// Check whether performer of the action is actually itself
        /// </summary>
        /// <param name="identity"></param>
        /// <param name="target"></param>
        /// <returns></returns>
        private bool HasAccess(ClaimsIdentity? identity, User? target)
        {
            if (identity is null || target is null)
                return false;
            
            IEnumerable<Claim> claim = identity.Claims; 

            var userEmailClaim = claim
                .FirstOrDefault(x => x.Type == ClaimTypes.Email);

            if (userEmailClaim is null)
                return false;
            
            return userEmailClaim.Value == target.Email;
        } 
    }
}