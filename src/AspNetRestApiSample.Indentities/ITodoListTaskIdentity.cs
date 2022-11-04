// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Indentities
{
  /// <summary>Represents an identity of a todo list task.</summary>
  public interface ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
