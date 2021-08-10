using Entidades.Modelos;
using Microsoft.EntityFrameworkCore;

namespace Repositorio.Contexto
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        { }

        public virtual DbSet<HistoricoDNS> HistoricoDNS { get; set; }
    }
}
