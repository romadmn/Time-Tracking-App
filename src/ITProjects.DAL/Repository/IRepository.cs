﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using ITProjects.DAL.Entities;

namespace ITProjects.DAL.Repository
{
    public interface IRepository<TEntity> where TEntity : IEntityBase
    {
        /// <summary>
        /// Enables further filtering by returning IQueryable
        /// </summary>
        /// <returns>entity framework's entity</returns>
        IQueryable<TEntity> GetAll();

        /// <summary>
        /// Finds and returns TEntity based on the predicate
        /// </summary>
        /// <param name="predicate">customized condition</param>
        /// <returns>entity framework's entity</returns>
        Task<TEntity> FindByCondition(Expression<Func<TEntity, bool>> predicate);

        /// <summary>
        /// Adds entity into DBContext
        /// </summary>
        /// <param name="entity">entity framework's entity</param>
        void Add(TEntity entity);
        
        /// <summary>
        /// Removes entities from DBContext
        /// </summary>
        /// <param name="entity">entity framework's entity</param>
        void Remove(TEntity entity);
        
        /// <summary>
        /// Updates existing entity or creates a new one
        /// </summary>
        /// <param name="entity">entity framework's entity</param>
        void Update(TEntity entity);
        
        /// <summary>
        /// Save changes into the database
        /// </summary>
        Task<int> SaveChangesAsync();
    }
}
