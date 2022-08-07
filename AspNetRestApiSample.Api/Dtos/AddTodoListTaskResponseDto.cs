using AspNetRestApiSample.Api.Indentities;

namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data of a response for the add todo list task command.</summary>
  public sealed class AddTodoListTaskResponseDto : ITodoListIdentity, ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that reprsents an ID of a todo list.</summary>
    public Guid TodoListId { get; set; }

    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
