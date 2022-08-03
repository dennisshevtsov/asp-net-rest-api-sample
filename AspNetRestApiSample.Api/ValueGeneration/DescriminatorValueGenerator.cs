// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.ValueGeneration
{
  using Microsoft.EntityFrameworkCore.ChangeTracking;
  using Microsoft.EntityFrameworkCore.ValueGeneration;

  public sealed class DescriminatorValueGenerator : ValueGenerator
  {
    public override bool GeneratesTemporaryValues => false;

    protected override object? NextValue(EntityEntry entry) => entry.Entity.GetType().Name;
  }
}
