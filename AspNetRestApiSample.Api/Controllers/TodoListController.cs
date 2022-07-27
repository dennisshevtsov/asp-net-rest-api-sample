// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Controllers
{
  using Microsoft.AspNetCore.Mvc;

  using AspNetRestApiSample.Api.Dtos;

  [ApiController]
  [Route("todo-list")]
  public sealed class TodoListController : ControllerBase
  {
    [HttpGet("{todoListId}", Name = nameof(TodoListController.GetTodoList))]
    [Consumes("application/json")]
    public Task<GetTodoListResponseDto> GetTodoList(
      [FromRoute] GetTodoListRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult(new GetTodoListResponseDto());
    }

    [HttpGet(Name = nameof(TodoListController.SearchTodoLists))]
    [Consumes("application/json")]
    public Task<SearchTodoListsRecordResponseDto[]> SearchTodoLists(
      [FromRoute] SearchTodoListsRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.FromResult(new SearchTodoListsRecordResponseDto[0]);
    }

    [HttpPost("{todoListId}", Name = nameof(TodoListController.AddTodoList))]
    [Consumes("application/json")]
    public Task<AddTodoListResponseDto> AddTodoList(
      [FromBody] AddTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.FromResult(new AddTodoListResponseDto());
    }

    [HttpPut("{todoListId}", Name = nameof(TodoListController.UpdateTodoList))]
    [Consumes("application/json")]
    public Task UpdateTodoList(
      [FromBody] UpdateTodoListRequestDto command,
      CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }

    [HttpDelete("{todoListId}", Name = nameof(TodoListController.DeleteTodoList))]
    [Consumes("application/json")]
    public Task DeleteTodoList(
      [FromRoute] DeleteTodoListRequestDto query,
      CancellationToken cancellationToken)
    {
      return Task.CompletedTask;
    }
  }
}
