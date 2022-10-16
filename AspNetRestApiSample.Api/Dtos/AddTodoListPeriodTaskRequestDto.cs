// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data to add a task to a todo list.</summary>
  public sealed class AddTodoListPeriodTaskRequestDto : AddTodoListTaskRequestDtoBase
  {
    /// <summary>Gets/sets an object that represents a beginning of a task.</summary>
    public long Begin { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public long End { get; set; }
  }
}
