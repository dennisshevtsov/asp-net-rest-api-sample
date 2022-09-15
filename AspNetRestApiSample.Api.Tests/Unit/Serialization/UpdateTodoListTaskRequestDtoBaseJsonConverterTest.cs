// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Serialization
{
  using System.Text.Json;

  using AspNetRestApiSample.Api.Serialization;

  [TestClass]
  public sealed class UpdateTodoListTaskRequestDtoBaseJsonConverterTest
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
          new UpdateTodoListTaskRequestDtoBaseJsonConverter(),
        },
      };
    }

    [TestMethod]
    public void Deserialize_Should_Deserialize_UpdateTodoListDayTaskRequestDto()
    {
      var expetedUpdateTodoListDayTaskRequestDto = new UpdateTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1),
        Type = TodoListTaskType.Day,
      };

      var json = $@"{{
""{nameof(UpdateTodoListDayTaskRequestDto.TodoListId)}"": ""{expetedUpdateTodoListDayTaskRequestDto.TodoListId}"",
""{nameof(UpdateTodoListDayTaskRequestDto.TodoListTaskId)}"": ""{expetedUpdateTodoListDayTaskRequestDto.TodoListTaskId}"",
""{nameof(UpdateTodoListDayTaskRequestDto.Title)}"": ""{expetedUpdateTodoListDayTaskRequestDto.Title}"",
""{nameof(UpdateTodoListDayTaskRequestDto.Description)}"": ""{expetedUpdateTodoListDayTaskRequestDto.Description}"",
""{nameof(UpdateTodoListDayTaskRequestDto.Type)}"": {(int)expetedUpdateTodoListDayTaskRequestDto.Type},
""{nameof(UpdateTodoListDayTaskRequestDto.Date)}"": ""{expetedUpdateTodoListDayTaskRequestDto.Date.ToString("yyyy-MM-ddTHH:mm:ss")}""
}}
";

      var updateTodoListTaskRequestDto = JsonSerializer.Deserialize<UpdateTodoListTaskRequestDtoBase>(json, _jsonSerializerOptions);

      Assert.IsNotNull(updateTodoListTaskRequestDto);

      var actualAddTodoListDayTaskRequestDto = updateTodoListTaskRequestDto as UpdateTodoListDayTaskRequestDto;

      Assert.IsNotNull(actualAddTodoListDayTaskRequestDto);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.TodoListId,
                      actualAddTodoListDayTaskRequestDto.TodoListId);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.TodoListTaskId,
                      actualAddTodoListDayTaskRequestDto.TodoListTaskId);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.Title,
                      actualAddTodoListDayTaskRequestDto.Title);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.Description,
                      actualAddTodoListDayTaskRequestDto.Description);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.Date,
                      actualAddTodoListDayTaskRequestDto.Date);
      Assert.AreEqual(expetedUpdateTodoListDayTaskRequestDto.Type,
                      actualAddTodoListDayTaskRequestDto.Type);
    }

    [TestMethod]
    public void Deserialize_Should_Deserialize_UpdateTodoListPeriodTaskRequestDto()
    {
      var expetedUpdateTodoListPeriodTaskRequestDto = new UpdateTodoListPeriodTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        TodoListTaskId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 13, 30, 0),
        Type = TodoListTaskType.Period,
      };

      var json = $@"{{
""{nameof(UpdateTodoListPeriodTaskRequestDto.TodoListId)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.TodoListId}"",
""{nameof(UpdateTodoListPeriodTaskRequestDto.TodoListTaskId)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.TodoListTaskId}"",
""{nameof(UpdateTodoListPeriodTaskRequestDto.Title)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.Title}"",
""{nameof(UpdateTodoListPeriodTaskRequestDto.Description)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.Description}"",
""{nameof(UpdateTodoListPeriodTaskRequestDto.Type)}"": {(int)expetedUpdateTodoListPeriodTaskRequestDto.Type},
""{nameof(UpdateTodoListPeriodTaskRequestDto.Beginning)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.Beginning.ToString("yyyy-MM-ddTHH:mm:ss")}"",
""{nameof(UpdateTodoListPeriodTaskRequestDto.End)}"": ""{expetedUpdateTodoListPeriodTaskRequestDto.End.ToString("yyyy-MM-ddTHH:mm:ss")}""
}}
";

      var updateTodoListTaskRequestDto = JsonSerializer.Deserialize<UpdateTodoListTaskRequestDtoBase>(json, _jsonSerializerOptions);

      Assert.IsNotNull(updateTodoListTaskRequestDto);

      var actualAddTodoListPeriodTaskRequestDto = updateTodoListTaskRequestDto as UpdateTodoListPeriodTaskRequestDto;

      Assert.IsNotNull(actualAddTodoListPeriodTaskRequestDto);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.TodoListId,
                      actualAddTodoListPeriodTaskRequestDto.TodoListId);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.TodoListTaskId,
                      actualAddTodoListPeriodTaskRequestDto.TodoListTaskId);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.Title,
                      actualAddTodoListPeriodTaskRequestDto.Title);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.Description,
                      actualAddTodoListPeriodTaskRequestDto.Description);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.Beginning,
                      actualAddTodoListPeriodTaskRequestDto.Beginning);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.End,
                      actualAddTodoListPeriodTaskRequestDto.End);
      Assert.AreEqual(expetedUpdateTodoListPeriodTaskRequestDto.Type,
                      actualAddTodoListPeriodTaskRequestDto.Type);
    }
  }
}
