// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Serialization
{
  using System.Text.Json;

  using AspNetRestApiSample.Api.Serialization;

  [TestClass]
  public sealed class TodoListTaskWriterJsonConverterTest
  {
#pragma warning disable CS8618
    private JsonSerializerOptions _jsonSerializerOptions;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _jsonSerializerOptions = new JsonSerializerOptions
      {
        Converters = {
          new TodoListTaskWriterJsonConverter<GetTodoListTaskResponseDtoBase>(),
          new TodoListTaskWriterJsonConverter<SearchTodoListTasksRecordResponseDtoBase>(),
        },
      };
    }
  }
}
