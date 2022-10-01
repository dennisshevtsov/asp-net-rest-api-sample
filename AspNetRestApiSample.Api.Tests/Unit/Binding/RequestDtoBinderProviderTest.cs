// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Binding
{
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;
  using Moq;

  using AspNetRestApiSample.Api.Binding;

  [TestClass]
  public sealed class RequestDtoBinderProviderTest
  {
#pragma warning disable CS8618
    private MvcOptions _mvcOptions;
    private RequestDtoBinderProvider _requestDtoBinderProvider;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _mvcOptions = new MvcOptions();
      _requestDtoBinderProvider = new RequestDtoBinderProvider(_mvcOptions);
    }

    [TestMethod]
    public void GetBinder_Should_Return_Null()
    {
      var modelBinderProviderContextMock = new Mock<ModelBinderProviderContext>();
      var modelMeradataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestOtherDto)));

      modelBinderProviderContextMock.SetupGet(context => context.Metadata)
                                    .Returns(modelMeradataMock.Object)
                                    .Verifiable();

      var modelBinder =
        _requestDtoBinderProvider.GetBinder(
          modelBinderProviderContextMock.Object);

      Assert.IsNull(modelBinder);
    }

    private sealed class TestRequestDto : IRequestDto { }

    private sealed class TestOtherDto { }
  }
}
