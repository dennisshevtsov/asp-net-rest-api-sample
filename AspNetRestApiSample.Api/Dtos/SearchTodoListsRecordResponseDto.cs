// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  using AspNetRestApiSample.Api.Indentities;

  public sealed class SearchTodoListsRecordResponseDto : ITodoListIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
  }
}
