// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  public abstract class EntityCollectionBase<TEntity> : IEntityCollection<TEntity> where TEntity : TodoListEntityBase
  {
    private readonly DbContext _dbContext;

    protected EntityCollectionBase(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<TEntity?> GetAttachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> GetDetachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    public TEntity Add(object command)
    {
      var entity = Activator.CreateInstance<TEntity>();

      _dbContext.Entry(entity)
                .CurrentValues
                .SetValues(command);

      return entity;
    }

    public void Delete(TEntity entity) => _dbContext.Entry(entity).State = EntityState.Deleted;

    protected IQueryable<TEntity> AsQueryable() => _dbContext.Set<TEntity>();
  }
}
