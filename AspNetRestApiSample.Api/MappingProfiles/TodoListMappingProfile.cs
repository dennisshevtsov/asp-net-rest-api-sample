// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.MappingProfiles
{
  using AutoMapper;

  using AspNetRestApiSample.Api.Dtos;
  using AspNetRestApiSample.Api.Entities;

  public sealed class TodoListMappingProfile : Profile
  {
    public TodoListMappingProfile()
    {
      TodoListMappingProfile.ConfigureGetTodoListMapping(this);
      TodoListMappingProfile.ConfigureSearchTodoListsMapping(this);
      TodoListMappingProfile.ConfigureAddTodoListMapping(this);
      TodoListMappingProfile.ConfigureUpdateTodoListMapping(this);
    }

    private static void ConfigureGetTodoListMapping(Profile profile)
    {
      profile.CreateMap<TodoListEntity, GetTodoListResponseDto>();
    }

    private static void ConfigureSearchTodoListsMapping(Profile profile)
    {
      profile.CreateMap<TodoListEntity, SearchTodoListsRecordResponseDto>();
    }

    private static void ConfigureAddTodoListMapping(Profile profile)
    {
      profile.CreateMap<AddTodoListRequestDto, TodoListEntity>();
      profile.CreateMap<TodoListEntity, AddTodoListResponseDto>();
    }

    private static void ConfigureUpdateTodoListMapping(Profile profile)
    {
      profile.CreateMap<UpdateTodoListRequestDto, TodoListEntity>()
             .ForMember(dst => dst.TodoListId, opt => opt.Ignore());
    }
  }
}
