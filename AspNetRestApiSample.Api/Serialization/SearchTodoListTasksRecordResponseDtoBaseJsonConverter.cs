﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Serialization
{
  using System.Text.Json;
  using System.Text.Json.Serialization;

  using AspNetRestApiSample.Api.Dtos;

  /// <summary>Converts an object or value to or from JSON.</summary>
  public sealed class SearchTodoListTasksRecordResponseDtoBaseJsonConverter
    : JsonConverter<SearchTodoListTasksRecordResponseDtoBase>
  {
    /// <summary>Reads and converts the JSON to type <see cref="AspNetRestApiSample.Api.Dtos.SearchTodoListTasksRecordResponseDtoBase"/>.</summary>
    /// <param name="reader">The <see cref="System.Text.Json.Utf8JsonReader"/> to read from.</param>
    /// <param name="typeToConvert">The <see cref="AspNetRestApiSample.Api.Dtos.SearchTodoListTasksRecordResponseDtoBase"/> being converted.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    /// <returns>The value that was converted.</returns>
    public override SearchTodoListTasksRecordResponseDtoBase? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

    /// <summary>Write the value as JSON.</summary>
    /// <param name="writer">The <see cref="System.Text.Json.Utf8JsonWriter"/> to write to.</param>
    /// <param name="value">The value to convert.</param>
    /// <param name="options">The <see cref="System.Text.Json.JsonSerializerOptions"/> being used.</param>
    public override void Write(
      Utf8JsonWriter writer,
      SearchTodoListTasksRecordResponseDtoBase value,
      JsonSerializerOptions options)
    {
      if (value == null)
      {
        writer.WriteNullValue();
      }
      else
      {
        JsonSerializer.Serialize(writer, value, value.GetType(), options);
      }
    }
  }
}
