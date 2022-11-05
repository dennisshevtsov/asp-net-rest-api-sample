// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  using AspNetRestApiSample.ApplicationCore.Indentities;

  /// <summary>Represents conditions to query a todo list.</summary>
  public sealed class GetTodoListRequestDto : ITodoListIdentity, IRequestDto
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }
  }
}
