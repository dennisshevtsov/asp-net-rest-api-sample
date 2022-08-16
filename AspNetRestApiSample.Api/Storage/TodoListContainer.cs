// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListContainer : ContainerBase<TodoListEntity>, ITodoListContainer
  {
    public TodoListContainer(DbContext dbContext) : base(dbContext)
    {
    }
  }
}
