using BC = BCrypt.Net.BCrypt;
using DataLib.Models;
using DataLib;

namespace Services {
    internal class BusinessLogic {
        private readonly Requests req;

        public BusinessLogic(Requests requests) {
            req = requests;
        }

        public Boolean HasAccesToProject(User user, Project project) {
            return project.Users.Contains(user);
        }

        public Boolean HasAccesToDocument(User user, Document document) {
            return HasAccesToProject(user, document.Project);
        }

        public String HashPassword(String password) {
            return BC.HashPassword(password);
        }

        public Project CreateProject(User creator, String? name, String? description) {
            Project project = new() { Users = { creator }, Name = name, Description = description };
            req.AddProject(project);
            return project;
        }

        public Project AddUserToProject(User user, Project project) {
            project.Users.Add(user);
            req.Update(project);

            return project;
        }

        public Project UpdateProject(Project project, String? name, String? description) {
            project.Name = name ?? project.Name;
            project.Description = description ?? project.Description;

            req.Update(project);

            return project;
        }

        public Document CreateDocument(User user, Project project, String? title, String? description, String? text) {
            Document doc = new() { Project = project, Title = title, Description = description, Contents = text };

            req.AddDocument(doc);

            project.Documents.Add(doc);
            req.Update(project);

            return doc;
        }

        public Document UpdateDocument(Document document, String? title, String? description, String? contents, Project? project) {
            document.Title = title ?? document.Title;
            document.Description = description ?? document.Description;
            document.Contents = contents ?? document.Contents;
            document.Project = project ?? document.Project;

            req.Update(document);

            return document;
        }

        public User CreateUser(String name, String? surname, String email, String password) {
            String hashedPassword = HashPassword(password);
            User user = new() { Name = name, Surname = surname, Email = email, HashedPassword = hashedPassword };

            req.AddUser(user);

            return user;
        }

        public Boolean ValidatePassword(User user, String password) {
            return BC.Verify(password, user.HashedPassword); ;
        }

        public User UpdateUser(User user, String? name, String? surname, String? email, String? password) {
            user.Name = name ?? user.Name;
            user.Surname = surname ?? user.Surname;
            user.Email = email ?? user.Email;
            if (password != null) {
                user.HashedPassword = HashPassword(password);
            }

            req.Update(user);

            return user;
        }

        public ContactInfo AddContactInfo(User user, String type, String value) {
            ContactInfo contact = new() { Type = type, Value = value, User = user };

            req.AddContact(contact);

            return contact;
        }

        public ContactInfo UpdateContactInfo(ContactInfo contact, String? type, String? value) {
            contact.Type = type ?? contact.Type;
            contact.Value = value ?? contact.Value;

            req.Update(contact);

            return contact;
        }
    }
}
