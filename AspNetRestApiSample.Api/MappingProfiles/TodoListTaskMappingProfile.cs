// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.MappingProfiles
{
  using AutoMapper;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListTaskMappingProfile : Profile
  {
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

      expression.CreateMap<TodoListDayTaskEntity, GetTodoListDayTaskResponseDto>();
      expression.CreateMap<TodoListPeriodTaskEntity, GetTodoListPeriodTaskResponseDto>();
    }

    private static void ConfigureSearchTodoListsMapping(IProfileExpression expression)
    {
      expression.CreateMap<TodoListTaskEntityBase, SearchTodoListTasksRecordResponseDtoBase>()
                .Include<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>()
                .Include<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>()
                .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));

      expression.CreateMap<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>();
      expression.CreateMap<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>();
    }

    private static void ConfigureAddTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<AddTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
                .Include<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
                .Include<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      expression.CreateMap<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      expression.CreateMap<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      expression.CreateMap<TodoListTaskEntityBase, AddTodoListTaskResponseDto>();
    }

    private static void ConfigureUpdateTodoListMapping(IProfileExpression expression)
    {
      expression.CreateMap<UpdateTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
                .Include<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
                .Include<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>()
                .ForMember(dst => dst.TodoListId, opt => opt.Ignore());

      expression.CreateMap<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      expression.CreateMap<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();
    }
  }
}
