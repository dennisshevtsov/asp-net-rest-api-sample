// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  using AspNetRestApiSample.ApplicationCore.Indentities;

  /// <summary>Represents data to update a todo list.</summary>
  public sealed class UpdateTodoListRequestDto : TodoListDtoBase, ITodoListIdentity, IRequestDto
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }
  }
}
