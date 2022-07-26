﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  using AspNetRestApiSample.ApplicationCore.Indentities;

  /// <summary>Represents conditions to query a TODO list task.</summary>
  public sealed class GetTodoListTaskRequestDto : ITodoListIdentity, ITodoListTaskIdentity, IRequestDto
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
