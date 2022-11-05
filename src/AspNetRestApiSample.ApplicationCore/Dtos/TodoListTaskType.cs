// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents types of a TODO list task.</summary>
  public enum TodoListTaskType : byte
  {
    /// <summary>A value that indicates if a TODO list task is unknown.</summary>
    Unknown = 0,

    /// <summary>A value that indicates if a TODO list task is a day task.</summary>
    Day = 1,

    /// <summary>A value that indicates if a TODO list task is a period task.</summary>
    Period = 2,
  }
}
