﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents data to add a task to a todo list.</summary>
  public sealed class AddTodoListDayTaskRequestDto : AddTodoListTaskRequestDtoBase
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public long Date { get; set; }
  }
}
