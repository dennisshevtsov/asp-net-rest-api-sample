﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents data to add a task to a todo list.</summary>
  public interface IAddTodoListTaskRequestDto : ITodoListTaskDto, IRequestDto
  {
  }
}
