// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using System.Linq.Expressions;

  public class AsyncEnumerableMock<T> : EnumerableQuery<T>, IAsyncEnumerable<T>, IQueryable<T>
  {
    public AsyncEnumerableMock(IEnumerable<T> enumerable)
      : base(enumerable)
    { }

    public AsyncEnumerableMock(Expression expression)
      : base(expression)
    { }

    public IAsyncEnumerator<T> GetAsyncEnumerator(CancellationToken token)
    {
      return new AsyncEnumeratorMock<T>(this.AsEnumerable().GetEnumerator());
    }

    IQueryProvider IQueryable.Provider
    {
      get
      {
        return new AsyncQueryProviderMock<T>(this);
      }
    }
  }
}
