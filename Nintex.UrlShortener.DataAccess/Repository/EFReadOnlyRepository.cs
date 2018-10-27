/// <summary>
/// Nintex.UrlShortener.DataAccess
/// </summary>
namespace Nintex.UrlShortener.DataAccess
{
    using System;
    using System.Collections.Generic;
    using System.Data.Entity;
    using System.Linq;
    using System.Linq.Expressions;

    /// <summary>
    /// EFReadOnly Repository
    /// </summary>
    /// <typeparam name="TContext">T Context param</typeparam>
    public class EFReadOnlyRepository<TContext> : IReadOnlyRepository
        where TContext : DbContext
    {
        #region IReadOnlyRepository Members
        /// <summary>
        /// T Context
        /// </summary>
        protected readonly TContext Context;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFReadOnlyRepository{TContext}"/> class.
        /// </summary>
        /// <param name="context">T Context</param>
        public EFReadOnlyRepository(TContext context)
        {
            this.Context = context;
        }

        /// <summary>
        /// Get all
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">task number</param>
        /// <returns>All Entity</returns>
        public virtual IEnumerable<TEntity> GetAll<TEntity>(
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(null, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">task number</param>
        /// <returns>virtual entity</returns>
        public virtual IEnumerable<TEntity> Get<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take).ToList();
        }

        /// <summary>
        /// Get Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns>IQueryable list of Entity</returns>
        public virtual IQueryable<TEntity> GetAsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter = null, string includeProperties = null) where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter, null, includeProperties, null, null);
        }

        /// <summary>
        /// Get query able
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">task number</param>
        /// <returns>Query able Entity</returns>
        IQueryable<TEntity> IReadOnlyRepository.GetAsQueryable<TEntity>(Expression<Func<TEntity, bool>> filter, Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy, string includeProperties, int? skip, int? take)
        {
            return this.GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Get Query able Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">task number</param>
        /// <returns>Query able Entity</returns>
        public virtual IEnumerable<TEntity> GetAsQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter, orderBy, includeProperties, skip, take);
        }

        /// <summary>
        /// Get One
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns>One Entity</returns>
        public virtual TEntity GetOne<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            string includeProperties = "")
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter, null, includeProperties).SingleOrDefault();
        }

       
        /// <summary>
        /// Get First
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">Order By</param>
        /// <param name="includeProperties">include properties</param>
        /// <returns>First Entity</returns>
        public virtual TEntity GetFirst<TEntity>(
           Expression<Func<TEntity, bool>> filter = null,
           Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
           string includeProperties = "")
           where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter, orderBy, includeProperties).FirstOrDefault();
        }


        /// <summary>
        /// Get Entity by id
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="id">object id</param>
        /// <returns>Entity by id</returns>
        public virtual TEntity GetById<TEntity>(object id)
            where TEntity : class
        {
            return this.Context.Set<TEntity>().Find(id);
        }

        /// <summary>
        /// Get Count
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <returns>number of entity</returns>
        public virtual int GetCount<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter).Count();
        }

        /// <summary>
        /// Get Exist
        /// </summary>
        /// <typeparam name="TEntity">Entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <returns>Exist or not</returns>
        public virtual bool GetExists<TEntity>(Expression<Func<TEntity, bool>> filter = null)
            where TEntity : class
        {
            return this.GetQueryable<TEntity>(filter).Any();
        }

        /// <summary>
        /// Get Query able
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="filter">filter model</param>
        /// <param name="orderBy">order by</param>
        /// <param name="includeProperties">include properties</param>
        /// <param name="skip">skip number</param>
        /// <param name="take">task number</param>
        /// <returns>Virtual query able</returns>
        protected virtual IQueryable<TEntity> GetQueryable<TEntity>(
            Expression<Func<TEntity, bool>> filter = null,
            Func<IQueryable<TEntity>, IOrderedQueryable<TEntity>> orderBy = null,
            string includeProperties = null,
            int? skip = null,
            int? take = null)
            where TEntity : class
        {
            includeProperties = includeProperties ?? string.Empty;
            IQueryable<TEntity> query = this.Context.Set<TEntity>();

            if (filter != null)
            {
                query = query.Where(filter);
            }

            foreach (var includeProperty in includeProperties.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                query = query.Include(includeProperty);
            }

            if (orderBy != null)
            {
                query = orderBy(query);
            }

            if (skip.HasValue)
            {
                query = query.Skip(skip.Value);
            }

            if (take.HasValue)
            {
                query = query.Take(take.Value);
            }

            return query;
        }

        #endregion
    }
}
