// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Serialization
{
  using System.Text.Json;
  using System.Text.Json.Serialization;

  using AspNetRestApiSample.Api.Dtos;

  public sealed class SearchTodoListTasksRecordResponseDtoBaseJsonConverter
    : JsonConverter<SearchTodoListTasksRecordResponseDtoBase>
  {
    public override SearchTodoListTasksRecordResponseDtoBase? Read(
      ref Utf8JsonReader reader,
      Type typeToConvert,
      JsonSerializerOptions options)
    {
      throw new NotImplementedException();
    }

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
