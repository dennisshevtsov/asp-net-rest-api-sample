// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using AspNetRestApiSample.Api.Entities;

  public interface IContainer<TEntity> where TEntity : TodoListEntityBase
  {
    public Task<TEntity?> GetAttachedEntityAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken);

    public Task<TEntity?> GetDetachedEntityAsync(
      Guid id, Guid todoListId, CancellationToken cancellationToken);

    public TEntity Create(object command);

    public void Delete(TEntity entity);
  }
}
