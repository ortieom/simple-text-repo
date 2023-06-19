using DataLib.Repositories;
using Microsoft.EntityFrameworkCore;
using System.Data.Common;
using System.Reflection.Metadata;

namespace DataLib {
    internal class UnitOfWork : IUnitOfWork {
        private readonly Context _db;

        public UnitOfWork(DbContext context) {
            _db = (Context) context;
            Users = new UserRepository(_db);
            Projects = new ProjectRepository(_db);
            Documents = new DocumentRepository(_db);
        }

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IDocumentRepository Documents { get; private set; }

        public int Commit() {
            return _db.SaveChanges();
        }

        public void Dispose() {
            _db.Dispose();
        }
    }
}
