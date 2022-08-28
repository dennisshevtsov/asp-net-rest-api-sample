// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

using AspNetRestApiSample.Api.Services;
using AspNetRestApiSample.Api.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoListService, TodoListService>();
builder.Services.AddScoped<ITodoListTaskService, TodoListTaskService>();

builder.Services.Configure<DatabaseOptions>(builder.Configuration);
builder.Services.AddDbContext<DbContext, AspNetRestApiSampleDbContext>(
  (provider, builder) =>
  {
    var options = provider.GetRequiredService<IOptions<DatabaseOptions>>().Value;

    if (string.IsNullOrWhiteSpace(options.AccountEndpoint))
    {
      throw new ArgumentNullException(nameof(options.AccountEndpoint));
    }

    if (string.IsNullOrWhiteSpace(options.AccountKey))
    {
      throw new ArgumentNullException(nameof(options.AccountEndpoint));
    }

    if (string.IsNullOrWhiteSpace(options.DatabaseName))
    {
      throw new ArgumentNullException(nameof(options.DatabaseName));
    }

    builder.UseCosmos(options.AccountEndpoint, options.AccountKey, options.DatabaseName);
  });

var app = builder.Build();

app.UseSwagger();
app.MapControllers();

app.Run();
