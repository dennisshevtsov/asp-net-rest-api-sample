// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.Binding;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(options =>
                {
                  options.AddDefaultPolicy(policy => policy.WithOrigins("http://localhost:4200")
                                                           .WithMethods(HttpMethods.Options,
                                                                        HttpMethods.Get,
                                                                        HttpMethods.Post,
                                                                        HttpMethods.Put,
                                                                        HttpMethods.Delete));
                });
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
