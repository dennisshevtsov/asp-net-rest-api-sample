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
        Begin = new DateTime(2022, 9, 1, 12, 15, 0),
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
      Assert.AreEqual(todoListPeriodTaskEntity.Begin, getTodoListPeriodTaskResponseDto.Begin);
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
        Completed = false,
        Begin = new DateTime(2022, 9, 1, 12, 15, 0),
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
      Assert.AreEqual(todoListPeriodTaskEntity.Begin, searchTodoListTasksPeriodRecordResponseDto.Begin);
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
      AddTodoListTaskRequestDtoBase addTodoListTaskRequestDto = addTodoListDayTaskRequestDto;

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

    [TestMethod]
    public void Map_Should_Populate_AddTodoListTaskResponseDto_From_TodoListDayTaskEntity()
    {
      TodoListTaskEntityBase todoListTaskEntity = new TodoListDayTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
      };

      var addTodoListTaskResponseDto = _mapper.Map<AddTodoListTaskResponseDto>(todoListTaskEntity);

      Assert.IsNotNull(addTodoListTaskResponseDto);

      Assert.AreEqual(todoListTaskEntity.Id, addTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, addTodoListTaskResponseDto.TodoListId);
    }

    [TestMethod]
    public void Map_Should_Populate_TodoListPeriodTaskEntity_From_AddTodoListDayTaskRequestDto()
    {
      var addTodoListPeriodTaskRequestDto = new AddTodoListPeriodTaskRequestDto
      {
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Begin = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 12, 45, 0),
      };
      AddTodoListTaskRequestDtoBase addTodoListTaskRequestDto = addTodoListPeriodTaskRequestDto;

      var todoListTaskEntity = _mapper.Map<TodoListTaskEntityBase>(addTodoListTaskRequestDto);

      Assert.IsNotNull(todoListTaskEntity);

      var todoListPeriodTaskEntity = todoListTaskEntity as TodoListPeriodTaskEntity;

      Assert.IsNotNull(todoListPeriodTaskEntity);
      Assert.AreEqual(default, todoListPeriodTaskEntity.Id);
      Assert.AreEqual(addTodoListPeriodTaskRequestDto.TodoListId, todoListPeriodTaskEntity.TodoListId);
      Assert.AreEqual(addTodoListPeriodTaskRequestDto.Title, todoListPeriodTaskEntity.Title);
      Assert.AreEqual(addTodoListPeriodTaskRequestDto.Description, todoListPeriodTaskEntity.Description);
      Assert.IsFalse(todoListPeriodTaskEntity.Completed);
      Assert.AreEqual(addTodoListPeriodTaskRequestDto.Begin, todoListPeriodTaskEntity.Begin);
      Assert.AreEqual(addTodoListPeriodTaskRequestDto.End, todoListPeriodTaskEntity.End);
    }

    [TestMethod]
    public void Map_Should_Populate_AddTodoListTaskResponseDto_From_TodoListPeriodTaskEntity()
    {
      TodoListTaskEntityBase todoListTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
      };

      var addTodoListTaskResponseDto = _mapper.Map<AddTodoListTaskResponseDto>(todoListTaskEntity);

      Assert.IsNotNull(addTodoListTaskResponseDto);

      Assert.AreEqual(todoListTaskEntity.Id, addTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, addTodoListTaskResponseDto.TodoListId);
    }

    [TestMethod]
    public void Map_Should_Populate_TodoListDayTaskEntity_From_UpdateTodoListDayTaskRequestDto()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var updateTodoListDayTaskRequestDto = new UpdateTodoListDayTaskRequestDto
      {
        TodoListTaskId = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Date = new DateTime(2022, 9, 1),
      };
      UpdateTodoListTaskRequestDtoBase updateTodoListTaskRequestDto = updateTodoListDayTaskRequestDto;

      var todoListDayTaskEntity = new TodoListDayTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = false,
        Date = new DateTime(2022, 9, 1),
      };
      TodoListTaskEntityBase todoListTaskEntity = todoListDayTaskEntity;

      _mapper.Map(updateTodoListTaskRequestDto, todoListTaskEntity);

      Assert.AreEqual(updateTodoListDayTaskRequestDto.TodoListTaskId, todoListDayTaskEntity.Id);
      Assert.AreEqual(updateTodoListDayTaskRequestDto.TodoListId, todoListDayTaskEntity.TodoListId);
      Assert.AreEqual(updateTodoListDayTaskRequestDto.Title, todoListDayTaskEntity.Title);
      Assert.AreEqual(updateTodoListDayTaskRequestDto.Description, todoListDayTaskEntity.Description);
      Assert.IsFalse(todoListDayTaskEntity.Completed);
      Assert.AreEqual(updateTodoListDayTaskRequestDto.Date, todoListDayTaskEntity.Date);
    }

    [TestMethod]
    public void Map_Should_Populate_TodoListPeriodTaskEntity_From_UpdateTodoListPeriodTaskRequestDto()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var updateTodoListPeriodTaskRequestDto = new UpdateTodoListPeriodTaskRequestDto
      {
        TodoListTaskId = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Begin = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 12, 45, 0),
      };
      UpdateTodoListTaskRequestDtoBase updateTodoListTaskRequestDto = updateTodoListPeriodTaskRequestDto;

      var todoListPeriodTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = false,
        Begin = new DateTime(2022, 9, 2, 13, 45, 0),
        End = new DateTime(2022, 9, 2, 14, 30, 0),
      };
      TodoListTaskEntityBase todoListTaskEntity = todoListPeriodTaskEntity;

      _mapper.Map(updateTodoListTaskRequestDto, todoListTaskEntity);

      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.TodoListTaskId, todoListPeriodTaskEntity.Id);
      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.TodoListId, todoListPeriodTaskEntity.TodoListId);
      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.Title, todoListPeriodTaskEntity.Title);
      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.Description, todoListPeriodTaskEntity.Description);
      Assert.IsFalse(todoListPeriodTaskEntity.Completed);
      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.Begin, todoListPeriodTaskEntity.Begin);
      Assert.AreEqual(updateTodoListPeriodTaskRequestDto.End, todoListPeriodTaskEntity.End);
    }
  }
}
