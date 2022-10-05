// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Unit.Binding
{
  using Microsoft.AspNetCore.Http;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Moq;

  using AspNetRestApiSample.Api.Binding;
  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Routing;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Metadata;

  [TestClass]
  public sealed class RequestDtoBinderTest
  {
#pragma warning disable CS8618
    private Mock<ModelBindingContext> _modelBindingContextMock;
    private Mock<HttpContext> _httpContextMock;
    private Mock<HttpRequest> _httpRequestMock;

    private Mock<IModelBinder> _bodyModelBinderMock;
    private Mock<IModelBinder> _complexObjectModelBinderMock;

    private RequestDtoBinder _requestDtoBinder;
#pragma warning restore CS8618

    [TestInitialize]
    public void Initialize()
    {
      _modelBindingContextMock = new Mock<ModelBindingContext>();
      _httpContextMock = new Mock<HttpContext>();
      _httpRequestMock = new Mock<HttpRequest>();

      _httpContextMock.SetupGet(context => context.Request)
                      .Returns(_httpRequestMock.Object)
                      .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.HttpContext)
                              .Returns(_httpContextMock.Object)
                              .Verifiable();

      _bodyModelBinderMock = new Mock<IModelBinder>();
      _complexObjectModelBinderMock = new Mock<IModelBinder>();

      _requestDtoBinder = new RequestDtoBinder(
        _bodyModelBinderMock.Object, _complexObjectModelBinderMock.Object);
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Call_Complex_Object_Model_Binder_If_Request_Is_Get()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethod.Get.Method)
                      .Verifiable();

      _complexObjectModelBinderMock.Setup(binder => binder.BindModelAsync(It.IsAny<ModelBindingContext>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

      await _requestDtoBinder.BindModelAsync(_modelBindingContextMock.Object);

      _complexObjectModelBinderMock.Verify();
      _complexObjectModelBinderMock.VerifyNoOtherCalls();

      _modelBindingContextMock.Verify();
      _modelBindingContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpRequestMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Call_Complex_Object_Model_Binder_If_Body_Is_Empty()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethod.Post.Method)
                      .Verifiable();

      _httpRequestMock.SetupGet(request => request.ContentLength)
                      .Returns(0)
                      .Verifiable();

      _complexObjectModelBinderMock.Setup(binder => binder.BindModelAsync(It.IsAny<ModelBindingContext>()))
                                   .Returns(Task.CompletedTask)
                                   .Verifiable();

      await _requestDtoBinder.BindModelAsync(_modelBindingContextMock.Object);

      _complexObjectModelBinderMock.Verify();
      _complexObjectModelBinderMock.VerifyNoOtherCalls();

      _modelBindingContextMock.Verify();
      _modelBindingContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpRequestMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Skip_Route_If_Body_Is_Not_Correct()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethod.Post.Method)
                      .Verifiable();

      _httpRequestMock.SetupGet(request => request.ContentLength)
                      .Returns(1024)
                      .Verifiable();

      _bodyModelBinderMock.Setup(binder => binder.BindModelAsync(It.IsAny<ModelBindingContext>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.Result)
                              .Returns(ModelBindingResult.Failed())
                              .Verifiable();

      await _requestDtoBinder.BindModelAsync(_modelBindingContextMock.Object);

      _complexObjectModelBinderMock.Verify();
      _complexObjectModelBinderMock.VerifyNoOtherCalls();

      _modelBindingContextMock.Verify();
      _modelBindingContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpRequestMock.VerifyNoOtherCalls();
    }

    [TestMethod]
    public async Task BindModelAsync_Should_Populate_Model_From_Route()
    {
      _httpRequestMock.SetupGet(request => request.Method)
                      .Returns(HttpMethod.Post.Method)
                      .Verifiable();

      _httpRequestMock.SetupGet(request => request.ContentLength)
                      .Returns(1024)
                      .Verifiable();

      _bodyModelBinderMock.Setup(binder => binder.BindModelAsync(It.IsAny<ModelBindingContext>()))
                          .Returns(Task.CompletedTask)
                          .Verifiable();

      var modelBindingResult = ModelBindingResult.Success(new TestRequestDto());

      _modelBindingContextMock.SetupGet(context => context.Result)
                              .Returns(modelBindingResult)
                              .Verifiable();

      _modelBindingContextMock.SetupSet(context => context.Result = It.IsAny<ModelBindingResult>())
                              .Verifiable();

      var modelId0 = Guid.NewGuid();
      var modelId1 = Guid.NewGuid();

      var actionContext = new ActionContext
      {
        RouteData = new RouteData
        {
          Values = {
            { nameof(TestRequestDto.ModelId0), modelId0.ToString() },
            { nameof(TestRequestDto.ModelId1), modelId1.ToString() },
          },
        },
      };

      _modelBindingContextMock.SetupGet(context => context.ActionContext)
                              .Returns(actionContext)
                              .Verifiable();

      var modelMetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForType(typeof(TestRequestDto)));

      var modelId0MetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestRequestDto).GetProperty(nameof(TestRequestDto.ModelId0))!,
          typeof(Guid),
          typeof(TestRequestDto)));

      modelId0MetadataMock.SetupGet(metadata => metadata.PropertySetter)
                          .Returns((object a, object? b) => { })
                          .Verifiable();

      var modelId1MetadataMock = new Mock<ModelMetadata>(
        ModelMetadataIdentity.ForProperty(
          typeof(TestRequestDto).GetProperty(nameof(TestRequestDto.ModelId1))!,
          typeof(Guid),
          typeof(TestRequestDto)));

      modelId1MetadataMock.SetupGet(metadata => metadata.PropertySetter)
                          .Returns((object a, object? b) => { })
                          .Verifiable();

      var properties = new ModelPropertyCollection(
        new[]
        {
          modelId0MetadataMock.Object,
          modelId1MetadataMock.Object,
        });

      modelMetadataMock.Setup(metadata => metadata.Properties)
                       .Returns(properties)
                       .Verifiable();

      _modelBindingContextMock.SetupGet(context => context.ModelMetadata)
                              .Returns(modelMetadataMock.Object)
                              .Verifiable();

      await _requestDtoBinder.BindModelAsync(_modelBindingContextMock.Object);

      _complexObjectModelBinderMock.Verify();
      _complexObjectModelBinderMock.VerifyNoOtherCalls();

      _modelBindingContextMock.Verify();
      _modelBindingContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpContextMock.VerifyNoOtherCalls();

      _httpContextMock.Verify();
      _httpRequestMock.VerifyNoOtherCalls();
    }

    private sealed class TestRequestDto
    {
      public Guid ModelId0 { get; set; }

      public Guid ModelId1 { get; set; }
    }
  }
}
