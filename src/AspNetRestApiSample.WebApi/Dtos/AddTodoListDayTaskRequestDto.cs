// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.WebApi.Dtos
{
  using AspNetRestApiSample.ApplicationCore.Dtos;

  /// <summary>Represents data to add a task to a todo list.</summary>
  public class AddTodoListDayTaskRequestDto : AddTodoListTaskRequestDtoBase, IAddTodoListDayTaskRequestDto
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public long Date { get; set; }
  }
}
