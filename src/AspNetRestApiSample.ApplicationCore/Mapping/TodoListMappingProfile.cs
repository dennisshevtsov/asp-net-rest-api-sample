﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Mapping
{
  using AutoMapper;

  using AspNetRestApiSample.ApplicationCore.Dtos;
  using AspNetRestApiSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class TodoListMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.WebApi.MappingProfiles.TodoListMappingProfile"/> class.</summary>
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
      expression.CreateMap<IAddTodoListRequestDto, TodoListEntity>();
      expression.CreateMap<TodoListEntity, AddTodoListResponseDto>();
    }

    private static void ConfigureUpdateTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<IUpdateTodoListRequestDto, TodoListEntity>()
                .ForMember(dst => dst.TodoListId, opt => opt.Ignore());
    }
  }
}
