﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents data to create a new todo list.</summary>
  public interface IAddTodoListRequestDto : IRequestDto
  {
    /// <summary>Gets an object that represents a title of a todo list.</summary>
    public string? Title { get; }

    /// <summary>Gets an object that represents a description of a todo list.</summary>
    public string? Description { get; }
  }
}
