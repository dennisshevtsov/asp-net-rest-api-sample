// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using System.Linq.Expressions;

  using Microsoft.EntityFrameworkCore;
  using Microsoft.EntityFrameworkCore.Query;
  using Moq;

  [TestClass]
  public sealed class TodoListTaskServiceTest
  {
#pragma warning disable CS8618
    private Mock<IAsyncQueryProvider> _asyncQueryProviderMock;
    private Mock<DbContext> _dbContextMock;

    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _asyncQueryProviderMock = new Mock<IAsyncQueryProvider>();

      var queryableMock = new Mock<IQueryable<TodoListTaskEntity>>();

      queryableMock.SetupGet(queryable => queryable.Provider)
                   .Returns(_asyncQueryProviderMock.Object);

      queryableMock.SetupGet(queryable => queryable.Expression)
                   .Returns(Expression.Constant(new EnumerableQuery<TodoListTaskEntity>(new TodoListTaskEntity[0])));

      _asyncQueryProviderMock.Setup(provider => provider.CreateQuery<TodoListTaskEntity>(It.IsAny<Expression>()))
                             .Returns(queryableMock.Object);

      var dbSetMock = new Mock<DbSet<TodoListTaskEntity>>();

      dbSetMock.As<IQueryable<TodoListTaskEntity>>()
               .SetupGet(queryable => queryable.Provider)
               .Returns(_asyncQueryProviderMock.Object);

      dbSetMock.As<IQueryable<TodoListTaskEntity>>()
               .SetupGet(queryable => queryable.Expression)
               .Returns(queryableMock.Object.Expression);

      _dbContextMock = new Mock<DbContext>();

      _dbContextMock.Setup(context => context.Set<TodoListTaskEntity>())
                    .Returns(dbSetMock.Object);

      _todoListTaskService = new TodoListTaskService(_dbContextMock.Object);
    }

    [TestMethod]
    public async Task GetAttachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      _asyncQueryProviderMock.Setup(provider => provider.ExecuteAsync<Task<TodoListTaskEntity>>(It.IsAny<Expression>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(new TodoListTaskEntity()));

      var todoListTaskEntity = await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(new GetTodoListTaskRequestDto(), CancellationToken.None);

      Assert.IsNotNull(todoListTaskEntity);
    }

    [TestMethod]
    public async Task GetDetachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      _asyncQueryProviderMock.Setup(provider => provider.ExecuteAsync<Task<TodoListTaskEntity>>(It.IsAny<Expression>(), It.IsAny<CancellationToken>()))
                             .Returns(Task.FromResult(new TodoListTaskEntity()));

      var todoListTaskEntity = await _todoListTaskService.GetDetachedTodoListTaskEntityAsync(new GetTodoListTaskRequestDto(), CancellationToken.None);

      Assert.IsNotNull(todoListTaskEntity);
    }

    [TestMethod]
    public void GetTodoListTask_Should_Return_Populated_Dto()
    {
      var todoListTaskEntity = new TodoListTaskEntity
      {
        Id = Guid.NewGuid(),
        TodoListId = Guid.NewGuid(),
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var getTodoListTaskResponseDto = _todoListTaskService.GetTodoListTask(todoListTaskEntity);

      Assert.IsNotNull(getTodoListTaskResponseDto);

      Assert.AreEqual(todoListTaskEntity.Id, getTodoListTaskResponseDto.TodoListTaskId);
      Assert.AreEqual(todoListTaskEntity.TodoListId, getTodoListTaskResponseDto.TodoListId);
      Assert.AreEqual(todoListTaskEntity.Title, getTodoListTaskResponseDto.Title);
      Assert.AreEqual(todoListTaskEntity.Description, getTodoListTaskResponseDto.Description);
    }
  }
}
