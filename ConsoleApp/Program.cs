using ConsoleApp.Data;
using ConsoleApp.Models;

namespace ConsoleApp {
    class Program {
        static void Main(string[] args) {
            using(Context db = new()) {
                // temporary code
                User userTest = new() { Name = "User1", Email = "mail@example.com", HashedPassword = "dfhjsklfhasduifu32e2d3213df13=="};
                db.Users.Add(userTest);
                db.SaveChanges();

                var users = db.Users;
                Console.WriteLine("Users:");
                foreach (User u in users) {
                    Console.WriteLine("{0}. {1} - {2}", u.Id, u.Name, u.Email);
                }
            }
        }
    }
}