// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Dtos
{
  /// <summary>Represents data of a TODO list day task for a response of a request to get a TODO list task.</summary>
  public sealed class GetTodoListDayTaskResponseDto : GetTodoListTaskResponseDtoBase
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public long Date { get; set; }
  }
}
