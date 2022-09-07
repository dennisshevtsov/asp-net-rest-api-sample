// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Mapping
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class TodoListTaskMappingTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;
    private IMapper _mapper;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var serviceScope = new ServiceCollection().AddMapping().BuildServiceProvider().CreateScope();

      _disposable = serviceScope;
      _mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [TestCleanup]
    public void CleanUp()
    {
      _disposable?.Dispose();
    }

    [TestMethod]
    public void Map_Should_Populate_GetTodoListDayTaskResponseDto()
    {
      var todoListDayTaskEntity = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 9, 1),
      };

      var getTodoListTaskResponseDto = _mapper.Map<GetTodoListTaskResponseDtoBase>(todoListDayTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      var getTodoListDayTaskResponseDto = getTodoListTaskResponseDto as GetTodoListDayTaskResponseDto;

      Assert.IsNotNull(getTodoListDayTaskResponseDto);
      Assert.AreEqual(todoListDayTaskEntity.TodoListId, getTodoListDayTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListDayTaskEntity.Title, getTodoListDayTaskResponseDto.Title);
      Assert.AreEqual(todoListDayTaskEntity.Description, getTodoListDayTaskResponseDto.Description);
      Assert.AreEqual(todoListDayTaskEntity.Completed, getTodoListDayTaskResponseDto.Completed);
      Assert.AreEqual(todoListDayTaskEntity.Date, getTodoListDayTaskResponseDto.Date);
    }

    [TestMethod]
    public void Map_Should_Populate_GetTodoListPeriodTaskResponseDto()
    {
      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 12, 45, 0),
      };

      var getTodoListTaskResponseDto = _mapper.Map<GetTodoListTaskResponseDtoBase>(todoListPeriodTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      var getTodoListPeriodTaskResponseDto = getTodoListTaskResponseDto as GetTodoListPeriodTaskResponseDto;

      Assert.IsNotNull(getTodoListPeriodTaskResponseDto);
      Assert.AreEqual(todoListPeriodTaskEntity.TodoListId, getTodoListPeriodTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListPeriodTaskEntity.Title, getTodoListPeriodTaskResponseDto.Title);
      Assert.AreEqual(todoListPeriodTaskEntity.Description, getTodoListPeriodTaskResponseDto.Description);
      Assert.AreEqual(todoListPeriodTaskEntity.Completed, getTodoListPeriodTaskResponseDto.Completed);
      Assert.AreEqual(todoListPeriodTaskEntity.Beginning, getTodoListPeriodTaskResponseDto.Beginning);
      Assert.AreEqual(todoListPeriodTaskEntity.End, getTodoListPeriodTaskResponseDto.End);
    }

    [TestMethod]
    public void Map_Should_Populate_Collection_Of_SearchTodoListTasksRecordResponseDtoBase()
    {
      var todoListId = Guid.NewGuid();
      var todoListDayTaskEntity = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Date = new DateTime(2022, 9, 1),
      };
      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 12, 45, 0),
      };
      var todoListTaskEntityCollection = new TodoListTaskEntityBase[]
      {
        todoListDayTaskEntity,
        todoListPeriodTaskEntity,
      };

      var searchTodoListTasksRecordResponseDtoCollection =
        _mapper.Map<SearchTodoListTasksRecordResponseDtoBase[]>(todoListTaskEntityCollection);

      Assert.IsNotNull(searchTodoListTasksRecordResponseDtoCollection);

      var searchTodoListTasksDayRecordResponseDto =
        searchTodoListTasksRecordResponseDtoCollection[0] as SearchTodoListTasksDayRecordResponseDto;

      Assert.IsNotNull(searchTodoListTasksDayRecordResponseDto);
      Assert.AreEqual(todoListDayTaskEntity.TodoListId, searchTodoListTasksDayRecordResponseDto.TodoListId);
      Assert.AreEqual(todoListDayTaskEntity.Title, searchTodoListTasksDayRecordResponseDto.Title);
      Assert.AreEqual(todoListDayTaskEntity.Description, searchTodoListTasksDayRecordResponseDto.Description);
      Assert.AreEqual(todoListDayTaskEntity.Completed, searchTodoListTasksDayRecordResponseDto.Completed);
      Assert.AreEqual(todoListDayTaskEntity.Date, searchTodoListTasksDayRecordResponseDto.Date);

      var searchTodoListTasksPeriodRecordResponseDto =
        searchTodoListTasksRecordResponseDtoCollection[1] as SearchTodoListTasksPeriodRecordResponseDto;

      Assert.IsNotNull(searchTodoListTasksPeriodRecordResponseDto);
      Assert.AreEqual(todoListPeriodTaskEntity.TodoListId, searchTodoListTasksPeriodRecordResponseDto.TodoListId);
      Assert.AreEqual(todoListPeriodTaskEntity.Title, searchTodoListTasksPeriodRecordResponseDto.Title);
      Assert.AreEqual(todoListPeriodTaskEntity.Description, searchTodoListTasksPeriodRecordResponseDto.Description);
      Assert.AreEqual(todoListPeriodTaskEntity.Completed, searchTodoListTasksPeriodRecordResponseDto.Completed);
      Assert.AreEqual(todoListPeriodTaskEntity.Beginning, searchTodoListTasksPeriodRecordResponseDto.Beginning);
      Assert.AreEqual(todoListPeriodTaskEntity.End, searchTodoListTasksPeriodRecordResponseDto.End);
    }

    [TestMethod]
    public void Map_Should_Populate_TodoListDayTaskEntity_From_AddTodoListDayTaskRequestDto()
    {
      var addTodoListDayTaskRequestDto = new AddTodoListDayTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1),
      };
      var addTodoListTaskRequestDto = addTodoListDayTaskRequestDto;

      var todoListTaskEntity = _mapper.Map<TodoListTaskEntityBase>(addTodoListTaskRequestDto);

      Assert.IsNotNull(todoListTaskEntity);

      var todoListDayTaskEntity = todoListTaskEntity as TodoListDayTaskEntity;

      Assert.IsNotNull(todoListDayTaskEntity);
      Assert.AreEqual(default, todoListDayTaskEntity.Id);
      Assert.AreEqual(addTodoListDayTaskRequestDto.TodoListId, todoListDayTaskEntity.TodoListId);
      Assert.AreEqual(addTodoListDayTaskRequestDto.Title, todoListDayTaskEntity.Title);
      Assert.AreEqual(addTodoListDayTaskRequestDto.Description, todoListDayTaskEntity.Description);
      Assert.IsFalse(todoListDayTaskEntity.Completed);
      Assert.AreEqual(addTodoListDayTaskRequestDto.Date, todoListDayTaskEntity.Date);
    }
  }
}
