// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Serialization
{
  using System.Text.Json;
  using System.Text.Json.Serialization;

  using AspNetRestApiSample.Api.Dtos;

  /// <summary>Converts an object or value to or from JSON.</summary>
  public sealed class UpdateTodoListTaskRequestDtoBaseJsonConverter
    : JsonConverter<UpdateTodoListTaskRequestDtoBase>
  {
    /// <summary>Reads and converts the JSON to type <see cref="AspNetRestApiSample.Api.Dtos.UpdateTodoListTaskRequestDtoBase"/>.</summary>
    /// <param name="reader">The <see cref="System.Text.Json.Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The <see cref="System.Type"/> being converted.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    /// <returns>The value that was converted.</returns>
    public override UpdateTodoListTaskRequestDtoBase? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      var todoListTaskType = UpdateTodoListTaskRequestDtoBaseJsonConverter.GetTodoListType(reader);

      UpdateTodoListTaskRequestDtoBase? requestDto = null;

      if (todoListTaskType != TodoListTaskType.Unknown)
      {
        var requestDtoType = UpdateTodoListTaskRequestDtoBaseJsonConverter.GetRequestDtoType(todoListTaskType);

        requestDto = JsonSerializer.Deserialize(ref reader, requestDtoType, options) as UpdateTodoListTaskRequestDtoBase;
      }

      return requestDto;
    }

    /// <summary>Write the value as JSON.</summary>
    /// <param name="writer">The <see cref="System.Text.Json.Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    public override void Write(
      Utf8JsonWriter writer,
      UpdateTodoListTaskRequestDtoBase value,
      JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

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

        if (propertyName == null || !string.Equals(propertyName, nameof(UpdateTodoListTaskRequestDtoBase.Type), StringComparison.OrdinalIgnoreCase))
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

    private static Type GetRequestDtoType(TodoListTaskType todoListTaskType)
    {
      if (todoListTaskType == TodoListTaskType.Day)
      {
        return typeof(UpdateTodoListDayTaskRequestDto);
      }

      return typeof(UpdateTodoListPeriodTaskRequestDto);
    }
  }
}
