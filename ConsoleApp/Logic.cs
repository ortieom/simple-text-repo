using DataLib.Models;
using Services;

namespace MainApp {
    internal class Logic {
        private readonly UserService userService;
        private readonly ProjectService projectService;
        private readonly DocumentService docService;

        public Logic(UserService us, ProjectService ps, DocumentService ds) {
            userService = us;
            projectService = ps;
            docService = ds;
        }

        /// <summary>
        /// Some temporary code example
        /// </summary>
        public void TestRun() {
            // temporary code
            User user1 = userService.CreateUser("Artyom", "m@ex.com", "admin");
            User user2 = userService.CreateUser("Test", "m2@ex.com", "admin");

            userService.AddContactInfo(user1, "tg", "tt");
            userService.EditContact(user1, type: "vk");

            userService.Edit(user2, name: "edit test");

            User? aUser = userService.GetUser("m@ex.com", "admin");

            Project project = projectService.CreateProject(aUser, description: "test desc");

            projectService.AddUserToProject(user2, project);

            projectService.Edit(projectService.CreateProject(aUser, name: "name"), description: "hey!");

            foreach (Project p in projectService.GetUserProjectsPaginated(aUser, 1, 10000)) {
                Console.WriteLine("Project #{0} {1} ({2}) contains users", p.Id, p.Name, p.Description);
                foreach (User u in userService.GetUsersInProjectPaginated(p, 1, 10000)) {
                    Console.WriteLine("{0}. {1} {2} {3} (contact {4}: {5})", u.Id, u.Name, u.Surname ?? "", u.Email, 
                        u.ContactInfo?.Type ?? "", u.ContactInfo?.Value ?? "");
                }
            }

            docService.CreateDocument(project, text: "новый документ");
            docService.Delete(docService.CreateDocument(project, text: "2новый документ"));
            docService.Edit(docService.CreateDocument(project, text: "3новый документ"), text: "Обновлённый текст");

            projectService.Delete(project);

            userService.Delete(user1);
            userService.Delete(user2);
        }
    }
}
