// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Entities
{
  using AspNetRestApiSample.Api.Indentities;

  public sealed class TodoListEntity : ITodoListIdentity
  {
    public Guid TodoListId { get; set; }

    public string? Title { get; set; }

    public string? Description { get; set; }
  }
}
