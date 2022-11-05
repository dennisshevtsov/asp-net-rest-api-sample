// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.WebApi.Serialization.Test.Unit
{
  using System.Text.Json;

  [TestClass]
  public sealed class AddTodoListTaskRequestDtoBaseJsonConverterTest
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
          new AddTodoListTaskRequestDtoBaseJsonConverter(),
        },
      };
    }

    [TestMethod]
    public void Deserialize_Should_Deserialize_AddTodoListDayTaskRequestDto()
    {
      var expetedAddTodoListDayTaskRequestDto = new AddTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1).Ticks,
        Type = TodoListTaskType.Day,
      };

      var json = $@"{{
""{nameof(AddTodoListDayTaskRequestDto.Type)}"": {(int)expetedAddTodoListDayTaskRequestDto.Type},
""{nameof(AddTodoListDayTaskRequestDto.TodoListId)}"": ""{expetedAddTodoListDayTaskRequestDto.TodoListId}"",
""{nameof(AddTodoListDayTaskRequestDto.Title)}"": ""{expetedAddTodoListDayTaskRequestDto.Title}"",
""{nameof(AddTodoListDayTaskRequestDto.Description)}"": ""{expetedAddTodoListDayTaskRequestDto.Description}"",
""{nameof(AddTodoListDayTaskRequestDto.Date)}"": {expetedAddTodoListDayTaskRequestDto.Date}
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
        Begin = new DateTime(2022, 9, 1, 12, 15, 0).Ticks,
        End = new DateTime(2022, 9, 1, 13, 30, 0).Ticks,
        Type = TodoListTaskType.Period,
      };

      var json = $@"{{
""{nameof(AddTodoListPeriodTaskRequestDto.Type)}"": {(int)expetedAddTodoListPeriodTaskRequestDto.Type},
""{nameof(AddTodoListPeriodTaskRequestDto.TodoListId)}"": ""{expetedAddTodoListPeriodTaskRequestDto.TodoListId}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Title)}"": ""{expetedAddTodoListPeriodTaskRequestDto.Title}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Description)}"": ""{expetedAddTodoListPeriodTaskRequestDto.Description}"",
""{nameof(AddTodoListPeriodTaskRequestDto.Begin)}"": {expetedAddTodoListPeriodTaskRequestDto.Begin},
""{nameof(AddTodoListPeriodTaskRequestDto.End)}"": {expetedAddTodoListPeriodTaskRequestDto.End}
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
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Begin,
                      actualAddTodoListPeriodTaskRequestDto.Begin);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.End,
                      actualAddTodoListPeriodTaskRequestDto.End);
      Assert.AreEqual(expetedAddTodoListPeriodTaskRequestDto.Type,
                      actualAddTodoListPeriodTaskRequestDto.Type);
    }
  }
}
