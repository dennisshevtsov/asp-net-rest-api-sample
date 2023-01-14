// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents data to update todo list task.</summary>
  public interface IUpdateTodoListPeriodTaskRequestDto : IUpdateTodoListTaskRequestDto
  {
    /// <summary>Gets an object that represents a begin of a task.</summary>
    public long Begin { get; }

    /// <summary>Gets an object that represents an end of a task.</summary>
    public long End { get; }
  }
}
