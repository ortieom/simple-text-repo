using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextRepo.API.Responses;
using TextRepo.Commons.Models;
using TextRepo.Services;
namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Controllers related to users.
    /// </summary>
    [Route("user")]
    public class UserController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        
        /// <summary>
        /// Creates UserController
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="projectService"></param>
        public UserController(UserService userService, ProjectService projectService)
        {
            _userService = userService;
            _projectService = projectService;
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
        
        /// <summary>
        /// Gets information about user with corresponding id
        /// </summary>
        /// <param name="userId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{userId}")]
        public IActionResult GetUserInfo(int userId)
        {
            var user = _userService.Get(userId);

            if (user is null)
            {
                return NotFound();
            }

            return Ok(user);
        }
        
        /// <summary>
        /// Add contact information
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="type"></param>
        /// <param name="value"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{userId}/setcontact")]
        public IActionResult SetUserContact(int userId, string type, string value)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Unauthorized();
            }
            _userService.AddContactInfo(user!, type, value);
            return Ok();
        }
        
        /// <summary>
        /// Edit user's main data
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="name"></param>
        /// <param name="surname"></param>
        /// <param name="email"></param>
        /// <param name="password"></param>
        /// <returns></returns>
        [HttpPost]
        [Authorize]
        [Route("{userId}/edit")]
        public IActionResult EditResult(int userId, string? name = null, string? surname = null, string? email = null,
            string? password = null)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Unauthorized();
            }

            _userService.Edit(user!, name, surname, email, password);
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
                return Unauthorized();
            }
            
            _userService.Delete(user!);
            return Ok();
        }

        /// <summary>
        /// Get user's projects by pages of size 50
        /// </summary>
        /// <param name="userId"></param>
        /// <param name="pageNo">from 1</param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{userId}/projects/{pageNo}")]
        public IActionResult ListProjects(int userId, int pageNo = 1)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = _userService.Get(userId);
            if (!HasAccess(identity, user))
            {
                return Unauthorized();
            }

            return Ok(
                _projectService.GetUserProjectsPaginated(user!, pageNo, 50));
        }
    }
}