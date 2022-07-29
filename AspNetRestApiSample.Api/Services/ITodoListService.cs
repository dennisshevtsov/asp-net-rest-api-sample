// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Services
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;
  using AspNetRestApiSample.Api.Indentities;

  public interface ITodoListService
  {
    public Task<TodoListEntity?> GetDetachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken);

    public Task<TodoListEntity?> GetAttachedTodoListAsync(
      ITodoListIdentity query, CancellationToken cancellationToken);

    public GetTodoListResponseDto GetTodoList(TodoListEntity todoListEntity);

    public Task<SearchTodoListsRecordResponseDto[]> SearchTodoListsAsync(
      SearchTodoListsRequestDto query, CancellationToken cancellationToken);

    public Task<AddTodoListResponseDto> AddTodoListAsync(
      AddTodoListRequestDto command, CancellationToken cancellationToken);

    public Task UpdateTodoListAsync(
      UpdateTodoListRequestDto command,
      TodoListEntity todoListEntity,
      CancellationToken cancellationToken);

    public Task DeleteTodoListAsync(
      TodoListEntity todoListEntity, CancellationToken cancellationToken);
  }
}
