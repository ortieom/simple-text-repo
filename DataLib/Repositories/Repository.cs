using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLib.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        protected readonly Context db;
        private readonly DbSet<TEntity> _entities;
        
        
        public Repository(DbContext context) {
            db = (Context) context;
            _entities = db.Set<TEntity>();
        }


        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity with corresponding id</returns>
        public TEntity? Get(int id) {
            return _entities.Find(id);
        }

        /// <summary>
        /// Get all entities 
        /// </summary>
        /// <returns>All entities</returns>
        public IEnumerable<TEntity> GetAll() {
            return _entities.ToList();
        }

        /// <summary>
        /// Get all entities that satisfy predicate expression
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <returns>All satisfying entites</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return _entities.Where(predicate);
        }

        /// <summary>
        /// Get only one entity that satisfies predicate or defaul value if nothing found.
        /// Will throw error if more than one entity satisfies predicate.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <param name="predicate"></param>
        /// <returns>Entity that satisfies predicate or defaul value if nothing found</returns>
        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return _entities.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Add new entity to storage
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity) {
            _entities.Add(entity);
        }

        /// <summary>
        /// Add all entities of provided range to storage
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities) {
            _entities.AddRange(entities);
        }

        /// <summary>
        /// Remove entity from storage
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity) {
            _entities.Remove(entity);
        }

        /// <summary>
        /// Remove all entities of provided range from storage
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities) {
            _entities.RemoveRange(entities);
        }
    }
}