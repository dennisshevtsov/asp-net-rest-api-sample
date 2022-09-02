// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Services
{
  using AutoMapper;
  using Moq;

  using AspNetRestApiSample.Api.Storage;
  
  [TestClass]
  public sealed class TodoListServiceTest
  {
    private CancellationToken _cancellationToken;

#pragma warning disable CS8618
    private Mock<IMapper> _mapperMock;

    private Mock<ITodoListEntityCollection> _todoListEntityCollectionMock;
    private Mock<ITodoListTaskEntityCollection> _todoListTaskEntityCollectionMock;
    private Mock<IEntityDatabase> _entityDatabaseMock;

    private TodoListService _todoListService;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _cancellationToken = CancellationToken.None;

      _mapperMock = new Mock<IMapper>();

      _todoListEntityCollectionMock = new Mock<ITodoListEntityCollection>();
      _todoListTaskEntityCollectionMock = new Mock<ITodoListTaskEntityCollection>();
      _entityDatabaseMock = new Mock<IEntityDatabase>();

      _entityDatabaseMock.SetupGet(database => database.TodoLists)
                         .Returns(_todoListEntityCollectionMock.Object);

      _entityDatabaseMock.SetupGet(database => database.TodoListTasks)
                         .Returns(_todoListTaskEntityCollectionMock.Object);

      _todoListService = new TodoListService(_mapperMock.Object, _entityDatabaseMock.Object);
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

      _mapperMock.VerifyNoOtherCalls();

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

      _mapperMock.VerifyNoOtherCalls();

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

      _mapperMock.VerifyNoOtherCalls();

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

      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.Verify(collection => collection.GetAttachedAsync(query.TodoListId, query.TodoListId, _cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public void GetTodoList_Should_Populate_Dto_With_Entity_Data()
    {
      var todoListEntity = new TodoListEntity();

      _mapperMock.Setup(mapper => mapper.Map<GetTodoListResponseDto>(It.IsAny<TodoListEntity>()))
                 .Returns(new GetTodoListResponseDto())
                 .Verifiable();

      var todoListResponseDto = _todoListService.GetTodoList(todoListEntity);

      Assert.IsNotNull(todoListResponseDto);

      _mapperMock.Verify(mapper => mapper.Map<GetTodoListResponseDto>(todoListEntity));
      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.VerifyNoOtherCalls();
      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task SearchTodoListsAsync_Should_Return_Entity_Collection()
    {
      var todoListEntityCollection = new TodoListEntity[0];

      _todoListEntityCollectionMock.Setup(collection => collection.GetDetachedTodoListsAsync(It.IsAny<CancellationToken>()))
                                   .ReturnsAsync(todoListEntityCollection)
                                   .Verifiable();

      _mapperMock.Setup(mapper => mapper.Map<SearchTodoListsRecordResponseDto[]>(It.IsAny<TodoListEntity[]>()))
                 .Returns(new SearchTodoListsRecordResponseDto[0])
                 .Verifiable();

      var searchTodoListRecordRequestDtos = await _todoListService.SearchTodoListsAsync(new SearchTodoListsRequestDto(), CancellationToken.None);

      Assert.IsNotNull(searchTodoListRecordRequestDtos);

      _mapperMock.Verify(mapper => mapper.Map<SearchTodoListsRecordResponseDto[]>(todoListEntityCollection));
      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.Verify(collection => collection.GetDetachedTodoListsAsync(_cancellationToken));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task AddTodoListAsync_Should_Create_New_Todo_List()
    {
      var todoListEntity = new TodoListEntity();

      _mapperMock.Setup(mapper => mapper.Map<TodoListEntity>(It.IsAny<AddTodoListRequestDto>()))
                 .Returns(todoListEntity)
                 .Verifiable();

      _todoListEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListEntity>()))
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

      _mapperMock.Verify(mapper => mapper.Map<TodoListEntity>(command));
      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.Verify(collection => collection.Add(todoListEntity));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task UpdateTodoListAsync_Should_Update_Entity()
    {
      _todoListEntityCollectionMock.Setup(collection => collection.Add(It.IsAny<TodoListEntity>()))
                                   .Verifiable();

      _entityDatabaseMock.Setup(database => database.CommitAsync(It.IsAny<CancellationToken>()))
                         .Returns(Task.CompletedTask)
                         .Verifiable();

      var command = new UpdateTodoListRequestDto();
      var todoListEntity = new TodoListEntity();

      await _todoListService.UpdateTodoListAsync(command, todoListEntity, CancellationToken.None);

      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.Verify(collection => collection.Add(todoListEntity));
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

      _mapperMock.VerifyNoOtherCalls();

      _todoListEntityCollectionMock.Verify(collection => collection.Delete(todoListEntity));
      _todoListEntityCollectionMock.VerifyNoOtherCalls();

      _todoListTaskEntityCollectionMock.VerifyNoOtherCalls();

      _entityDatabaseMock.Verify(database => database.CommitAsync(_cancellationToken));
      _entityDatabaseMock.VerifyNoOtherCalls();
    }
  }
}
