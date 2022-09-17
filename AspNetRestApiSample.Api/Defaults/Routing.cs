// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Defaults
{
  /// <summary>Provides values of routes.</summary>
  public static class Routing
  {
    /// <summary>A value that represents a base route for the TODO lists endpoints.</summary>
    public const string TodoListRoute = "api/todo-list";

    /// <summary>A value that represents a route to get a TODO list.</summary>
    public const string GetTodoListRoute = "{todoListId}";

    /// <summary>A value that represents a route to update a TODO list.</summary>
    public const string UpdateTodoListRoute = Routing.GetTodoListRoute;

    /// <summary>A value that represents a route to delete a TODO list.</summary>
    public const string DeleteTodoListRoute = Routing.GetTodoListRoute;

    /// <summary>A value that represents a base route for the TODO list tasks endpoints.</summary>
    public const string TodoListTaskRoute = Routing.GetTodoListRoute + "/task";

    /// <summary>A value that represents a route to get a TODO list task.</summary>
    public const string GetTodoListTaskRoute = "{todoListTaskId}";

    /// <summary>A value that represents a route to update a TODO list task.</summary>
    public const string UpdateTodoListTaskRoute = Routing.GetTodoListTaskRoute;

    /// <summary>A value that represents a route to delete a TODO list task.</summary>
    public const string DeleteTodoListTaskRoute = Routing.GetTodoListTaskRoute;

    /// <summary>A value that represents a route to complete a TODO list task.</summary>
    public const string CompleteTodoListTaskRoute = Routing.GetTodoListTaskRoute + "/complete";

    /// <summary>A value that represents a route to uncomplete a TODO list task.</summary>
    public const string UncompleteTodoListTaskRoute = Routing.GetTodoListTaskRoute + "/uncomplete";
  }
}
