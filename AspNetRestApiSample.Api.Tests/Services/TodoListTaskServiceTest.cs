// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using Microsoft.EntityFrameworkCore;
  using Moq;

  [TestClass]
  public sealed class TodoListTaskServiceTest
  {
#pragma warning disable CS8618
    private Mock<DbContext> _dbContextMock;
    private TodoListTaskService _todoListTaskService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _dbContextMock = new Mock<DbContext>();
      _todoListTaskService = new TodoListTaskService(_dbContextMock.Object);
    }

    [TestMethod]
    public async Task GetAttachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListTaskEntityCollection = new[]
      {
        testTodoListTaskEntity,
      };

      SetupDbContext(todoListTaskEntityCollection);

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetAttachedTodoListTaskEntityAsync(query, CancellationToken.None);

      Assert.IsNotNull(actulalTodoListTaskEntity);

      Assert.AreEqual(testTodoListTaskEntity.Id, actulalTodoListTaskEntity.Id);
      Assert.AreEqual(testTodoListTaskEntity.TodoListId, actulalTodoListTaskEntity.TodoListId);
      Assert.AreEqual(testTodoListTaskEntity.Title, actulalTodoListTaskEntity.Title);
      Assert.AreEqual(testTodoListTaskEntity.Description, actulalTodoListTaskEntity.Description);
    }

    [TestMethod]
    public async Task GetDetachedTodoListTaskEntityAsync_Should_Return_Entity()
    {
      var todoListId = Guid.NewGuid();
      var todoListTaskId = Guid.NewGuid();

      var testTodoListTaskEntity = new TodoListTaskEntity
      {
        Id = todoListTaskId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListTaskEntityCollection = new[]
      {
        testTodoListTaskEntity,
      };

      SetupDbContext(todoListTaskEntityCollection);

      var query = new GetTodoListTaskRequestDto
      {
        TodoListId = todoListId,
        TodoListTaskId = todoListTaskId,
      };

      var actulalTodoListTaskEntity =
        await _todoListTaskService.GetDetachedTodoListTaskEntityAsync(query, CancellationToken.None);

      Assert.IsNotNull(actulalTodoListTaskEntity);

      Assert.AreEqual(testTodoListTaskEntity.Id, actulalTodoListTaskEntity.Id);
      Assert.AreEqual(testTodoListTaskEntity.TodoListId, actulalTodoListTaskEntity.TodoListId);
      Assert.AreEqual(testTodoListTaskEntity.Title, actulalTodoListTaskEntity.Title);
      Assert.AreEqual(testTodoListTaskEntity.Description, actulalTodoListTaskEntity.Description);
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

    private void SetupDbContext<TEntity>(IEnumerable<TEntity> collection) where TEntity : class
    {
      var queryable = collection.AsQueryable();

      var dbSetMock = new Mock<DbSet<TEntity>>();

      dbSetMock.As<IAsyncEnumerable<TEntity>>()
               .Setup(enumerable => enumerable.GetAsyncEnumerator(default))
               .Returns(new AsyncEnumeratorMock<TEntity>(queryable.GetEnumerator()));

      dbSetMock.As<IQueryable<TEntity>>()
               .Setup(queryable => queryable.Provider)
               .Returns(new AsyncQueryProviderMock<TEntity>(queryable.Provider));

      dbSetMock.As<IQueryable<TEntity>>()
               .Setup(queryable => queryable.Expression).Returns(queryable.Expression);

      dbSetMock.As<IQueryable<TEntity>>()
               .Setup(queryable => queryable.ElementType).Returns(queryable.ElementType);

      dbSetMock.As<IQueryable<TEntity>>()
               .Setup(queryable => queryable.GetEnumerator()).Returns(queryable.GetEnumerator());

      _dbContextMock.Setup(context => context.Set<TEntity>())
                    .Returns(dbSetMock.Object);
    }
  }
}
