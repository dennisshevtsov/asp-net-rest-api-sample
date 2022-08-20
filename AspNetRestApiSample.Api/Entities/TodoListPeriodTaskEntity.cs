namespace AspNetRestApiSample.Api.Entities
{
  /// <summary>Represents data of a TODO list period task.</summary>
  public sealed class TodoListPeriodTaskEntity : TodoListTaskEntityBase
  {
    /// <summary>Gets/sets an object that represents a beginning of a task.</summary>
    public DateTime Beginning { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public DateTime End { get; set; }
  }
}
