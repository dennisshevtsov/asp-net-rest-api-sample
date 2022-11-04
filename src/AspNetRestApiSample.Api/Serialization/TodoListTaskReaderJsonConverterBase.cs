// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Serialization
{
  using System.Text.Json;
  using System.Text.Json.Serialization;

  using AspNetRestApiSample.Api.Dtos;

  /// <summary>Converts an object or value to or from JSON.</summary>
  public abstract class TodoListTaskReaderJsonConverterBase<T> : JsonConverter<T> where T : TodoListTaskDtoBase
  {
    /// <summary>Reads and converts the JSON to type <see cref="AspNetRestApiSample.Api.Dtos.SearchTodoListTasksRecordResponseDtoBase"/>.</summary>
    /// <param name="reader">The <see cref="System.Text.Json.Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The <see cref="System.Type"/> being converted.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    /// <returns>The value that was converted.</returns>
    public override T? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      var todoListTaskType = TodoListTaskReaderJsonConverterBase<T>.GetTodoListType(reader);

      T? requestDto = null;

      if (todoListTaskType != TodoListTaskType.Unknown)
      {
        var requestDtoType = GetRequestDtoType(todoListTaskType);

        requestDto = JsonSerializer.Deserialize(ref reader, requestDtoType, options) as T;
      }

      return requestDto;
    }

    /// <summary>Write the value as JSON.</summary>
    /// <param name="writer">The <see cref="System.Text.Json.Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    public override void Write(
      Utf8JsonWriter writer,
      T value,
      JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    /// <summary>Gets an instance of the <see cref="System.Type"/> that represents a type of a request DTO.</summary>
    /// <param name="todoListTaskType">A value that represents a type of a request DTO.</param>
    /// <returns>A value that represents a type of a request DTO.</returns>
    protected abstract Type GetRequestDtoType(TodoListTaskType todoListTaskType);

    private static TodoListTaskType GetTodoListType(Utf8JsonReader reader)
    {
      var todoListTaskType = TodoListTaskType.Unknown;
      string? propertyName = null;

      while (reader.Read())
      {
        if (reader.TokenType == JsonTokenType.PropertyName)
        {
          propertyName = reader.GetString();

          continue;
        }

        if (propertyName == null || !string.Equals(propertyName, nameof(AddTodoListTaskRequestDtoBase.Type), StringComparison.OrdinalIgnoreCase))
        {
          continue;
        }

        if (reader.TokenType == JsonTokenType.Number)
        {
          var todoListTaskTypeNumber = reader.GetByte();

          if (Enum.IsDefined(typeof(TodoListTaskType), todoListTaskTypeNumber))
          {
            todoListTaskType = (TodoListTaskType)todoListTaskTypeNumber;
          }

          break;
        }

        if (reader.TokenType == JsonTokenType.String)
        {
          var todoListTaskTypeString = reader.GetString();

          Enum.TryParse(todoListTaskTypeString, out todoListTaskType);
        }

        break;
      }

      return todoListTaskType;
    }
  }
}
