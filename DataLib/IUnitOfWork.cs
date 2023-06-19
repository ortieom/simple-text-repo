using DataLib.Repositories;

namespace DataLib {
    public interface IUnitOfWork : IDisposable {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IDocumentRepository Documents { get; }

        int Commit();
    }
}