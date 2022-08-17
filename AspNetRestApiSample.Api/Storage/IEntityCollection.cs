// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using AspNetRestApiSample.Api.Entities;

  public interface IEntityCollection<TEntity> where TEntity : TodoListEntityBase
  {
    public Task<TEntity?> GetAttachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken);

    public Task<TEntity?> GetDetachedAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken);

    public TEntity Add(object command);

    public void Delete(TEntity entity);
  }
}
