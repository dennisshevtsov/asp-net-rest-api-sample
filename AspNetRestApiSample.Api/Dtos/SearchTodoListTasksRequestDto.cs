// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.Indentities;

namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents conditions to query TODO list tasks.</summary>
  public sealed class SearchTodoListTasksRequestDto : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }
  }
}
