// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using AspNetRestApiSample.Api.Entities;

  public interface IContainer<TEntity> where TEntity : TodoListEntityBase
  {
    public Task<TEntity> GetAttachedEntityAsync(Guid id, Guid partitionKey);

    public Task<TEntity> GetDetachedEntityAsync(Guid id, Guid partitionKey);

    public TEntity Create(object command);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);
  }
}
