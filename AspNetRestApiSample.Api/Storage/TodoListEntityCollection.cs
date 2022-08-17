// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListEntityCollection : EntityCollectionBase<TodoListEntity>, ITodoListEntityCollection
  {
    public TodoListEntityCollection(DbContext dbContext) : base(dbContext)
    {
    }
  }
}
