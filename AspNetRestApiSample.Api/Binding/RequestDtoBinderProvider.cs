// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Binding
{
  using Microsoft.AspNetCore.Mvc.ModelBinding;

  using AspNetRestApiSample.Api.Dtos;

  /// <summary>Provides a simple API to create an instance of the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> class.</summary>
  public sealed class RequestDtoBinderProvider : IModelBinderProvider
  {
    /// <summary>Creates a <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> based on <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.ModelBinderProviderContext"/>.</summary>
    /// <param name="context">An object that represents a context for the <see cref="Microsoft.AspNetCore.Mvc.ModelBinding.IModelBinder"/> class.</param>
    /// <returns>An object that defines an interface for model binders.</returns>
    public IModelBinder? GetBinder(ModelBinderProviderContext context)
    {
      if (context.Metadata.ModelType.IsAssignableTo(typeof(IRequestDto)))
      {
        return new RequestDtoBinder();
      }

      return null;
    }
  }
}
