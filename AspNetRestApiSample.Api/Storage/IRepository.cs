using AspNetRestApiSample.Api.Entities;

namespace AspNetRestApiSample.Api.Storage
{
  public interface IRepository<TEntity> where TEntity : TodoListEntityBase
  {
    public TEntity Create(object command);

    public void Update(TEntity entity);

    public void Delete(TEntity entity);
  }
}
