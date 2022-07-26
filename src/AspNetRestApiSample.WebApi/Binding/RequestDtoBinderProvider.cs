﻿// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.WebApi.Binding
{
  using System;

  using Microsoft.AspNetCore.Mvc;
  using Microsoft.AspNetCore.Mvc.ModelBinding;
  using Microsoft.AspNetCore.Mvc.ModelBinding.Binders;

  using AspNetRestApiSample.ApplicationCore.Dtos;

  /// <summary>Provides a simple API to create an instance of the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> class.</summary>
  public sealed class RequestDtoBinderProvider : IModelBinderProvider
  {
    public const string NoComplextObjectModelBinderProviderMessage = "There is no complex object model binder provider.";
    public const string NoBodyModelBinderProviderMessage = "There is no body model binder provider.";

    private readonly MvcOptions _mvcOptions;

    /// <summary>Initializes a new instance of the <see cref="AspNetRestApiSample.WebApi.Binding.RequestDtoBinderProvider"/> class.</summary>
    /// <param name="mvcOptions">An object that provides programmatic configuration for the MVC framework.</param>
    public RequestDtoBinderProvider(MvcOptions mvcOptions)
    {
      _mvcOptions = mvcOptions ?? throw new ArgumentNullException(nameof(mvcOptions));
    }

    /// <summary>Creates a <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> based on <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext"/>.</summary>
    /// <param name="context">An object that represents a context for the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> class.</param>
    /// <returns>An object that defines an interface for model binders.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
      if (!context.Metadata.ModelType.IsAssignableTo(typeof(IRequestDto)))
      {
        return null;
      }

      context.BindingInfo.BindingSource = BindingSource.Body;

      var bodyModelBinderProvider = _mvcOptions.ModelBinderProviders.FirstOrDefault(
        provider => provider is BodyModelBinderProvider);

      if (bodyModelBinderProvider == null)
      {
        throw new InvalidOperationException(RequestDtoBinderProvider.NoBodyModelBinderProviderMessage);
      }

      var bodyModelBinder = bodyModelBinderProvider.GetBinder(context)!;

      var complexObjectModelBinderProvider = _mvcOptions.ModelBinderProviders.FirstOrDefault(
        provider => provider is ComplexObjectModelBinderProvider);

      if (complexObjectModelBinderProvider == null)
      {
        throw new InvalidOperationException(RequestDtoBinderProvider.NoComplextObjectModelBinderProviderMessage);
      }

      var complexObjectModelBinder = complexObjectModelBinderProvider.GetBinder(context)!;

      return new RequestDtoBinder(bodyModelBinder, complexObjectModelBinder);
    }
  }
}
