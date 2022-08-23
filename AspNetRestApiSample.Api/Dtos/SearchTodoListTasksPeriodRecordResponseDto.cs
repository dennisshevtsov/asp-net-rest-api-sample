namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data of a TODO list period task for a response of request to search TODO list tasks.</summary>
  public sealed class SearchTodoListTasksPeriodRecordResponseDto : SearchTodoListTasksRecordResponseDtoBase
  {
    /// <summary>Gets/sets an object that represents a beginning of a task.</summary>
    public DateTime Beginning { get; set; }

    /// <summary>Gets/sets an object that represents an end of a task.</summary>
    public DateTime End { get; set; }
  }
}
