// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Entities
{
  /// <summary>Represents data of a TODO list day task.</summary>
  public sealed class TodoListDayTaskEntity : TodoListTaskEntityBase
  {
    /// <summary>Gets/sets an object that represents a date of a task.</summary>
    public DateTime Date { get; set; }
  }
}
