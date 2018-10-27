/// <summary>
/// Nintex.UrlShortener.DataAccess.Repository
/// </summary>
namespace Nintex.UrlShortener.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Threading.Tasks;

    /// <summary>
    /// Interface ReadOnlyRepository
    /// </summary>
    public interface IReadOnlyRepository
    {
        /// <summary>
        /// Get All
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="orderBy">Order By</param>
        /// <param name="includeProperties">include Properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">take number</param>
        /// <returns>List of Entity</returns>
        IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class;

        

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">Order By</param>
        /// <param name="includeProperties">include Properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">take number</param>
        /// <returns>List of Entity</returns>
        IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class;

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="includeProperties">include Properties</param>
        /// <returns>IQueryable list of Entity</returns>
        IQueryable<TEntity> GetAsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null) where TEntity : class;

        /// <summary>
        /// Get as Query able
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">Order By</param>
        /// <param name="includeProperties">include Properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">take number</param>
        /// <returns>List of Entity</returns>
        IQueryable<TEntity> GetAsQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class;

        
        /// <summary>
        /// Get One Entity
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="includeProperties">include Properties</param>
        /// <returns>One Entity</returns>
        TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = null)
            where TEntity : class;

        

        /// <summary>
        /// Get First Entity
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">order By</param>
        /// <param name="includeProperties">include Properties</param>
        /// <returns>First Entity</returns>
        TEntity GetFirst<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null)
            where TEntity : class;


        /// <summary>
        /// Get Entity By id
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="id">object id</param>
        /// <returns> Entity By id</returns>
        TEntity GetById<TEntity>(object id)
            where TEntity : class;


        /// <summary>
        /// Get Count
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <returns>number of entity</returns>
        int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        

        /// <summary>
        /// Get Exists
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <returns>check exist</returns>
        bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class;

        
    }
}
