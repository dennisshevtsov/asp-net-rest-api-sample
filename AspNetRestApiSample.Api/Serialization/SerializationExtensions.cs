// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace Microsoft.Extensions.DependencyInjection
{
  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Serialization;

  /// <summary>Provides a simple API to register application services.</summary>
  public static class SerializationExtensions
  {
    /// <summary>Confitures JSON serialization options.</summary>
    /// <param name="services">An object that specifies the contract for a collection of service descriptors.</param>
    /// <returns>An object that specifies the contract for a collection of service descriptors.</returns>
    public static void AddJsonSerialization(this IMvcBuilder builder)
    {
      builder.AddJsonOptions(options =>
             {
               options.JsonSerializerOptions.Converters.Add(new TodoListTaskWriterJsonConverter<GetTodoListTaskResponseDtoBase>());
               options.JsonSerializerOptions.Converters.Add(new TodoListTaskWriterJsonConverter<SearchTodoListTasksRecordResponseDtoBase>());
               options.JsonSerializerOptions.Converters.Add(new AddTodoListTaskRequestDtoBaseJsonConverter());
               options.JsonSerializerOptions.Converters.Add(new UpdateTodoListTaskRequestDtoBaseJsonConverter());
             });
    }
  }
}
