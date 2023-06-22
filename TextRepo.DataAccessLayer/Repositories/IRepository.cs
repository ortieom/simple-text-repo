using System.Linq.Expressions;

namespace TextRepo.DataAccessLayer.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        /// <summary>
        /// Get entity by id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Entity with corresponding id</returns>
        TEntity? Get(int id);

        /// <summary>
        /// Get all entities 
        /// </summary>
        /// <returns>All entities</returns>
        IEnumerable<TEntity> GetAll();

        /// <summary>
        /// Get all entities that satisfy predicate expression
        /// </summary>
        /// <param name="predicate">lambda expression</param>
        /// <returns>All satisfying entities</returns>
        IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Add new entity to storage
        /// </summary>
        /// <param name="entity"></param>
        void Add(TEntity entity);

        /// <summary>
        /// Add all entities of provided range to storage
        /// </summary>
        /// <param name="entities"></param>
        void AddRange(IEnumerable<TEntity> entities);

        /// <summary>
        /// Remove entity from storage
        /// </summary>
        /// <param name="entity"></param>
        void Remove(TEntity entity);

        /// <summary>
        /// Remove all entities of provided range from storage
        /// </summary>
        /// <param name="entities"></param>
        void RemoveRange(IEnumerable<TEntity> entities);
    }
}