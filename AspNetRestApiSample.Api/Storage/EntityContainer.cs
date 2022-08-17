// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Storage
{
  using Microsoft.EntityFrameworkCore;

  public sealed class EntityContainer : IEntityContainer
  {
    private readonly DbContext _dbContext;

    public EntityContainer(
      DbContext dbContext,
      ITodoListEntityCollection todoLists,
      ITodoListTaskEntityCollection todoListTasks)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

      TodoLists = todoLists ?? throw new ArgumentNullException(nameof(todoLists));
      TodoListTasks = todoListTasks ?? throw new ArgumentNullException(nameof(todoListTasks));
    }

    public ITodoListEntityCollection TodoLists { get; }

    public ITodoListTaskEntityCollection TodoListTasks { get; }

    public Task CommitAsync(CancellationToken cancellationToken)
      => _dbContext.SaveChangesAsync(cancellationToken);
  }
}
