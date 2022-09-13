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
      var getTodoListDayTaskResponseDto = new GetTodoListDayTaskResponseDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 9, 1),
        Type = TodoListTaskType.Day,
      };
      GetTodoListTaskResponseDtoBase getTodoListTaskResponseDto = getTodoListDayTaskResponseDto;

      var json = JsonSerializer.Serialize(getTodoListTaskResponseDto, _jsonSerializerOptions);

      Assert.IsNotNull(json);

      var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

      Assert.AreEqual(getTodoListDayTaskResponseDto.TodoListId.ToString(),
                      obj[nameof(GetTodoListDayTaskResponseDto.TodoListId)]);
      Assert.AreEqual(getTodoListDayTaskResponseDto.TodoListTaskId.ToString(),
                      obj[nameof(GetTodoListDayTaskResponseDto.TodoListTaskId)]);
      Assert.AreEqual(getTodoListDayTaskResponseDto.Title,
                      obj[nameof(GetTodoListDayTaskResponseDto.Title)]);
      Assert.AreEqual(getTodoListDayTaskResponseDto.Description,
                      obj[nameof(GetTodoListDayTaskResponseDto.Description)]);
      Assert.AreEqual(getTodoListDayTaskResponseDto.Completed,
                      obj[nameof(GetTodoListDayTaskResponseDto.Completed)]);
      Assert.AreEqual(getTodoListDayTaskResponseDto.Date, obj[nameof(GetTodoListDayTaskResponseDto.Date)]);
      Assert.AreEqual((int)TodoListTaskType.Day,
                      obj[nameof(GetTodoListDayTaskResponseDto.Type)]);
    }

    [TestMethod]
    public void Serialize_Should_Serialize_AddTodoListPeriodTaskRequestDto()
    {
      var getTodoListPeriodTaskResponseDto = new GetTodoListPeriodTaskResponseDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 13, 30, 0),
        Type = TodoListTaskType.Period,
      };
      GetTodoListTaskResponseDtoBase getTodoListTaskResponseDto = getTodoListPeriodTaskResponseDto;

      var json = JsonSerializer.Serialize(getTodoListTaskResponseDto, _jsonSerializerOptions);

      Assert.IsNotNull(json);

      var obj = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JObject>(json);

      Assert.AreEqual(getTodoListPeriodTaskResponseDto.TodoListId.ToString(),
                      obj[nameof(GetTodoListPeriodTaskResponseDto.TodoListId)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.TodoListTaskId.ToString(),
                      obj[nameof(GetTodoListPeriodTaskResponseDto.TodoListTaskId)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.Title,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Title)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.Description,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Description)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.Completed,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Completed)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.Beginning,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Beginning)]);
      Assert.AreEqual(getTodoListPeriodTaskResponseDto.End,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.End)]);
      Assert.AreEqual((int)TodoListTaskType.Day,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Type)]);
    }
  }
}
