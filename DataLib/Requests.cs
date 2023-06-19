using Microsoft.EntityFrameworkCore;
using ConsoleApp.Models;

namespace DataLib {
    public class Requests {
        internal Context db;

        public Requests(DbContext context) {
            db = (Context)context;
        }

        public UserInfo GetUserInfoById(ulong userId) {
            return db.Users.Join(db.Contacts,
                u => u.ContactInfo!.Id,
                c => c.Id,
                (u, c) => new UserInfo {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    HashedPassword = u.HashedPassword,
                    Projects = u.Projects,
                    ContactType = c.Type,
                    ContactValue = c.Value
                }).Where(x => x.Id == userId).First();
        }

        public UserInfo GetUserInfoByEmail(string email) {
            return db.Users.Join(db.Contacts,
                u => u.ContactInfo!.Id,
                c => c.Id,
                (u, c) => new UserInfo {
                    Id = u.Id,
                    Name = u.Name,
                    Surname = u.Surname,
                    Email = u.Email,
                    HashedPassword = u.HashedPassword,
                    Projects = u.Projects,
                    ContactType = c.Type,
                    ContactValue = c.Value
                }).Where(x => x.Email == email).First();
        }

        public Project GetProjectById(ulong projectId) {
            var projects = (from project in db.Projects where project.Id == projectId select project).ToList();
            return projects[0];
        }

        public Document GetDocumentById(ulong documentId) {
            var documents = (from doc in db.Documents where doc.Id == documentId select doc).ToList();
            return documents[0];
        }

        public List<Project> GetUserProjects(User user) {
            return (from project in db.Projects
                    where project.Users.Any(u => u.Id == user.Id)
                    select project).ToList();
        }

        public List<User> GetProjectUsers(Project project) {
            return (from user in db.Users
                    where user.Projects.Any(p => p.Id == project.Id)
                    select user).ToList();
        }

        public List<Document> GetProjectDocuments(Project project) {
            return (from doc in db.Documents.Include(p => p.Project)
                    where doc.Project.Id == project.Id
                    select doc).ToList();
        }
    }
}
