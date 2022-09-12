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

    [TestMethod]
    public void Serialize_Should_Serialize_GetTodoListDayTaskResponseDto()
    {
      var dto = new GetTodoListDayTaskResponseDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 9, 1),
        Type = TodoListTaskType.Day,
      };

      var json = JsonSerializer.Serialize(dto, _jsonSerializerOptions);

      Assert.IsNotNull(json);

      var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

      Assert.AreEqual(dto.TodoListId.ToString(), obj[nameof(GetTodoListDayTaskResponseDto.TodoListId)]);
      Assert.AreEqual(dto.TodoListTaskId.ToString(), obj[nameof(GetTodoListDayTaskResponseDto.TodoListTaskId)]);
      Assert.AreEqual(dto.Title, obj[nameof(GetTodoListDayTaskResponseDto.Title)]);
      Assert.AreEqual(dto.Description, obj[nameof(GetTodoListDayTaskResponseDto.Description)]);
      Assert.AreEqual(dto.Completed, obj[nameof(GetTodoListDayTaskResponseDto.Completed)]);
      Assert.AreEqual(dto.Date, obj[nameof(GetTodoListDayTaskResponseDto.Date)]);
      Assert.AreEqual((int)TodoListTaskType.Day, obj[nameof(GetTodoListDayTaskResponseDto.Type)]);
    }
  }
}
