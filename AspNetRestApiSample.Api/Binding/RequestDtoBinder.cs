// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Binding
{
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  /// <summary>Provides a simple API to create an instance of a model for an HTTP request.</summary>
  public sealed class RequestDtoBinder : IModelBinder
  {
    private readonly IModelBinder _complexObjectModelBinder;
    private readonly IModelBinder _bodyModelBinder;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.Api.Binding.RequestDtoBinder"/> class.</summary>
    /// <param name="complexObjectModelBinder">An object that defines an interface for model binders.</param>
    /// <param name="bodyModelBinder">An object that defines an interface for model binders.</param>
    public RequestDtoBinder(
      IModelBinder complexObjectModelBinder,
      IModelBinder bodyModelBinder)
    {
      _complexObjectModelBinder = complexObjectModelBinder ??
        throw new ArgumentNullException(nameof(complexObjectModelBinder));
      _bodyModelBinder = bodyModelBinder ??
        throw new ArgumentNullException(nameof(bodyModelBinder));
    }

    /// <summary>Attempts to bind a model.</summary>
    /// <param name="bindingContext">An object that represents a context that contains operating information for model binding and validation.</param>
    /// <returns>An object that represents an asynchronous operation.</returns>
    public Task BindModelAsync(ModelBindingContext bindingContext)
    {
      if (bindingContext.ActionContext.HttpContext.Request.Method == HttpMethod.Get.Method)
      {
        return _complexObjectModelBinder.BindModelAsync(bindingContext);
      }

      return _bodyModelBinder.BindModelAsync(bindingContext);
    }
  }
}
