using ConsoleApp.Models;
using Microsoft.EntityFrameworkCore;

namespace ConsoleApp.Data {
    internal class Context : DbContext {
        public DbSet<User> Users { get; set; } = null!;
        public DbSet<Project> Projects { get; set; } = null!;
        public DbSet<Document> Documents { get; set; } = null!;
        public DbSet<ContactInfo> Contacts { get; set; } = null!;

        public string DbPath { get; }

        public Context() {
            var path = Environment.CurrentDirectory;
            DbPath = System.IO.Path.Join(path, "storage.db");

            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            => optionsBuilder.UseSqlite($"Data Source={DbPath}");
    }
}
