namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data of a TODO list day task for a response of a request to get a TODO list task.</summary>
  public sealed class GetTodoListPeriodTaskResponseDto : GetTodoListTaskResponseDtoBase
  {
    /// <summary>Gets/sets an object that represents a beginning of a task.</summary>
    public DateTime Beginning { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public DateTime End { get; set; }
  }
}
