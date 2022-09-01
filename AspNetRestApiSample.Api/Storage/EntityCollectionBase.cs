// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  /// <summary>Provides a simple API to an entity collection in a database.</summary>
  /// <typeparam name="TEntity">A type of an entity.</typeparam>
  public abstract class EntityCollectionBase<TEntity> : IEntityCollection<TEntity> where TEntity : TodoListEntityBase
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Storage.EntityCollectionBase{TEntity}"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    protected EntityCollectionBase(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    /// <summary>Gets an attached entity that satisfies defined conditions.</summary>
    /// <param name="id">An object that represents a primary ID of an entity.</param>
    /// <param name="todoListId">An object that represents a ID of a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TEntity?> GetAttachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Gets an detached entity that satisfies defined conditions.</summary>
    /// <param name="id">An object that represents a primary ID of an entity.</param>
    /// <param name="todoListId">An object that represents a ID of a TODO list.</param>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation that can return a value.</returns>
    public Task<TEntity?> GetDetachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    /// <summary>Populates an entity with data of a command.</summary>
    /// <param name="command">An object that represents data to populate an entity.</param>
    /// <param name="entity">An instance of an entity.</param>
    public void AddOrUpdate(object command, TEntity entity)
    {
      var entry = _dbContext.Entry(entity);

      if (entry.State == EntityState.Detached)
      {
        entry.State = EntityState.Added;
      }

      entry.CurrentValues.SetValues(command);
    }

    /// <summary>Enqueues an entity to be deleted.</summary>
    /// <param name="entity">An instance of an entity.</param>
    public void Delete(TEntity entity) => _dbContext.Entry(entity).State = EntityState.Deleted;

    protected IQueryable<TEntity> AsQueryable() => _dbContext.Set<TEntity>();
  }
}
