﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Represents a base of a response for the get TODO list query.</summary>
  public abstract class GetTodoListTaskResponseDtoBase : ITodoListIdentity, ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a TODO list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents an ID of a TODO list task.</summary>
    public Guid TodoListTaskId { get; set; }

    /// <summary>Gets/sets an object that represents a title of a TODO list task.</summary>
    public string? Title { get; set; }

    /// <summary>Gets/sets an object that represents a description of a TODO list task.</summary>
    public string? Description { get; set; }

    /// <summary>Gets/sets an object that indicates if an TODO list is completed.</summary>
    public bool Completed { get; set; }
  }
}