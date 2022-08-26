// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using Moq;

  using AspNetRestApiSample.Api.Storage;
  using static Microsoft.EntityFrameworkCore.DbLoggerCategory.Database;

  [TestClass]
  public sealed class TodoListServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<ITodoListEntityCollection> _todoListEntityCollectionMock;
    private Mock<ITodoListTaskEntityCollection> _todoListTaskEntityCollectionMock;
    private Mock<IEntityDatabase> _entityDatabaseMock;

    private TodoListService _todoListService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _todoListEntityCollectionMock = new Mock<ITodoListEntityCollection>();
      _todoListTaskEntityCollectionMock = new Mock<ITodoListTaskEntityCollection>();
      _entityDatabaseMock = new Mock<IEntityDatabase>();

      _entityDatabaseMock.SetupGet(database => database.TodoLists)
                         .Returns(_todoListEntityCollectionMock.Object);

      _entityDatabaseMock.SetupGet(database => database.TodoListTasks)
                         .Returns(_todoListTaskEntityCollectionMock.Object);

      _todoListService = new TodoListService(_entityDatabaseMock.Object);
    }

    [TestMethod]
    public async Task GetDetachedTodoListAsync_Should_Return_Null()
    {
      _todoListEntityCollectionMock.Setup(collection => collection.GetDetachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(default(TodoListEntity))
                                   .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var todoListEntity = await _todoListService.GetDetachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNull(todoListEntity);

      _todoListEntityCollectionMock.Verify(collection => collection.GetDetachedAsync(query.TodoListId, query.TodoListId, _cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetDetachedTodoListAsync_Should_Return_Detached_Entity()
    {
      var testTodoListEntity = new TodoListEntity();

      _todoListEntityCollectionMock.Setup(collection => collection.GetDetachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(testTodoListEntity)
                                   .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actualTodoListEntity = await _todoListService.GetDetachedTodoListAsync(query, CancellationToken.None);

      Assert.AreEqual(testTodoListEntity, actualTodoListEntity);

      _todoListEntityCollectionMock.Verify(collection => collection.GetDetachedAsync(query.TodoListId, query.TodoListId, _cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAttachedTodoListAsync_Should_Return_Null()
    {
      _todoListEntityCollectionMock.Setup(collection => collection.GetAttachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(default(TodoListEntity))
                                   .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var todoListEntity = await _todoListService.GetAttachedTodoListAsync(query, CancellationToken.None);

      Assert.IsNull(todoListEntity);

      _todoListEntityCollectionMock.Verify(collection => collection.GetAttachedAsync(query.TodoListId, query.TodoListId, _cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task GetAttachedTodoListAsync_Should_Return_Attached_Entity()
    {
      var testTodoListEntity = new TodoListEntity();

      _todoListEntityCollectionMock.Setup(collection => collection.GetAttachedAsync(It.IsAny<Guid>(), It.IsAny<Guid>(), It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(testTodoListEntity)
                                   .Verifiable();

      var query = new GetTodoListRequestDto
      {
        TodoListId = Guid.NewGuid(),
      };

      var actualTodoListEntity = await _todoListService.GetAttachedTodoListAsync(query, CancellationToken.None);

      Assert.AreEqual(testTodoListEntity, actualTodoListEntity);

      _todoListEntityCollectionMock.Verify(collection => collection.GetAttachedAsync(query.TodoListId, query.TodoListId, _cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void GetTodoList_Should_Return_Filled_Dto()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListResponseDto = _todoListService.GetTodoList(todoListEntity);

      Assert.IsNotNull(todoListResponseDto);

      Assert.AreEqual(todoListEntity.Id, todoListResponseDto.TodoListId);
      Assert.AreEqual(todoListEntity.Title, todoListResponseDto.Title);
      Assert.AreEqual(todoListEntity.Description, todoListResponseDto.Description);
    }

    [TestMethod]
    public async Task SearchTodoListsAsync_Should_Return_Entity_Collection()
    {
      var todoListIdCollection = new[]
      {
        Guid.NewGuid(),
        Guid.NewGuid(),
        Guid.NewGuid(),
      };

      var todoListEntityCollection = new[]
      {
        new TodoListEntity
        {
          Id = todoListIdCollection[0],
          TodoListId = todoListIdCollection[0],
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListEntity
        {
          Id = todoListIdCollection[1],
          TodoListId = todoListIdCollection[1],
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
        new TodoListEntity
        {
          Id = todoListIdCollection[2],
          TodoListId = todoListIdCollection[2],
          Title = Guid.NewGuid().ToString(),
          Description = Guid.NewGuid().ToString(),
        },
      };

      _todoListEntityCollectionMock.Setup(collection => collection.GetDetachedTodoListsAsync(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(todoListEntityCollection)
                                   .Verifiable();

      var searchTodoListRecordRequestDtos = await _todoListService.SearchTodoListsAsync(new SearchTodoListsRequestDto(), CancellationToken.None);

      Assert.IsNotNull(searchTodoListRecordRequestDtos);
      Assert.AreEqual(todoListEntityCollection.Length, searchTodoListRecordRequestDtos.Length);

      var searchTodoListRecordRequestDto0 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListIdCollection[0]);

      Assert.IsNotNull(searchTodoListRecordRequestDto0);

      Assert.AreEqual(todoListEntityCollection[0].TodoListId, searchTodoListRecordRequestDto0.TodoListId);
      Assert.AreEqual(todoListEntityCollection[0].Title, searchTodoListRecordRequestDto0.Title);
      Assert.AreEqual(todoListEntityCollection[0].Description, searchTodoListRecordRequestDto0.Description);

      var searchTodoListRecordRequestDto1 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListIdCollection[1]);

      Assert.IsNotNull(searchTodoListRecordRequestDto1);

      Assert.AreEqual(todoListEntityCollection[1].TodoListId, searchTodoListRecordRequestDto1.TodoListId);
      Assert.AreEqual(todoListEntityCollection[1].Title, searchTodoListRecordRequestDto1.Title);
      Assert.AreEqual(todoListEntityCollection[1].Description, searchTodoListRecordRequestDto1.Description);

      var searchTodoListRecordRequestDto2 = searchTodoListRecordRequestDtos.FirstOrDefault(dto => dto.TodoListId == todoListIdCollection[2]);

      Assert.IsNotNull(searchTodoListRecordRequestDto2);

      Assert.AreEqual(todoListEntityCollection[2].TodoListId, searchTodoListRecordRequestDto2.TodoListId);
      Assert.AreEqual(todoListEntityCollection[2].Title, searchTodoListRecordRequestDto2.Title);
      Assert.AreEqual(todoListEntityCollection[2].Description, searchTodoListRecordRequestDto2.Description);

      _todoListEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListsAsync(_cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListAsync_Should_Create_New_Todo_List()
    {
      var todoListId = Guid.NewGuid();
      TodoListEntity? todoListEntity = null;

      _todoListEntityCollectionMock.Setup(collection => collection.Update(It.IsAny<AddTodoListRequestDto>(), It.IsAny<TodoListEntity>()))
                                   .Callback((object command, TodoListEntity entity) =>
                                   {
                                     entity.TodoListId = todoListId;

                                     todoListEntity = entity;
                                   })
                                   .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new AddTodoListRequestDto
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var addTodoListResponseDto = await _todoListService.AddTodoListAsync(command, CancellationToken.None);

      Assert.IsNotNull(addTodoListResponseDto);
      Assert.IsNotNull(todoListEntity);
      Assert.AreEqual(todoListId, addTodoListResponseDto.TodoListId);

      _todoListEntityCollectionMock.Verify(collection => collection.Update(command, todoListEntity));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoListAsync_Should_Update_Entity()
    {
      _todoListEntityCollectionMock.Setup(collection => collection.Update(It.IsAny<UpdateTodoListRequestDto>(), It.IsAny<TodoListEntity>()))
                                   .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new UpdateTodoListRequestDto();
      var todoListEntity = new TodoListEntity();

      await _todoListService.UpdateTodoListAsync(command, todoListEntity, CancellationToken.None);

      _todoListEntityCollectionMock.Verify(collection => collection.Update(command, todoListEntity));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task DeleteTodoListAsync_Should_Remove_Entity()
    {
      _todoListEntityCollectionMock.Setup(collection => collection.Delete(It.IsAny<TodoListEntity>()))
                                   .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var todoListEntity = new TodoListEntity();

      await _todoListService.DeleteTodoListAsync(todoListEntity, CancellationToken.None);

      _todoListEntityCollectionMock.Verify(collection => collection.Delete(todoListEntity));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }
  }
}
