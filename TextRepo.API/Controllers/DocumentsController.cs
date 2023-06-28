using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using TextRepo.API.DataTransferObjects;
using TextRepo.API.Tools;
using TextRepo.Commons.Models;
using TextRepo.API.Services;
namespace TextRepo.API.Controllers
{
    /// <summary>
    /// Controllers related to documents.
    /// </summary>
    [Route("[controller]")]
    public class DocumentsController : Controller
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly DocumentService _documentService;
        private readonly IMapper _mapper;

        /// <summary>
        /// Creates DocumentController
        /// </summary>
        /// <param name="userService"></param>
        /// <param name="projectService"></param>
        /// <param name="documentService"></param>
        /// <param name="mapper"></param>
        public DocumentsController(UserService userService, ProjectService projectService, 
            DocumentService documentService, IMapper mapper)
        {
            _userService = userService;
            _projectService = projectService;
            _documentService = documentService;
            _mapper = mapper;
        }

        /// <summary>
        /// Get document with corresponding id
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpGet]
        [Authorize]
        [Route("{documentId}")]
        public IActionResult GetDocumentInfo(int documentId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var document = _documentService.Get(documentId);
            if (!HasAccess(user, document))
            {
                return Unauthorized();
            }

            return Ok(_mapper.Map<DocumentResponseDto>(document));
        }
        
        /// <summary>
        /// Edit document
        /// </summary>
        /// <param name="documentId"></param>
        /// <param name="documentRequest"></param>
        /// <returns></returns>
        [HttpPut]
        [Authorize]
        [Route("{documentId}")]
        public IActionResult EditDocument(int documentId, DocumentRequestDto documentRequest)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var document = _documentService.Get(documentId);
            if (!HasAccess(user, document))
            {
                return Unauthorized();
            }

            var newDoc = _mapper.Map<Document>(documentRequest);

            _documentService.Edit(document!, newDoc);
            return Ok();
        }
        
        /// <summary>
        /// Remove document
        /// </summary>
        /// <param name="documentId"></param>
        /// <returns></returns>
        [HttpDelete]
        [Authorize]
        [Route("{documentId}")]
        public IActionResult DeleteProject(int documentId)
        {
            var identity = HttpContext.User.Identity as ClaimsIdentity;
            var user = TokenEntities.GetUser(identity, _userService);
            var document = _documentService.Get(documentId);
            if (!HasAccess(user, document))
            {
                return Unauthorized();
            }
            
            _documentService.Delete(document!);
            return Ok();
        }
                
        /// <summary>
        /// Check whether user can do this action with document
        /// </summary>
        /// <param name="user"></param>
        /// <param name="document"></param>
        /// <returns></returns>
        private bool HasAccess(User? user, Document? document)
        {
            if (user is null || document is null)
                return false;
            
            return _documentService.HasAccessToDocument(user, document);
        } 
    }
}