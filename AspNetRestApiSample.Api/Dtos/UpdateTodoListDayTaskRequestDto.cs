namespace AspNetRestApiSample.Api.Dtos
{
  /// <summary>Represents data to update todo list task.</summary>
  public sealed class UpdateTodoListDayTaskRequestDto : UpdateTodoListTaskRequestDtoBase
  {
    /// <summary>Gets/sets an object that represents a date of a TODO list task.</summary>
    public DateTime Date { get; set; }
  }
}
