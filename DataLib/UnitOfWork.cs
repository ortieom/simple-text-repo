using DataLib.Repositories;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;

namespace DataLib
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly Context _db;
        private readonly ILogger _logger;

        public UnitOfWork(DbContext context, ILogger<UnitOfWork> logger,
            IUserRepository userRepository, IProjectRepository projectRepository,
            IDocumentRepository documentRepository)
        {
            _db = (Context)context;
            _logger = logger;
            Users = userRepository;
            Projects = projectRepository;
            Documents = documentRepository;
        }

        public IUserRepository Users { get; private set; }
        public IProjectRepository Projects { get; private set; }
        public IDocumentRepository Documents { get; private set; }

        /// <summary>
        /// Delete generic entity from storage
        /// </summary>
        /// <typeparam name="TEntity"></typeparam>
        /// <param name="entity"></param>
        public void Delete<TEntity>(TEntity entity)
        {
            _db.Remove(entity!);
            _db.SaveChanges();
        }

        /// <summary>
        /// Commit pending changes
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            _logger.LogDebug("Committed pending changes");
            return _db.SaveChanges();
        }

        /// <summary>
        /// Release allocated resources for db context
        /// </summary>
        public void Dispose()
        {
            _db.Dispose();
        }
    }
}