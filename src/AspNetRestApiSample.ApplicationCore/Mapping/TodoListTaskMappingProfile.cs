﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.ApplicationCore.Mapping
{
  using AutoMapper;

  using AspNetRestApiSample.ApplicationCore.Dtos;
  using AspNetRestApiSample.ApplicationCore.Entities;

  /// <summary>Provides a named configuration for maps.</summary>
  public sealed class TodoListTaskMappingProfile : Profile
  {
    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.WebApi.MappingProfiles.TodoListTaskMappingProfile"/> class.</summary>
    public TodoListTaskMappingProfile()
    {
      TodoListTaskMappingProfile.ConfigureGetTodoListMapping(this);
      TodoListTaskMappingProfile.ConfigureSearchTodoListsMapping(this);
      TodoListTaskMappingProfile.ConfigureAddTodoListMapping(this);
      TodoListTaskMappingProfile.ConfigureUpdateTodoListMapping(this);
    }

    private static void ConfigureGetTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<TodoListTaskEntityBase, GetTodoListTaskResponseDtoBase>()
                .Include<TodoListDayTaskEntity, GetTodoListDayTaskResponseDto>()
                .Include<TodoListPeriodTaskEntity, GetTodoListPeriodTaskResponseDto>()
                .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));

      expression.CreateMap<TodoListDayTaskEntity, GetTodoListDayTaskResponseDto>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => TodoListTaskType.Day));
      expression.CreateMap<TodoListPeriodTaskEntity, GetTodoListPeriodTaskResponseDto>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => TodoListTaskType.Period));
    }

    private static void ConfigureSearchTodoListsMapping(IProfileExpression expression)
    {
      expression.CreateMap<TodoListTaskEntityBase, SearchTodoListTasksRecordResponseDtoBase>()
                .Include<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>()
                .Include<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>()
                .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));

      expression.CreateMap<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => TodoListTaskType.Day));
      expression.CreateMap<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>()
                .ForMember(dst => dst.Type, opt => opt.MapFrom(src => TodoListTaskType.Period));
    }

    private static void ConfigureAddTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<AddTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
                .Include<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
                .Include<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      expression.CreateMap<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      expression.CreateMap<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      expression.CreateMap<TodoListTaskEntityBase, AddTodoListTaskResponseDto>()
                .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));
    }

    private static void ConfigureUpdateTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<UpdateTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
                .Include<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
                .Include<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>()
                .ForMember(dst => dst.TodoListId,
                           opt =>
                           {
                             opt.Condition((src, dst) => dst.TodoListId == default);
                             opt.MapFrom(src => src.TodoListId);
                           })
                .ForMember(dst => dst.Id,
                           opt =>
                           {
                             opt.Condition((src, dst) => dst.Id == default);
                             opt.MapFrom(src => src.TodoListTaskId);
                           });

      expression.CreateMap<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      expression.CreateMap<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();
    }
  }
}
