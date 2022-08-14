// Copyright (c) Dennis Shevtsov. All rights reserved.
// Licensed under the MIT License.
// See LICENSE in the project root for license information.

namespace AspNetRestApiSample.Api.Tests.Services
{
  using System.Linq.Expressions;
  
  using Microsoft.EntityFrameworkCore.Query;

  public class AsyncQueryProviderMock<TEntity> : IAsyncQueryProvider
  {
    private readonly IQueryProvider _queryProvider;

    public AsyncQueryProviderMock(IQueryProvider queryProvider)
    {
      _queryProvider = queryProvider ?? throw new ArgumentNullException(nameof(queryProvider));
    }

    public IQueryable CreateQuery(Expression expression)
    {
      return new AsyncEnumerableMock<TEntity>(expression);
    }

    public IQueryable<TElement> CreateQuery<TElement>(Expression expression)
    {
      return new AsyncEnumerableMock<TElement>(expression);
    }

    public object Execute(Expression expression)
    {
      return _queryProvider.Execute(expression)!;
    }

    public TResult Execute<TResult>(Expression expression)
    {
      return _queryProvider.Execute<TResult>(expression);
    }

    public TResult ExecuteAsync<TResult>(Expression expression, CancellationToken cancellationToken = new CancellationToken())
    {
      var resultType = typeof(TResult).GetGenericArguments()[0];
      var result = ((IQueryProvider)this).Execute(expression);

      return (TResult)typeof(Task).GetMethod(nameof(Task.FromResult))!
                                  .MakeGenericMethod(resultType)
                                  .Invoke(null, new[] { result })!;
    }
  }
}
