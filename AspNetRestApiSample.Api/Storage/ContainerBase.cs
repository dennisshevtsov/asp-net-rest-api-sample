// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  public abstract class ContainerBase<TEntity> : IContainer<TEntity> where TEntity : TodoListEntityBase
  {
    private readonly DbContext _dbContext;

    protected ContainerBase(DbContext dbContext)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));
    }

    public Task<TEntity?> GetAttachedEntityAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    public Task<TEntity?> GetDetachedEntityAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken)
      => _dbContext.Set<TEntity>()
                   .AsNoTracking()
                   .WithPartitionKey(todoListId.ToString())
                   .Where(entity => entity.Id == id)
                   .FirstOrDefaultAsync(cancellationToken);

    public TEntity Create(object command)
    {
      var entity = Activator.CreateInstance<TEntity>();

      _dbContext.Attach(entity)
                .CurrentValues
                .SetValues(command);

      return entity;
    }

    public void Delete(TEntity entity) => _dbContext.Entry(entity).State = EntityState.Deleted;
  }
}
