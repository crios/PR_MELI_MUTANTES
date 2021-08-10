using System.Collections.Generic;
namespace Repositorio.Base
{
    public interface IRepositorio<TEntity> where TEntity : class
    {
        IEnumerable<TEntity> List();
        TEntity Adicionar(TEntity entity);
    }
}
