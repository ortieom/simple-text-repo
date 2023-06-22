using DataLib.Repositories;

namespace DataLib
{
    public interface IUnitOfWork : IDisposable
    {
        IUserRepository Users { get; }
        IProjectRepository Projects { get; }
        IDocumentRepository Documents { get; }

        /// <summary>
        /// Delete generic entity from storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Delete<TEntity>(TEntity entity);

        /// <summary>
        /// Commit pending changes
        /// </summary>
        /// <returns></returns>
        int Commit();
    }
}