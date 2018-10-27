/// <summary>
/// Nintex.UrlShortener.DataAccess
/// </summary>
namespace Nintex.UrlShortener.DataAccess
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Infrastructure;
    using System.Data.Entity.Validation;
    using System.Linq;

    /// <summary>
    /// EF Repository
    /// </summary>
    /// <typeparam name="TContext">T Context param</typeparam>
    public class EFRepository<TContext> : EFReadOnlyRepository<TContext>, IRepository, IDisposable
        where TContext : DbContext
    {
        /// <summary>
        /// disposed variable
        /// </summary>
        private bool disposed = false;

        /// <summary>
        /// Initializes a new instance of the <see cref="EFRepository{TContext}"/> class.
        /// </summary>
        /// <param name="context">T Context</param>
        public EFRepository(TContext context) : base(context)
        {
        }

        /// <summary>
        /// Create entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="entity">entity model</param>
        /// <param name="createdBy">created by</param>
        /// <returns>created Entity model</returns>
        public virtual TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class
        {
            ////entity.CreatedDate = DateTime.UtcNow;
            ////entity.CreatedBy = createdBy;
            return Context.Set<TEntity>().Add(entity);
        }

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="entity">entity model</param>
        /// <param name="modifiedBy">modified By</param>
        public virtual void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class
        {
            ////entity.ModifiedDate = DateTime.UtcNow;
            ////entity.ModifiedBy = modifiedBy;
            Context.Set<TEntity>().Attach(entity);
            Context.Entry(entity).State = EntityState.Modified;
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="id">object id</param>
        public virtual void Delete<TEntity>(object id)
            where TEntity : class
        {
            TEntity entity = Context.Set<TEntity>().Find(id);
            this.Delete(entity);
        }

        /// <summary>
        /// Delete Entity
        /// </summary>
        /// <typeparam name="TEntity">Entity Param</typeparam>
        /// <param name="entity">entity object</param>
        public virtual void Delete<TEntity>(TEntity entity)
            where TEntity : class
        {
            var dataSet = this.Context.Set<TEntity>();
            if (Context.Entry(entity).State == EntityState.Detached)
            {
                dataSet.Attach(entity);
            }

            dataSet.Remove(entity);
        }

        /// <summary>
        /// Save configure
        /// </summary>
        /// <param name="userName">user name</param>
        public virtual void Save(string userName = "")
        {
            try
            {
                Context.SaveChanges();
            }
            catch (DbEntityValidationException e)
            {
                this.ThrowEnhancedValidationException(e);
            }
        }



        /// <summary>
        /// SQL Query
        /// </summary>
        /// <typeparam name="T">T param</typeparam>
        /// <param name="sql">SQL param</param>
        /// <param name="parameters">list of parameters</param>
        /// <returns>SQL Query Data</returns>
        public DbRawSqlQuery<T> SQLQuery<T>(string sql, params object[] parameters)
        {
            return this.Context.Database.SqlQuery<T>(sql, parameters);
        }

        /// <summary>
        /// Execute SQL Command
        /// </summary>
        /// <param name="sql">SQL Param</param>
        /// <param name="parameters">list of parameters</param>
        /// <returns>SQL Command result</returns>
        public int ExecuteSqlCommand(string sql, params object[] parameters)
        {
            return Context.Database.ExecuteSqlCommand(sql, parameters);
        }

        #region IDisposable Members

        /// <summary>
        /// Dispose active
        /// </summary>
        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        /// <summary>
        /// Dispose data
        /// </summary>
        /// <param name="disposing">bool value</param>
        protected virtual void Dispose(bool disposing)
        {
            if (!this.disposed)
            {
                if (disposing)
                {
                    Context.Dispose();
                }
            }

            this.disposed = true;
        }
        #endregion

        /// <summary>
        /// Throw Enhanced Validation Exception
        /// </summary>
        /// <param name="e">validation exception</param>
        protected virtual void ThrowEnhancedValidationException(DbEntityValidationException e)
        {
            var errorMessages = e.EntityValidationErrors
                    .SelectMany(x => x.ValidationErrors)
                    .Select(x => x.ErrorMessage);

            var fullErrorMessage = string.Join("; ", errorMessages);
            var exceptionMessage = string.Concat(e.Message, " The validation errors are: ", fullErrorMessage);
            throw new DbEntityValidationException(exceptionMessage, e.EntityValidationErrors);
        }
    }
}
