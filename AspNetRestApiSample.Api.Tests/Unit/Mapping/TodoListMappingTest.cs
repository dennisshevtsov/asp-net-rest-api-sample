// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Mapping
{
  using AutoMapper;
  using Microsoft.Extensions.DependencyInjection;

  [TestClass]
  public sealed class TodoListMappingTest
  {
#pragma warning disable CS8618
    private IDisposable _disposable;
    private IMapper _mapper;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      var serviceScope = new ServiceCollection().AddMapping().BuildServiceProvider().CreateScope();

      _disposable = serviceScope;
      _mapper = serviceScope.ServiceProvider.GetRequiredService<IMapper>();
    }

    [TestCleanup]
    public void CleanUp()
    {
      _disposable?.Dispose();
    }
  }
}
