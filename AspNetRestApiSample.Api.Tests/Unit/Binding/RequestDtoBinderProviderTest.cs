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
  using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;
  using Microsoft.Extensions.Logging;

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

      var modelBinder = _requestDtoBinderProvider.GetBinder(
        modelBinderProviderContextMock.Object);

      Assert.IsNull(modelBinder);
    }

    [TestMethod]
    public void GetBinder_Should_Throw_Exception_If_There_Is_No_Complex_Object_Model_Binder_Provider()
    {
      var modelBinderProviderContextMock = new Mock<ModelBinderProviderContext>();
      var modelMeradataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestRequestDto)));

      modelBinderProviderContextMock.SetupGet(context => context.Metadata)
                                    .Returns(modelMeradataMock.Object)
                                    .Verifiable();

      var exception = Assert.ThrowsException<InvalidOperationException>(
        () => _requestDtoBinderProvider.GetBinder(modelBinderProviderContextMock.Object));

      Assert.IsNotNull(exception);
      Assert.AreEqual("There is no complex object model binder provider.", exception.Message);
    }

    [TestMethod]
    public void GetBinder_Should_Throw_Exception_If_There_Is_No_Body_Model_Binder_Provider()
    {
      var modelBinderProviderContextMock = new Mock<ModelBinderProviderContext>();
      var modelMeradataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestRequestDto)));

      modelMeradataMock.SetupGet(metadata => metadata.Properties)
                       .Returns(new ModelPropertyCollection(new List<ModelMetadata>()));

      modelBinderProviderContextMock.SetupGet(context => context.Metadata)
                                    .Returns(modelMeradataMock.Object)
                                    .Verifiable();

      var serviceProviderMock = new Mock<IServiceProvider>();

      serviceProviderMock.Setup(provider => provider.GetService(It.IsAny<Type>()))
                         .Returns(new Mock<ILoggerFactory>().Object);

      modelBinderProviderContextMock.SetupGet(context => context.Services)
                                    .Returns(serviceProviderMock.Object);

      _mvcOptions.ModelBinderProviders.Add(new ComplexObjectModelBinderProvider());

      var exception = Assert.ThrowsException<InvalidOperationException>(
        () => _requestDtoBinderProvider.GetBinder(modelBinderProviderContextMock.Object));

      Assert.IsNotNull(exception);
      Assert.AreEqual("There is no body model binder provider.", exception.Message);
    }

    private sealed class TestRequestDto : IRequestDto
    {
      public int Property { get; set; }
    }

    private sealed class TestOtherDto { }
  }
}
