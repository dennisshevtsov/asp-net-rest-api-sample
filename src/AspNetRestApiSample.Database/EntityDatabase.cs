// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Database
{
  using Microsoft.EntityFrameworkCore;

  /// <summary>Provides a simple API to a database.</summary>
  public sealed class EntityDatabase : IEntityDatabase
  {
    private readonly DbContext _dbContext;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Database.EntityDatabase"/> class.</summary>
    /// <param name="dbContext">An object that represents a session with the database and can be used to query and save instances of your entities.</param>
    /// <param name="todoLists">An object that provides a simple API to an entity collection in a database.</param>
    /// <param name="todoListTasks">An object that provides a simple API to an entity collection in a database.</param>
    public EntityDatabase(
      DbContext dbContext,
      ITodoListEntityCollection todoLists,
      ITodoListTaskEntityCollection todoListTasks)
    {
      _dbContext = dbContext ?? throw new ArgumentNullException(nameof(dbContext));

      TodoLists = todoLists ?? throw new ArgumentNullException(nameof(todoLists));
      TodoListTasks = todoListTasks ?? throw new ArgumentNullException(nameof(todoListTasks));
    }

    /// <summary>Gets an object that provides a simple API to an entity collection in a database.</summary>
    public ITodoListEntityCollection TodoLists { get; }

    /// <summary>Gets an object that provides a simple API to an entity collection in a database.</summary>
    public ITodoListTaskEntityCollection TodoListTasks { get; }

    /// <summary>Commits all tracking changes.</summary>
    /// <param name="cancellationToken">An object that propagates notification that operations should be canceled.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task CommitAsync(CancellationToken cancellationToken)
      => _dbContext.SaveChangesAsync(cancellationToken);
  }
}
