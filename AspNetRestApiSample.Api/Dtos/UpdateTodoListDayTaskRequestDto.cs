// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data to update todo list task.</summary>
  public sealed class UpdateTodoListDayTaskRequestDto : UpdateTodoListTaskRequestDtoBase
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public long Date { get; set; }
  }
}
