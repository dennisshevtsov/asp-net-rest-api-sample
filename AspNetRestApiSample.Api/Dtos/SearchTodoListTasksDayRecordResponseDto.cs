namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data of a TODO list day task for a response of request to search TODO list tasks.</summary>
  public sealed class SearchTodoListTasksDayRecordResponseDto : SearchTodoListTasksRecordResponseDtoBase
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public DateTime Date { get; set; }
  }
}
