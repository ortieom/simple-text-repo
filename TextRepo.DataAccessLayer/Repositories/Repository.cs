using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace TextRepo.DataAccessLayer.Repositories
{
    /// <summary>
    /// Represents basic data access layer for all models
    /// </summary>
    /// <typeparam name="TEntity"></typeparam>
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        protected readonly Context Db;
        private readonly DbSet<TEntity> _entities;

        /// <summary>
        /// Creates base class with common methods for all repositories
        /// </summary>
        /// <param name="context"></param>
        public Repository(DbContext context)
        {
            Db = (Context)context;
            _entities = Db.Set<TEntity>();
        }

        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity with corresponding id</returns>
        public TEntity? Get(int id)
        {
            return _entities.Find(id);
        }

        /// <summary>
        /// Get all entities 
        /// </summary>
        /// <returns>All entities</returns>
        public IEnumerable<TEntity> GetAll()
        {
            return _entities.ToList();
        }

        /// <summary>
        /// Get all entities that satisfy predicate expression
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <returns>All satisfying entities</returns>
        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.Where(predicate);
        }

        /// <summary>
        /// Get only one entity that satisfies predicate or defaul value if nothing found.
        /// Will throw error if more than one entity satisfies predicate.
        /// </summary>
        /// <exception cref="InvalidOperationException"></exception>
        /// <param name="predicate"></param>
        /// <returns>Entity that satisfies predicate or default value if nothing found</returns>
        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate)
        {
            return _entities.SingleOrDefault(predicate);
        }

        /// <summary>
        /// Add new entity to storage
        /// </summary>
        /// <param name="entity"></param>
        public void Add(TEntity entity)
        {
            _entities.Add(entity);
        }

        /// <summary>
        /// Add all entities of provided range to storage
        /// </summary>
        /// <param name="entities"></param>
        public void AddRange(IEnumerable<TEntity> entities)
        {
            _entities.AddRange(entities);
        }

        /// <summary>
        /// Remove entity from storage
        /// </summary>
        /// <param name="entity"></param>
        public void Remove(TEntity entity)
        {
            _entities.Remove(entity);
        }

        /// <summary>
        /// Remove all entities of provided range from storage
        /// </summary>
        /// <param name="entities"></param>
        public void RemoveRange(IEnumerable<TEntity> entities)
        {
            _entities.RemoveRange(entities);
        }
        
        /// <summary>
        /// Update old entity in storage with new
        /// </summary>
        /// <param name="oldEntity"></param>
        /// <param name="newEntity"></param>
        public void Update(TEntity oldEntity, TEntity newEntity)
        {
            // iterating over all properties and updating non-null ones
            foreach(var toProp in typeof(TEntity).GetProperties())
            {
                var fromProp= typeof(TEntity).GetProperty(toProp.Name);
                var toValue = fromProp!.GetValue(newEntity, null);
                if (toValue != null)
                {
                    toProp.SetValue(oldEntity, toValue, null);
                }
            }
        }

        /// <summary>
        /// Commit pending changes
        /// </summary>
        /// <returns></returns>
        public int Commit()
        {
            return Db.SaveChanges();
        }
    }
}