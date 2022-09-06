// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Mapping
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class TodoListMappingTest
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
    public void Map_Should_Populate_GetTodoListResponseDto()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var getTodoListResponseDto = _mapper.Map<GetTodoListResponseDto>(todoListEntity);

      Assert.IsNotNull(getTodoListResponseDto);
      Assert.AreEqual(todoListEntity.TodoListId, getTodoListResponseDto.TodoListId);
      Assert.AreEqual(todoListEntity.Title, getTodoListResponseDto.Title);
      Assert.AreEqual(todoListEntity.Description, getTodoListResponseDto.Description);
    }

    [TestMethod]
    public void Map_Should_Populate_SearchTodoListsRecordResponseDto()
    {
      var todoListIdCollection = new[]
      {
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
      };

      var searchTodoListsRecordResponseDtoCollection =
        _mapper.Map<SearchTodoListsRecordResponseDto[]>(todoListEntityCollection);

      Assert.IsNotNull(searchTodoListsRecordResponseDtoCollection);
      Assert.AreEqual(todoListEntityCollection.Length, searchTodoListsRecordResponseDtoCollection.Length);

      TodoListMappingTest.Check(todoListEntityCollection[0], searchTodoListsRecordResponseDtoCollection);
      TodoListMappingTest.Check(todoListEntityCollection[1], searchTodoListsRecordResponseDtoCollection);
    }

    [TestMethod]
    public void Map_Should_Populate_TodoListEntity_From_AddTodoListRequestDto()
    {
      var addTodoListRequestDto = new AddTodoListRequestDto
      {
        Title = Guid.NewGuid().ToString(),
        Description = Guid.NewGuid().ToString(),
      };

      var todoListEntity = _mapper.Map<TodoListEntity>(addTodoListRequestDto);

      Assert.IsNotNull(todoListEntity);
      Assert.AreEqual(default, todoListEntity.Id);
      Assert.AreEqual(default, todoListEntity.TodoListId);
      Assert.AreEqual(addTodoListRequestDto.Title, todoListEntity.Title);
      Assert.AreEqual(addTodoListRequestDto.Description, todoListEntity.Description);
      Assert.AreEqual(default, todoListEntity.Tasks);
    }

    [TestMethod]
    public void Map_Should_Populate_AddTodoListResponseDto()
    {
      var todoListId = Guid.NewGuid();
      var todoListEntity = new TodoListEntity
      {
        Id = todoListId,
        TodoListId = todoListId,
      };

      var addTodoListResponseDto = _mapper.Map<AddTodoListResponseDto>(todoListEntity);

      Assert.IsNotNull(addTodoListResponseDto);
      Assert.AreEqual(todoListEntity.TodoListId, addTodoListResponseDto.TodoListId);
    }

    private static void Check(TodoListEntity todoListEntity, SearchTodoListsRecordResponseDto[] searchTodoListsRecordResponseDtoCollection)
    {
      var searchTodoListsRecordResponseDto0 = searchTodoListsRecordResponseDtoCollection.FirstOrDefault(
        dto => dto.TodoListId == todoListEntity.TodoListId);

      Assert.IsNotNull(searchTodoListsRecordResponseDto0);
      Assert.AreEqual(todoListEntity.TodoListId, searchTodoListsRecordResponseDto0.TodoListId);
      Assert.AreEqual(todoListEntity.Title, searchTodoListsRecordResponseDto0.Title);
      Assert.AreEqual(todoListEntity.Description, searchTodoListsRecordResponseDto0.Description);
    }
  }
}
