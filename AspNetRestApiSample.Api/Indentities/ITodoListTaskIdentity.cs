namespace AspNetRestApiSample.Api.Indentities
{
  /// <summary>Represents an identity of a todo list task.</summary>
  public interface ITodoListTaskIdentity
  {
    /// <summary>Gets/sets an object that represents an ID of a todo list task.</summary>
    public Guid TodoListTaskId { get; set; }
  }
}
