namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data to add a task to a todo list.</summary>
  public sealed class AddTodoListPeriodTaskRequestDto : AddTodoListTaskRequestDtoBase
  {
    /// <summary>Gets/sets an object that represents a beginning of a task.</summary>
    public DateTime Beginning { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public DateTime End { get; set; }
  }
}
