﻿using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace DataLib.Repositories {
    public class Repository<TEntity> : IRepository<TEntity> where TEntity : class {
        protected readonly Context db;
        private readonly DbSet<TEntity> _entities;
        
        
        public Repository(DbContext context) {
            db = (Context) context;
            _entities = db.Set<TEntity>();
        }

        public TEntity? Get(int id) {
            return _entities.Find(id);
        }

        public IEnumerable<TEntity> GetAll() {
            return _entities.ToList();
        }

        public IEnumerable<TEntity> Find(Expression<Func<TEntity, bool>> predicate) {
            return _entities.Where(predicate);
        }

        public TEntity? SingleOrDefault(Expression<Func<TEntity, bool>> predicate) {
            return _entities.SingleOrDefault(predicate);
        }

        public void Add(TEntity entity) {
            _entities.Add(entity);
        }

        public void AddRange(IEnumerable<TEntity> entities) {
            _entities.AddRange(entities);
        }

        public void Remove(TEntity entity) {
            _entities.Remove(entity);
        }

        public void RemoveRange(IEnumerable<TEntity> entities) {
            _entities.RemoveRange(entities);
        }
    }
}