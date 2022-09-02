// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.MappingProfiles;
using AspNetRestApiSample.Api.Services;
using AspNetRestApiSample.Api.Storage;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddAutoMapper(config =>
{
  config.AddProfile(new TodoListMappingProfile());
  config.AddProfile(new TodoListTaskMappingProfile());
});

var app = builder.Build();

app.UseSwagger();
app.MapControllers();

app.Run();
