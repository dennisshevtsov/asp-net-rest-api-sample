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

    private static void ConfigureGetTodoListMapping(Profile profile)
    {
      profile.CreateMap<TodoListTaskEntityBase, GetTodoListTaskResponseDtoBase>()
             .Include<TodoListDayTaskEntity, GetTodoListDayTaskResponseDto>()
             .Include<TodoListPeriodTaskEntity, GetTodoListPeriodTaskResponseDto>()
             .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));

      profile.CreateMap<TodoListDayTaskEntity, GetTodoListDayTaskResponseDto>();
      profile.CreateMap<TodoListPeriodTaskEntity, GetTodoListPeriodTaskResponseDto>();
    }

    private static void ConfigureSearchTodoListsMapping(Profile profile)
    {
      profile.CreateMap<TodoListTaskEntityBase, SearchTodoListTasksRecordResponseDtoBase>()
             .Include<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>()
             .Include<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>()
             .ForMember(dst => dst.TodoListTaskId, opt => opt.MapFrom(src => src.Id));

      profile.CreateMap<TodoListDayTaskEntity, SearchTodoListTasksDayRecordResponseDto>();
      profile.CreateMap<TodoListPeriodTaskEntity, SearchTodoListTasksPeriodRecordResponseDto>();
    }

    private static void ConfigureAddTodoListMapping(Profile profile)
    {
      profile.CreateMap<AddTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
             .Include<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
             .Include<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      profile.CreateMap<AddTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      profile.CreateMap<AddTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();

      profile.CreateMap<TodoListTaskEntityBase, AddTodoListTaskResponseDto>();
    }

    private static void ConfigureUpdateTodoListMapping(Profile profile)
    {
      profile.CreateMap<UpdateTodoListTaskRequestDtoBase, TodoListTaskEntityBase>()
             .Include<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>()
             .Include<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>()
             .ForMember(dst => dst.TodoListId, opt => opt.Ignore());

      profile.CreateMap<UpdateTodoListDayTaskRequestDto, TodoListDayTaskEntity>();
      profile.CreateMap<UpdateTodoListPeriodTaskRequestDto, TodoListPeriodTaskEntity>();
    }
  }
}
