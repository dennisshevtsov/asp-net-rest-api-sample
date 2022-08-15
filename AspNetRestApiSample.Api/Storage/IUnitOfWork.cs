using AspNetRestApiSample.Api.Entities;

namespace AspNetRestApiSample.Api.Storage
{
  public interface IUnitOfWork
  {
    IRepository<TEntity> GetRepository<TEntity>() where TEntity : TodoListEntityBase;

    Task CommitAsync(CancellationToken cancellationToken);
  }
}
