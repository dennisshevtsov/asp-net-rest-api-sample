// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Serialization
{
  using AspNetRestApiSample.Dtos;

  /// <summary>Converts an object or value to or from JSON.</summary>
  public sealed class AddTodoListTaskRequestDtoBaseJsonConverter
    : TodoListTaskReaderJsonConverterBase<AddTodoListTaskRequestDtoBase>
  {
    /// <summary>Gets an instance of the <see cref="System.Type"/> that represents a type of a request DTO.</summary>
    /// <param name="todoListTaskType">A value that represents a type of a request DTO.</param>
    /// <returns>A value that represents a type of a request DTO.</returns>
    protected override Type GetRequestDtoType(TodoListTaskType todoListTaskType)
    {
      if (todoListTaskType == TodoListTaskType.Day)
      {
        return typeof(AddTodoListDayTaskRequestDto);
      }

      return typeof(AddTodoListPeriodTaskRequestDto);
    }
  }
}
