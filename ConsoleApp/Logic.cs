using TextRepo.DataAccessLayer.Models;
using TextRepo.Services;

namespace TextRepo.MainApp
{
    internal class Logic
    {
        private readonly UserService _userService;
        private readonly ProjectService _projectService;
        private readonly DocumentService _docService;
        private readonly ContactService _contactService;

        public Logic(UserService us, ProjectService ps, DocumentService ds, ContactService cs)
        {
            _userService = us;
            _projectService = ps;
            _docService = ds;
            _contactService = cs;
        }

        /// <summary>
        /// Some temporary code example
        /// </summary>
        public void TestRun()
        {
            // temporary code
            User user1 = _userService.CreateUser("Artyom", "m@ex.com", "admin");
            User user2 = _userService.CreateUser("Test", "m2@ex.com", "admin");

            _userService.AddContactInfo(user1, "tg", "tt");
            _contactService.EditContact(user1, type: "vk");

            _userService.Edit(user2, name: "edit test");

            User? aUser = _userService.GetUser("m@ex.com", "admin");

            if (aUser is null)
            {
                throw new Exception("user not found");
            }
            
            Project project = _projectService.CreateProject(aUser, description: "test desc");

            _projectService.AddUserToProject(user2, project);

            _projectService.Edit(_projectService.CreateProject(aUser, name: "name"), description: "hey!");

            foreach (Project p in _projectService.GetUserProjectsPaginated(aUser, 1, 10000))
            {
                Console.WriteLine("Project #{0} {1} ({2}) contains users", p.Id, p.Name, p.Description);
                foreach (User u in _userService.GetUsersInProjectPaginated(p, 1, 10000))
                {
                    Console.WriteLine("{0}. {1} {2} {3} (contact {4}: {5})", u.Id, u.Name, u.Surname ?? "", u.Email,
                        u.ContactInfo?.Type ?? "", u.ContactInfo?.Value ?? "");
                }
            }

            _docService.CreateDocument(project, text: "новый документ");
            _docService.Delete(_docService.CreateDocument(project, text: "2новый документ"));
            _docService.Edit(_docService.CreateDocument(project, text: "3новый документ"), text: "Обновлённый текст");

            _projectService.Delete(project);

            _userService.Delete(user1);
            _userService.Delete(user2);
        }
    }
}