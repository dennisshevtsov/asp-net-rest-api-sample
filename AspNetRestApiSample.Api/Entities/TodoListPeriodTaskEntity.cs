// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Entities
{
  /// <summary>Represents data of a TODO list period task.</summary>
  public sealed class TodoListPeriodTaskEntity : TodoListTaskEntityBase
  {
    /// <summary>Gets/sets an object that represents a begin of a task.</summary>
    public DateTime Begin { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public DateTime End { get; set; }
  }
}
