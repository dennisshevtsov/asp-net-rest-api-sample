// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Indentities;

  /// <summary>Represents conditions to query a todo list.</summary>
  public sealed class GetTodoListRequestDto : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    [FromRoute(Name = "todoListId")]
    public Guid TodoListId { get; set; }
  }
}
