// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Mapping
{
  using AutoMapper;

  using AspNetRestApiSample.Dtos;
  using AspNetRestApiSample.Database.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class TodoListMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.MappingProfiles.TodoListMappingProfile"/> class.</summary>
    public TodoListMappingProfile()
    {
      TodoListMappingProfile.ConfigureGetTodoListMapping(this);
      TodoListMappingProfile.ConfigureSearchTodoListsMapping(this);
      TodoListMappingProfile.ConfigureAddTodoListMapping(this);
      TodoListMappingProfile.ConfigureUpdateTodoListMapping(this);
    }

    private static void ConfigureGetTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<TodoListEntity, GetTodoListResponseDto>();
    }

    private static void ConfigureSearchTodoListsMapping(IProfileExpression expression)
    {
      expression.CreateMap<TodoListEntity, SearchTodoListsRecordResponseDto>();
    }

    private static void ConfigureAddTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<AddTodoListRequestDto, TodoListEntity>();
      expression.CreateMap<TodoListEntity, AddTodoListResponseDto>();
    }

    private static void ConfigureUpdateTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<UpdateTodoListRequestDto, TodoListEntity>()
                .ForMember(dst => dst.TodoListId, opt => opt.Ignore());
    }
  }
}
