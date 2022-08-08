// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Entities
{
  /// <summary>Represents data of a todo list.</summary>
  public sealed class TodoListEntity : TodoListEntityBase
  {
    /// <summary>Gets/sets an object that represents a collection TODO list tasks.</summary>
    public IEnumerable<TodoListTaskEntity>? Tasks { get; set; }
  }
}
