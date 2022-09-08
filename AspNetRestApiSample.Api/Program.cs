// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.Serialization;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers()
                .AddJsonOptions(options =>
                {
                  options.JsonSerializerOptions.Converters.Add(new GetTodoListTaskResponseDtoBaseJsonConverter());
                  options.JsonSerializerOptions.Converters.Add(new SearchTodoListTasksRecordResponseDtoBaseJsonConverter())
                });
builder.Services.AddSwaggerGen();
builder.Services.AddServices();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMapping();

var app = builder.Build();

app.UseSwagger();
app.MapControllers();

app.Run();
