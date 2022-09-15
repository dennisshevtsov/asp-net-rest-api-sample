﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
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
          new AddTodoListTaskRequestDtoBaseJsonConverter(),
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
      Assert.AreEqual(getTodoListDayTaskResponseDto.Date,
                      obj[nameof(GetTodoListDayTaskResponseDto.Date)]);
      Assert.AreEqual((int)TodoListTaskType.Day,
                      obj[nameof(GetTodoListDayTaskResponseDto.Type)]);
    }

    [TestMethod]
    public void Serialize_Should_Serialize_GetTodoListPeriodTaskResponseDto()
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
      Assert.AreEqual((int)getTodoListPeriodTaskResponseDto.Type,
                      obj[nameof(GetTodoListPeriodTaskResponseDto.Type)]);
    }

    [TestMethod]
    public void Serialize_Should_Serialize_Collection_Of_SearchTodoListTasksRecordResponseDtoBase()
    {
      var searchTodoListTasksDayRecordResponseDto = new SearchTodoListTasksDayRecordResponseDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 9, 1),
        Type = TodoListTaskType.Day,
      };
      var searchTodoListTasksPeriodRecordResponseDto = new SearchTodoListTasksPeriodRecordResponseDto
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
      var searchTodoListTasksRecordResponseDtos = new SearchTodoListTasksRecordResponseDtoBase[]
      {
        searchTodoListTasksDayRecordResponseDto,
        searchTodoListTasksPeriodRecordResponseDto,
      };

      var json = JsonSerializer.Serialize(searchTodoListTasksRecordResponseDtos, _jsonSerializerOptions);

      Assert.IsNotNull(json);

      var arr = Newtonsoft.Json.JsonConvert.DeserializeObject<Newtonsoft.Json.Linq.JArray>(json);

      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.TodoListId.ToString(),
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.TodoListId)]);
      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.TodoListTaskId.ToString(),
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.TodoListTaskId)]);
      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.Title,
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.Title)]);
      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.Description,
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.Description)]);
      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.Completed,
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.Completed)]);
      Assert.AreEqual(searchTodoListTasksDayRecordResponseDto.Date,
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.Date)]);
      Assert.AreEqual((int)searchTodoListTasksDayRecordResponseDto.Type,
                      arr[0][nameof(SearchTodoListTasksDayRecordResponseDto.Type)]); ;

      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.TodoListId.ToString(),
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.TodoListId)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.TodoListTaskId.ToString(),
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.TodoListTaskId)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.Title,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.Title)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.Description,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.Description)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.Completed,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.Completed)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.Beginning,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.Beginning)]);
      Assert.AreEqual(searchTodoListTasksPeriodRecordResponseDto.End,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.End)]);
      Assert.AreEqual((int)searchTodoListTasksPeriodRecordResponseDto.Type,
                      arr[1][nameof(SearchTodoListTasksPeriodRecordResponseDto.Type)]);
    }

    [TestMethod]
    public void Deserialize_Should_Deserialize_AddTodoListDayTaskRequestDto()
    {
      var expetedAddTodoListDayTaskRequestDto = new AddTodoListDayTaskRequestDto
      {
        TodoListId  = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1),
        Type = TodoListTaskType.Day,
      };

      var json = $@"{{
""{nameof(AddTodoListDayTaskRequestDto.TodoListId)}"": ""{expetedAddTodoListDayTaskRequestDto.TodoListId}"",
""{nameof(AddTodoListDayTaskRequestDto.Title)}"": ""{expetedAddTodoListDayTaskRequestDto.Title}"",
""{nameof(AddTodoListDayTaskRequestDto.Description)}"": ""{expetedAddTodoListDayTaskRequestDto.Description}"",
""{nameof(AddTodoListDayTaskRequestDto.Type)}"": {(int)expetedAddTodoListDayTaskRequestDto.Type},
""{nameof(AddTodoListDayTaskRequestDto.Date)}"": ""{expetedAddTodoListDayTaskRequestDto.Date.ToString("yyyy-MM-ddTHH:mm:ss")}""
}}
";

      var addTodoListTaskRequestDto = JsonSerializer.Deserialize<AddTodoListTaskRequestDtoBase>(json, _jsonSerializerOptions);

      Assert.IsNotNull(addTodoListTaskRequestDto);

      var actualAddTodoListDayTaskRequestDto = addTodoListTaskRequestDto as AddTodoListDayTaskRequestDto;

      Assert.IsNotNull(actualAddTodoListDayTaskRequestDto);
      Assert.AreEqual(expetedAddTodoListDayTaskRequestDto.TodoListId,
                      actualAddTodoListDayTaskRequestDto.TodoListId);
      Assert.AreEqual(expetedAddTodoListDayTaskRequestDto.Title,
                      actualAddTodoListDayTaskRequestDto.Title);
      Assert.AreEqual(expetedAddTodoListDayTaskRequestDto.Description,
                      actualAddTodoListDayTaskRequestDto.Description);
      Assert.AreEqual(expetedAddTodoListDayTaskRequestDto.Date,
                      actualAddTodoListDayTaskRequestDto.Date);
      Assert.AreEqual(expetedAddTodoListDayTaskRequestDto.Type,
                      actualAddTodoListDayTaskRequestDto.Type);
    }

    [TestMethod]
    public void Deserialize_Should_Deserialize_AddTodoListPeriodTaskRequestDto()
    {
      var expetedAddTodoListPeriodTaskRequestDto = new AddTodoListPeriodTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 13, 30, 0),
        Type = TodoListTaskType.Period,
      };

      var json = $@"{{
""{nameof(AddTodoListPeriodTaskRequestDto.TodoListId)}"": ""{expetedAddTodoListPeriodTaskRequestDto.TodoListId}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Title)}"": ""{expetedAddTodoListPeriodTaskRequestDto.Title}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Description)}"": ""{expetedAddTodoListPeriodTaskRequestDto.Description}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Type)}"": {(int)expetedAddTodoListPeriodTaskRequestDto.Type},
""{nameof(AddTodoListPeriodTaskRequestDto.Beginning)}"": ""{expetedAddTodoListPeriodTaskRequestDto.Beginning.ToString("yyyy-MM-ddTHH:mm:ss")}"",
""{nameof(AddTodoListPeriodTaskRequestDto.End)}"": ""{expetedAddTodoListPeriodTaskRequestDto.End.ToString("yyyy-MM-ddTHH:mm:ss")}""
}}
";

      var addTodoListTaskRequestDto = JsonSerializer.Deserialize<AddTodoListTaskRequestDtoBase>(json, _jsonSerializerOptions);

      Assert.IsNotNull(addTodoListTaskRequestDto);

      var actualAddTodoListPeriodTaskRequestDto = addTodoListTaskRequestDto as AddTodoListPeriodTaskRequestDto;

      Assert.IsNotNull(actualAddTodoListPeriodTaskRequestDto);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.TodoListId,
                      actualAddTodoListPeriodTaskRequestDto.TodoListId);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Title,
                      actualAddTodoListPeriodTaskRequestDto.Title);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Description,
                      actualAddTodoListPeriodTaskRequestDto.Description);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Beginning,
                      actualAddTodoListPeriodTaskRequestDto.Beginning);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.End,
                      actualAddTodoListPeriodTaskRequestDto.End);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Type,
                      actualAddTodoListPeriodTaskRequestDto.Type);
    }
  }
}
