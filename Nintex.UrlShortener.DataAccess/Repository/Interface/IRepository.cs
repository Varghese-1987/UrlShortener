/// <summary>
/// Nintex.UrlShortener.DataAccess.Repository
/// </summary>
namespace Nintex.UrlShortener.DataAccess
{

    /// <summary>
    /// Interface Repository
    /// </summary>
    public interface IRepository : IReadOnlyRepository
    {
        /// <summary>
        /// Create Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="entity">entity type</param>
        /// <param name="createdBy">created by</param>
        /// <returns>Entity Result</returns>
        TEntity Create<TEntity>(TEntity entity, string createdBy = null)
            where TEntity : class;

        /// <summary>
        /// Update Entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="entity">entity type</param>
        /// <param name="modifiedBy"> modified By</param>
        void Update<TEntity>(TEntity entity, string modifiedBy = null)
            where TEntity : class;

        /// <summary>
        /// Delete entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="id">object id</param>
        void Delete<TEntity>(object id)
            where TEntity : class;

        /// <summary>
        /// Delete Entity by entity
        /// </summary>
        /// <typeparam name="TEntity">entity param</typeparam>
        /// <param name="entity">entity model</param>
        void Delete<TEntity>(TEntity entity)
            where TEntity : class;

        /// <summary>
        /// save entity
        /// </summary>
        /// <param name="userName">user name</param>
        void Save(string userName = "");

        
    }
}