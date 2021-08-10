using Microsoft.EntityFrameworkCore;
using Repositorio.Contexto;
using System.Collections.Generic;
using System.Linq;

namespace Repositorio.Base
{
    public class Repositorio<TEntity> : IRepositorio<TEntity> where TEntity : class
    {
        internal AppDbContext context;
        internal DbSet<TEntity> dbSet;

        public Repositorio(AppDbContext contextApp)
        {
            context = contextApp;
            dbSet = contextApp.Set<TEntity>();
        }

        public virtual IEnumerable<TEntity> List()
        {
            IQueryable<TEntity> query = dbSet;
            return query.ToList();
        }
        public virtual TEntity Adicionar(TEntity entityInsert)
        {
            dbSet.Add(entityInsert);
            return entityInsert;
        }
    }
}
