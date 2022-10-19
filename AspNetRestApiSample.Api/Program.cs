// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.Binding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCofiguredCors(builder.Configuration);
builder.Services.AddControllers(options => options.ModelBinderProviders.Insert(0, new RequestDtoBinderProvider(options)))
                .AddJsonSerialization();
builder.Services.AddSwaggerGen(options =>
                {
                  options.DescribeAllParametersInCamelCase();
                  options.UseOneOfForPolymorphism();
                });
builder.Services.AddServices();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.AddMapping();

var app = builder.Build();

app.UseSwagger();
app.UseCors();
app.MapControllers();
app.InitializeDatabase();

app.Run();
