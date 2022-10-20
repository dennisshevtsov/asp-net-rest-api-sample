// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCofiguredCors(builder.Configuration);
builder.Services.AddCofiguredControllers();
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
