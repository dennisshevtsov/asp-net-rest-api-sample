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
      var todoListDayTaskEntity = new TodoListPeriodTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
        Completed = true,
        Beginning = new DateTime(2022, 9, 1, 12, 15, 0),
        End = new DateTime(2022, 9, 1, 12, 45, 0),
      };

      var getTodoListTaskResponseDto = _mapper.Map<GetTodoListTaskResponseDtoBase>(todoListDayTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      var getTodoListPeriodTaskResponseDto = getTodoListTaskResponseDto as GetTodoListPeriodTaskResponseDto;

      Assert.IsNotNull(getTodoListPeriodTaskResponseDto);
      Assert.AreEqual(todoListDayTaskEntity.TodoListId, getTodoListPeriodTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListDayTaskEntity.Title, getTodoListPeriodTaskResponseDto.Title);
      Assert.AreEqual(todoListDayTaskEntity.Description, getTodoListPeriodTaskResponseDto.Description);
      Assert.AreEqual(todoListDayTaskEntity.Completed, getTodoListPeriodTaskResponseDto.Completed);
      Assert.AreEqual(todoListDayTaskEntity.Beginning, getTodoListPeriodTaskResponseDto.Beginning);
      Assert.AreEqual(todoListDayTaskEntity.End, getTodoListPeriodTaskResponseDto.End);
    }
  }
}
