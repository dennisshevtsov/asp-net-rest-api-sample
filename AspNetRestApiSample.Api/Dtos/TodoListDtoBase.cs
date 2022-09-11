namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents a base of a TODO list.</summary>
  public abstract class TodoListDtoBase
  {
    /// <summary>Gets/sets an object that represents a title of a todo list.</summary>
    public string? Title { get; set; }

    /// <summary>Gets/sets an object that represents a description of a todo list.</summary>
    public string? Description { get; set; }
  }
}
