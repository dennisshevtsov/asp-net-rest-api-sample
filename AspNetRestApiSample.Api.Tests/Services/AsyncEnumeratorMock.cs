// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  public class AsyncEnumeratorMock<T> : IAsyncEnumerator<T>
  {
    private readonly IEnumerator<T> _enumerator;

    public AsyncEnumeratorMock(IEnumerator<T> enumerator)
    {
      _enumerator = enumerator ?? throw new ArgumentNullException(nameof(enumerator));
    }

    public T Current => _enumerator.Current;

    public ValueTask<bool> MoveNextAsync()
    {
      return new ValueTask<bool>(_enumerator.MoveNext());
    }

    public ValueTask DisposeAsync()
    {
      _enumerator.Dispose();

      return ValueTask.CompletedTask;
    }
  }
}
