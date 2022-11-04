// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Dtos
{
  using AspNetRestApiSample.Indentities;

  /// <summary>Represents a base of a record of a response for search TODO list tasks.</summary>
  public abstract class SearchTodoListTasksRecordResponseDtoBase : TodoListTaskDtoBase, ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }

    /// <summary>Gets/sets an object that indicates if a TODO list task is completed.</summary>
    public bool Completed { get; set; }
  }
}
