using DataLib;
using ConsoleApp.Models;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            using (Context db = new("storage.db")) {
                // temporary code
                User userTest = new() { Name = "User5", Email = "1mail@example.com", HashedPassword = "dfhjsklfhasduifu32e2d223213df13==" };
                db.Users.Add(userTest);
                db.SaveChanges();
                ContactInfo contactInfo = new() { User = userTest, Type = "tg", Value = "ortieom3" };
                db.Contacts.Add(contactInfo);
                db.SaveChanges();

                var users = db.Users;
                Console.WriteLine("Users:");
                foreach (User u in users) {
                    Console.WriteLine("{0}. {1} - {2}", u.Id, u.Name, u.Email);
                    if (u.ContactInfo != null) {
                        Console.WriteLine("contact {0}: {1}", u.ContactInfo.Type, u.ContactInfo.Value);
                    }
                }
            }
        }
    }
}